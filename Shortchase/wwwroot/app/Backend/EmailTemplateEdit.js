$(document).ready(function () {

    $('#EditContent').summernote();

    

    $('#EditEmailForm').submit(function (e) {
        e.preventDefault();


        var obj = {
            Id: $('#EditTemplateId').val(),
            Subject: $('#EditSubject').val(),
            Title: $('#EditTitle').val(),
            Content: $('#EditContent').val(),
        };

        let url = $('#UpdateNewEmailTemplateURL').val();
        let redirectURL = $('#RedirectURL').val();
        AJAXpost(url, obj, false, redirectURL);

    });


});
