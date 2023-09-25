
var swiper = new Swiper(".mySwiper", {
    spaceBetween: 10,
    slidesPerView: 4,
    freeMode: true,
    watchSlidesProgress: true,
});
var swiper2 = new Swiper(".mySwiper2", {
    spaceBetween: 10,
    navigation: {
    nextEl: ".swiper-button-next",
    prevEl: ".swiper-button-prev",
    },
    thumbs: {
    swiper: swiper,
    },
});


const plus = (element) => {
    console.log(element);
    let elementParent = element.parentNode;
    // console.log(elementParent);
    let input = elementParent.getElementsByTagName("input")[0];

    // Lấy giá trị hiện tại của thẻ input và ép sang kiểu số nguyên
    let currentValue = parseInt(input.value);

    // Tăng giá trị lên 1
    let newValue = currentValue + 1;

    // Cập nhật giá trị mới vào thẻ input
    input.value = newValue;
}
const minus = (element) => {
    console.log(element);
    let elementParent = element.parentNode;
    // console.log(elementParent);
    let input = elementParent.getElementsByTagName("input")[0];

    // Lấy giá trị hiện tại của thẻ input và ép sang kiểu số nguyên
    let currentValue = parseInt(input.value);

    // Tăng giá trị lên 1
    let newValue = currentValue - 1;

    // Cập nhật giá trị mới vào thẻ input
    input.value = newValue;
}


