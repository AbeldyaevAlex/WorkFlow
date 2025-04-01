var reviewBlockState = {
    offset: 0,
    numberReviewsToLoad: 5,
    productGroupId: $('#product-group-id').val(),
    productId: $('#selected-productId').val()
}

function reviewFetcher() {
    fetch(`CommonProductGroup/GetNextReviews?groupId=${reviewBlockState.productGroupId}&offset=${reviewBlockState.offset}`).then(data => {
        return data.json();
    }).then(data => {
        reviewBlockState.offset += reviewBlockState.numberReviewsToLoad;
        appendFetchedReviewToShowedReviews(data)
    })
}

function submitFormValidate() {
    var reviewTitle = $('#txtReviewTitle').val();
    var reviewText = $('#taReviewText').val();
    if (reviewText == "" || reviewTitle == "") return false;
    return true;
}

function ShowMessageToUser(text) {
    debugger;
    $('#message-for-user').show().delay(5000).fadeOut();
    $('#message-for-user').text(text);
}

function reviewSender() {
    if (submitFormValidate()) {
        var reviewTitle = $('#txtReviewTitle').val();
        var reviewText = $('#taReviewText').val();
        var reviewRating = $("input[name='rating']:checked").val();

        var payLoad = {
            groupId: $('#product-group-id').val(),
            productId: $('#selected-productId').val(),
            Title: reviewTitle,
            Text: reviewText,
            reviewRating: reviewRating
        }

        debugger;
        fetch('CommonProductGroup/SubmitReview', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(payLoad)
        }).then(data => {
            if (data.Message != "ok") {
                ShowMessageToUser(data.Message)
            }
        });
    } else {
        ShowMessageToUser('Text and title should be filled');
    }

}

function hideShowAddReviewBlock() {
    debugger;
    var isRegistered = $('#registredField').val();
    if (isRegistered == 'True') {
        if ($('#submitReviewBlock').css('display') == 'none') {
            $('#submitReviewBlock').css('display', 'block')
        } else {
            $('#submitReviewBlock').css('display', 'none');
        }
    } else {
        $('#unregisteredTooltip').css('display', 'block');
    }
}

function appendFetchedReviewToShowedReviews(data) {
    repeatTemplate(data);
}

function findTemplate(templateId) {
    return $(templateId).clone().css('display','block');
}

function fillTemplateByObject(fillObject, template) {
    for (filler in fillObject) {
        $(template).find('#' + filler)
            .html(fillObject[filler])
    }
    return template;
}

function appendTemplate(template, appendToId) {
    $(appendToId).append(template)
}

function repeatTemplate(arr) {
    for (item in arr) {
        var tmp = findTemplate('#template1')
        var filledTmp = fillTemplateByObject(arr[item], tmp)
        appendTemplate(filledTmp, '#reviewItems')

    }

}

function replaceNumbersByStars(starsNumber) {
    var starsString = '';

    starsNumber = parseInt(starsNumber);

    for (var i = 0; i < starsNumber; i++)
        starsString += "&#9733";

    for (var i = 0; i < 5 - starsNumber; i++)
        starsString += "&#9734";

    return starsString
}

function getCapitalizedFirstCharachter(anyString) {
    if (anyString.length > 0) return anyString[0].toUpperCase()
    else return anyString;
}





