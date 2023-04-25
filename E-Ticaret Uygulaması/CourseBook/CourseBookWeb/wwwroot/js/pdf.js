document.getElementById("pdf-viewer").style.display = "none";


// Düğmeye tıklama olayını dinleyin
document.getElementById("view-pdf-button").addEventListener("click", function () {
    // PDF görüntüleyicinin oluşturulacağı div'in referansını alın
    document.getElementById("pdf-viewer").style.display = "block";
    const pdfViewer = document.getElementById("pdf-viewer");

    // PDF görüntüleyicisinin içeriğini oluşturun
    pdfViewer.innerHTML = `
          <object data="${pdfUrl}" type="application/pdf" width="100%" height="100%">
            <p>Bu tarayıcı PDF dosyalarını desteklemiyor. Lütfen PDF dosyasını indirin ve bilgisayarınızda açın.</p>
          </object>
        `;
});

document.getElementById("close-pdf-button").addEventListener("click", function () {
    // PDF görüntüleyici div'ini gizleyin
    document.getElementById("pdf-viewer").style.display = "none";
});
$(document).ready(function () {

    if ($('.brands_slider').length) {
        var brandsSlider = $('.brands_slider');

        brandsSlider.owlCarousel(
            {
                loop: true,
                autoplay: true,
                autoplayTimeout: 5000,
                nav: false,
                dots: false,
                autoWidth: true,
                items: 8,
                margin: 42
            });

        if ($('.brands_prev').length) {
            var prev = $('.brands_prev');
            prev.on('click', function () {
                brandsSlider.trigger('prev.owl.carousel');
            });
        }

        if ($('.brands_next').length) {
            var next = $('.brands_next');
            next.on('click', function () {
                brandsSlider.trigger('next.owl.carousel');
            });
        }
    }


});
