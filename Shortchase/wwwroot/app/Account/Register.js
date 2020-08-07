$(document).ready(function () {
    var form = $('#RegisterForm');//Give Id prop to your from
    form.removeData("validator");
    form.removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse(form);
});