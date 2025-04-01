"use strict";
let kodKomp = '';

kodKomp = document.querySelector("[id$='gvTTO_DXPEForm_DXEFL_DXEditor1_I']");
kodKomp.addEventListener('change', function (e) {
    $.ajax({
        type: "Post",
        url: "/TypicalTechnologicalOperations/TypicalTechnologicalOperations/GetKm?Km=" + kodKomp.value,
        data: "html",
        success: function (response) {

            /*document.querySelector("[id$='NaimOgt']").value = response[0].NaimOgt;*/
        }
    })
})