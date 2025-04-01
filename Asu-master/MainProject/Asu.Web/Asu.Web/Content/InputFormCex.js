"use strict";
let h1 = document.querySelector("h2");
let button = document.querySelector('.gg');
let prom = document.querySelector('h1');
let prom_2 = prom.value;
console.log(prom_2);
let ci11 = document.querySelector(".form_input_cii");
let ci12 = document.querySelector(".form_input_cii12");
let n = 11111111111.9999999;

//let krat = document.querySelector(".InputKrat");
//let krat_2 = krat.value;




button.onclick = function () {
    if (ci11.value == 2) {
        ci12.value = ci11.value
    }    
    console.log(ci11.value);
    console.log(ci12.value);
};

//select.addEventListener('change', () => {
//    ci12.value = ci11.value;
//});
