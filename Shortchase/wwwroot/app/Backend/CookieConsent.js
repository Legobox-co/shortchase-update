$(document).ready(function () {

    $('.UpdateCookieConsentBtn').click(function (e) {
        e.preventDefault();

        let obj = {
            text: $('#CookieConsentMessage').val(),
        };
        let url = $('#UpdateCookieConsentURL').val();
        let reloadAfterAjax = true;
        AJAXpost(url, obj, reloadAfterAjax);
    });
});