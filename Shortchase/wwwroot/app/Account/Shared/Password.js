$('.PasswordVisibility').click(function () {
    var Pass = $('.Password');
    Pass.each(function () {
        if (this.type === "password") {
            $(this).html('<i class="far fa-eye-slash"></i>');
            this.type = "text";
        } else {
            $(this).html('<i class="far fa-eye"></i>');
            this.type = "password";
        }
    });
});