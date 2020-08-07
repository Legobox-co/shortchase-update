$(document).ready(function () {
    var form = $('#RegisterForm');
    form.removeData("validator");
    form.removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse(form);
});