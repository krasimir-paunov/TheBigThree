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