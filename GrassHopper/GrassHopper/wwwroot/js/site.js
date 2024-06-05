// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", function () {
    var reviews = document.querySelectorAll(".slide-in");
    reviews.forEach(function (review) {
        setTimeout(function () {
            review.classList.add("active");
        }, 100);
    });
});
document.addEventListener("DOMContentLoaded", function () {
    var reviews = document.querySelectorAll(".slide-in-stagger");
    reviews.forEach(function (review) {
        setTimeout(function () {
            review.classList.add("active");
        }, 200);
    });
});
document.addEventListener("DOMContentLoaded", function () {
    var reviews = document.querySelectorAll(".slide-in-stagger2");
    reviews.forEach(function (review) {
        setTimeout(function () {
            review.classList.add("active");
        }, 300);
    });
});
const textarea = document.getElementById('ReviewBody');
const charCount = document.getElementById('charCount');
const maxChars = 528;

textarea.addEventListener('input', () => {
    const remaining = maxChars - textarea.value.length;
    charCount.textContent = `${remaining} characters remaining`;
});