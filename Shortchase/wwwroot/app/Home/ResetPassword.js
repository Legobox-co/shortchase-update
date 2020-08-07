$(document).ready(function () {

    $('#ResetPasswordForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            Email: $('#PwdRecoveryEmail').val(),
        };
        if (!IsEmptyString(obj.Email)) {
            let emailRegex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
            if (emailRegex.test(obj.Email)) {

                let url = $('#ResetUserPasswordURL').val();
                let RedirectURL = $('#ResetUserPasswordRedirectURL').val();
                AJAXpost(url, obj, false, RedirectURL);
            }
            else {
                return Swal.fire({ title: 'Email format incorrect!', text: 'In order to proceed, you will need to provide a valid email address.', type: 'error' });
            }
        }
        else {
            return Swal.fire({ title: 'Form incomplete!', text: 'In order to proceed, you will need to provide a valid email address.', type: 'error' });
        }
        
        
    });

});