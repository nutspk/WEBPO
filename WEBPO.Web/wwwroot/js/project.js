"use strict";

$.ajaxPrefilter(function (options, originalOptions, jqXHR) {
    if (options.type.toUpperCase() == "POST") {
        let isFormData = options.data instanceof FormData;
        if (!isFormData) {
            options.data = $.param($.extend(originalOptions.data, { __RequestVerificationToken: $('[name="__RequestVerificationToken"]').val() }));
        }
    }

    if (options.type.toUpperCase() == "GET") {
        jqXHR.setRequestHeader('RequestVerificationToken', $('[name="__RequestVerificationToken"]').val());
    }
});


function showLoadding() {
    $('#overlay').fadeIn();
}

function hideLoadding() {
    $('#overlay').fadeOut();
}

function getResponse(data) {

    try {
        if (typeof (data) !== "object") data = JSON.parse(data);
    } catch (e) {
        data = null;
    }

    let myResponse = {
        success : data.Success,
        icon: 'error',
        message : data.Message,
        data : data.Data
    }

    switch (data.ReturnType) {
        case 1: myResponse.icon = "success"; break;
        case 2: myResponse.icon = "error"; break;
        case 3: myResponse.icon = "warning"; break;
        case 4: myResponse.icon = "info"; break;
        case 5: myResponse.icon = "question"; break;
        default: myResponse.icon = "error"; break;
    }
    
    return myResponse;
}


function alertFactory(props, fnConfirm) {
    Swal.fire(props).then(fnConfirm);
}

function fnConfirm(result, uri) {
    if (result.isConfirmed && uri) window.location.href = uri;
}

function applicationAlert(message, icon, uri, showCancelButton = false) {
    message = (message) ? message : $('#errorMessage').val();
    icon = (icon) ? icon : 'error';

    if (uri) {
        alertFactory({
            text: message,
            icon: icon,
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Ok',
            cancelButtonColor: 'Cancel',
            showCancelButton: showCancelButton,
            allowOutsideClick: false
        }, (result) => fnConfirm(result, uri));
    } else {
        alertFactory({
            text: message,
            icon: icon,
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Ok',
            cancelButtonColor: 'Cancel',
            allowOutsideClick: false,
            showCancelButton: showCancelButton,
        });
    }
}


