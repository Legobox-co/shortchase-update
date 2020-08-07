$(document).ready(function () {

    $('#AddContent').summernote();

   

    $('#AddEmailForm').submit(function (e) {
        e.preventDefault();


        var obj = {
            Id: 0, 
            Subject: $('#AddSubject').val(),
            Title: $('#AddTitle').val(),
            Content: $('#AddContent').val(),
        };

        let url = $('#CreateNewEmailTemplateURL').val();
        let redirectURL = $('#RedirectURL').val();
        AJAXpost(url, obj, false, redirectURL);

    });



});
