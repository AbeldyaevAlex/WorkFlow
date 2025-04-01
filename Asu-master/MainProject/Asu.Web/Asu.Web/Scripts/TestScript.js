$('.menu-btn').on('click', function(e) {
    e.preventDefault;
    $(this).toggleClass('menu-btn-active');
    $('.menu-nav').toggleClass('menu-nav-active');
});
$('.burger-menu-button').on('click', function () {
    $('.burger-menu').toggleClass('menu-active');
})
$('.naim-skm-button k-button').on('click', function () {
    $('.burger-menu').toggleClass('menu-active');
})