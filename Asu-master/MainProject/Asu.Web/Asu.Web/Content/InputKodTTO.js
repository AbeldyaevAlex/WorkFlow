"use strict";
let button = document.querySelector('.button_form');
let id = 0.0000000;
let vpost_sh = 0;
let nrvp = 0;
let kodKomp = "";
id = document.querySelector("[id$='nrm_I']");
vpost = document.querySelector("[id$='vpost_I']");
nrvp = document.querySelector("[id$='nrvp_I']");
kodKomp = document.querySelector("[id$='KodKomp_I']");


//vpost = vpost_I.value;

nrvp.addEventListener('change', function (e) {
    if (vpost.value != "0" && nrvp.value != "0.0000000") {
        id.value = e.target.value / vpost.value;
    }    
})
vpost.addEventListener('change', function (e) {
    if (vpost.value != "0" && nrvp.value != "0.0000000") {
        id.value = e.target.value / vpost.value;
    }
})
kodKomp.addEventListener('change', function (e) {
    $.ajax({
        type: "Post",
        url: "/TypicalTechnologicalOperations/TypicalTechnologicalOperations/GetKm?Km=" + kodKomp.value,
        data: "html",
        success: function (response) {
            var t = response[0]
            /*document.querySelector("[id$='MarkaMater_I']").value = response[0].SprSkm.Km;*/
            /*document.querySelector("[id$='NaimOgt']").value = response[0].NaimOgt;*/
        }
    })
})


//id = document.querySelector('.test');


//button.onclick = function () {
//    vpost_sh = vpost_sh_I.value;
//    nrvp = nrvp_I.value;
//    vpost = vpost_I.value;
//    //number = (+nrvp - +vpost_sh);
//    let number = (+nrvp / +vpost);
//    id.value = number;
//};
