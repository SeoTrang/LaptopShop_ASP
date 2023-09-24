var swiper = new Swiper('.swiper-container', {
    autoplay: {
        delay: 2000,
    },
    loop: true,
    pagination: {
        el: '.swiper-pagination',
        clickable: true,
    },
    navigation: {
        nextEl: '.swiper-button-next', // Sử dụng lớp CSS của nút chuyển slide sang phải
        prevEl: '.swiper-button-prev', // Sử dụng lớp CSS của nút chuyển slide sang trái
    },
});
