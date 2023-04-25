using CourseBook.DataAccess.Repository.IRepository;
using CourseBook.Models;
using CourseBook.Models.ViewModels;
using CourseBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

using System.Security.Claims;

namespace CourseBookWeb.Areas.Customer.Controllers
{
	[Area("Customer")]
	[Authorize]
	public class CartController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        [BindProperty]
		public ShoppingCartViewModel ShoppingCartViewModel { get; set; }
		public int OrderTotal { get; set; }
        public CartController(IUnitOfWork unitOfWork, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
        }

        public IActionResult Index()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			ShoppingCartViewModel = new ShoppingCartViewModel()
			{
				ListCart = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value,
				includeProperties: "Product"),
				OrderHeader = new()
			};
			foreach (var cart in ShoppingCartViewModel.ListCart)
			{
				cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price,
					cart.Product.Price50, cart.Product.Price100);
				ShoppingCartViewModel.OrderHeader.OrderTotal += (cart.Price * cart.Count);
			}
			return View(ShoppingCartViewModel);
		}


		public IActionResult Summary()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			ShoppingCartViewModel = new ShoppingCartViewModel()
			{
				ListCart = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value,
				includeProperties: "Product"),
				OrderHeader = new()
			};
			ShoppingCartViewModel.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(
			   u => u.Id == claim.Value);

			ShoppingCartViewModel.OrderHeader.Name = ShoppingCartViewModel.OrderHeader.ApplicationUser.Name;
			ShoppingCartViewModel.OrderHeader.Email = ShoppingCartViewModel.OrderHeader.ApplicationUser.Email;

			foreach (var cart in ShoppingCartViewModel.ListCart)
			{
				cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price,
					cart.Product.Price50, cart.Product.Price100);
				ShoppingCartViewModel.OrderHeader.OrderTotal += (cart.Price * cart.Count);
			}
			return View(ShoppingCartViewModel);
		}
		[HttpPost]
		[ActionName("Summary")]
		[ValidateAntiForgeryToken]
		public IActionResult SummaryPOST()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			ShoppingCartViewModel.ListCart = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value,
				includeProperties: "Product");

			ShoppingCartViewModel.OrderHeader.OrderDate = System.DateTime.Now;
			ShoppingCartViewModel.OrderHeader.ApplicationUserId = claim.Value;


			foreach (var cart in ShoppingCartViewModel.ListCart)
			{
				cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price,
					cart.Product.Price50, cart.Product.Price100);
				ShoppingCartViewModel.OrderHeader.OrderTotal += (cart.Price * cart.Count);
			}

			ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);

			if (applicationUser.CompanyId.GetValueOrDefault() == 0)
			{
				ShoppingCartViewModel.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
				ShoppingCartViewModel.OrderHeader.OrderStatus = SD.StatusPending;
			}
			else
			{
				ShoppingCartViewModel.OrderHeader.PaymentStatus = SD.PaymentStatusDelayedPayment;
				ShoppingCartViewModel.OrderHeader.OrderStatus = SD.StatusApproved;
			}

			_unitOfWork.OrderHeader.Add(ShoppingCartViewModel.OrderHeader);
			_unitOfWork.Save();
			foreach (var cart in ShoppingCartViewModel.ListCart)
			{
				OrderDetail orderDetail = new()
				{
					ProductId = cart.ProductId,
					OrderId = ShoppingCartViewModel.OrderHeader.Id,
					Price = cart.Price,
					Count = cart.Count
				};
				_unitOfWork.OrderDetail.Add(orderDetail);
				_unitOfWork.Save();
			}
			if (applicationUser.CompanyId.GetValueOrDefault() == 0)
			{
				var domain = "https://localhost:44323/";
				var options = new SessionCreateOptions
				{
					PaymentMethodTypes = new List<string>
				{
				  "card",
				},
					LineItems = new List<SessionLineItemOptions>(),
					Mode = "payment",
					SuccessUrl = domain + $"customer/cart/OrderConfirmation?id={ShoppingCartViewModel.OrderHeader.Id}",
					CancelUrl = domain + $"customer/cart/index",
				};
				foreach (var item in ShoppingCartViewModel.ListCart)
				{

					var sessionLineItem = new SessionLineItemOptions
					{
						PriceData = new SessionLineItemPriceDataOptions
						{
							UnitAmount = (long)(item.Price * 100),//20.00 -> 2000
							Currency = "try",
							ProductData = new SessionLineItemPriceDataProductDataOptions
							{
								Name = item.Product.Title
							},

						},
						Quantity = item.Count,
					};
					options.LineItems.Add(sessionLineItem);

				}

				var service = new SessionService();
				Session session = service.Create(options);
				_unitOfWork.OrderHeader.UpdateStripePaymentID(ShoppingCartViewModel.OrderHeader.Id, session.Id, session.PaymentIntentId);
				_unitOfWork.Save();
				Response.Headers.Add("Location", session.Url);
				return new StatusCodeResult(303);
			}
			else
			{
				return RedirectToAction("OrderConfirmation", "Cart", new { id = ShoppingCartViewModel.OrderHeader.Id });
			}

			//_unitOfWork.ShoppingCart.RemoveRange(ShoppingCartVM.ListCart);
			//_unitOfWork.Save();
			//return RedirectToAction("Index", "CustomerHome");
		}

		public IActionResult OrderConfirmation(int id)
		{
			OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == id, includeProperties: "ApplicationUser");
			if (orderHeader.PaymentStatus != SD.PaymentStatusDelayedPayment)
			{
				var service = new SessionService();
				Session session = service.Get(orderHeader.SessionId);
				if (session.PaymentStatus.ToLower() == "paid")
				{
					_unitOfWork.OrderHeader.UpdateStatus(id, SD.StatusApproved, SD.PaymentStatusApproved);
					_unitOfWork.Save();
				}
			}
            _emailSender.SendEmailAsync(orderHeader.ApplicationUser.Email, "New Order - Udumate Course", "<p>New Order Created</p>");
            List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId ==
		   orderHeader.ApplicationUserId).ToList();
            HttpContext.Session.Clear();
            _unitOfWork.ShoppingCart.RemoveRange(shoppingCarts);
			_unitOfWork.Save();
			return View(id);
		}


			public IActionResult Plus(int cartId)
		{
			var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
			_unitOfWork.ShoppingCart.IncrementCount(cart, 1);
			_unitOfWork.Save();
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Minus(int cartId)
		{
			var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
			if (cart.Count <= 1)
			{
				_unitOfWork.ShoppingCart.Remove(cart);
                var count = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count - 1;
                HttpContext.Session.SetInt32(SD.SessionCart, count);
            }
			else
			{
				_unitOfWork.ShoppingCart.DecrementCount(cart, 1);
			}
			_unitOfWork.Save();
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Remove(int cartId)
		{
			var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
			_unitOfWork.ShoppingCart.Remove(cart);
			_unitOfWork.Save();
            var count = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count;
            HttpContext.Session.SetInt32(SD.SessionCart, count);
            return RedirectToAction(nameof(Index));
		}

		private double GetPriceBasedOnQuantity(double quantity, double price, double price2, double pricecupon)
		{
			if (quantity <= 1)
			{
				return price;
			}
			else
			{
				if (quantity <= 2)
				{
					return price2;
				}
				return pricecupon;
			}
		}

	}
}
