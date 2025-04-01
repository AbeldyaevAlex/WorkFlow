"use strict";
let first = document.querySelector('.first-class')
let second = document.querySelector('.second-class')
let itog = document.querySelector('.itog');
second.addEventListener('change', function (e) {
    itog.value = e.target.value / first.value;
})
first.addEventListener('change', function (e) {
    itog.value = e.target.value / first.value;
})
