$(document).ready(function () {

    $('#CreateNewPasswordForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            Id: $('#UID').val(),
            Password: $('#Password').val(),
            RepeatPassword: $('#RepeatPassword').val(),
        };

        console.log(obj);

        if (!IsEmptyString(obj.Id) && !IsEmptyString(obj.Password) && !IsEmptyString(obj.RepeatPassword)) {

            if (obj.Password === obj.RepeatPassword) {
                var strongRegex = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})");
                if (strongRegex.test(obj.Password)) {

                    let url = $('#SaveUserNewPasswordURL').val();
                    let RedirectURL = $('#SaveUserNewPasswordRedirectURL').val();
                    AJAXpost(url, obj, false, RedirectURL);

                }
                else {
                    Swal.fire(
                        "Passwords not strong enough!",
                        'You password must be at least 8 characters long and contain the following: 1 uppercase character, 1 lowercase character, 1 number and 1 special character!',
                        'error'
                    );
                    return false;
                }
            }
            else {
                Swal.fire(
                    "Passwords don't match",
                    'Re-enter password and repeat the same password on the Repeat Password field.',
                    'error'
                );
                return false;
            }




        }
        else {
            return Swal.fire({ title: 'Form incomplete!', text: 'In order to proceed, you will need to fill the form.', type: 'error' });
        }


    });

});