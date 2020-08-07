$(document).ready(function () {
    $('#SignInModal_Form').submit(function (e) {
        e.preventDefault();
        
        let obj = {
            Email: $('#SignInModal_Email').val(),
            Password: $('#SignInModal_Password').val(),
            RememberMe: true
        };


        if (obj.Email.trim() !== "" || obj.Password.trim() !== "") {
            FullPageLoaderShow();
            let url = $('#SignInURL').val();

            $.ajax({
                url: url,
                type: 'POST',
                dataType: 'JSON',
                data: obj,
                success: function (data) {
                    let success = data.status;
                    if (success) {
                        document.location.reload();
                    }
                    else {
                        if (data.message === "ValidatePhone") {
                            document.location.href = $('#ValidatePhoneURL').val() + "?q=" + obj.Email;
                        }
                        else Swal.fire({ title: '' + data.messageTitle, text: '' + data.message, type: 'error' });
                    }
                },
                complete: function () {
                    FullPageLoaderHide();
                }
            });

        }
        else {
            Swal.fire(
                "Fill in information",
                'Enter your email and password and try again.',
                'error'
            );
            return false;
        }

    });

});