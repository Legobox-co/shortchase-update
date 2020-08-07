$(document).ready(function () {
    $('#PrivacyPolicyInput').summernote();

    $('.UpdatePrivacyPolicyBtn').click(function (e) {
        e.preventDefault();

        let obj = {
            text: $('#PrivacyPolicyInput').val(),
        };
        let url = $('#UpdatePrivacyPolicyURL').val();
        let reloadAfterAjax = true;
        AJAXpost(url, obj, reloadAfterAjax);
    });
});