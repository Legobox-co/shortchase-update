$(document).ready(function () {
    $('#SessionManagerForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            SessionTimeout: parseInt($('#SessionTimeout').val()),
        };
        let url = $('#UpdateSessionManagerURL').val();
        let reloadAfterAjax = true;
        AJAXpost(url, obj, reloadAfterAjax);

    });
});