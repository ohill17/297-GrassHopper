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