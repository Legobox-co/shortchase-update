$(document).ready(function () {
    $('#EditAnswer').summernote('destroy');
    $('#TermosOfServiceInput').summernote();

    $('.UpdateTermsOfServiceBtn').click(function (e) {
        e.preventDefault();

        let obj = {
            text: $('#TermosOfServiceInput').val(),
        };
        let url = $('#UpdateTermsOfServiceURL').val();
        let reloadAfterAjax = true;
        AJAXpost(url, obj, reloadAfterAjax);
    });
});