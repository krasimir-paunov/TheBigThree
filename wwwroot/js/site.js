window.addEventListener('scroll', function () {
    var btn = document.getElementById('backToTop');
    if (window.scrollY > 150) {
        btn.style.opacity = '1';
        btn.style.pointerEvents = 'auto';
    } else {
        btn.style.opacity = '0';
        btn.style.pointerEvents = 'none';
    }
});

document.addEventListener('DOMContentLoaded', function () {
    var toasts = document.querySelectorAll('.toast');
    toasts.forEach(function (toastEl) {
        var toast = new bootstrap.Toast(toastEl);
        toast.show();
    });
});

