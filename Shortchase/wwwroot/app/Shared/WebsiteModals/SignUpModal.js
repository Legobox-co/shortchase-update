$(document).ready(function () {

    $('.verification-selector-email').click(function (e) {
        $('#SignUpModal_VerificationType').val('email');
        $('#SignUpModal_ValidateEmail, #SignUpModal_ValidateEmail2').prop('checked', true);
        $('#SignUpModal_PhoneInfo').hide(150);
    });

    $('.verification-selector-text').click(function (e) {
        $('#SignUpModal_VerificationType').val('phone');
        $('#SignUpModal_ValidateText, #SignUpModal_ValidateText2').prop('checked', true);
        $('#SignUpModal_PhoneInfo').show(150);
    });

    $('#SignUpModal_Form').submit(function (e) {
        e.preventDefault();
        let obj = {
            Email: $('#SignUpModal_Email').val(),
            FirstName: $('#SignUpModal_FirstName').val(),
            LastName: $('#SignUpModal_LastName').val(),
            BirthDate: $('#SignUpModal_BirthDate').val(),
            Password: $('#SignUpModal_Password').val(),
            RepeatPassword: $('#SignUpModal_RepeatPassword').val(),
            VerificationType: $('#SignUpModal_VerificationType').val(),
            PhoneCountry: null,
            PhoneNumber: null,
            FullPhone: null,
            Boisterous: $('#SignUpModal_Boisterous').val(),
            ShortchasePro: $('#SignUpModal_ShortchasePro').val(),
        };
        
        if (!IsEmptyString(obj.BirthDate)) {
            let correctDate = obj.BirthDate.split('/');
            obj.BirthDate = correctDate[2] + '-' + correctDate[0] + '-' + correctDate[1];

            let dateSplit = obj.BirthDate.split('-');
            if (parseInt(dateSplit[0]) < 1900 || parseInt(dateSplit[0]) > 2030) {
                Swal.fire(
                    "Birthdate error!",
                    'The year must be between 1900 and 2100.',
                    'error'
                );
                return false;
            }
            if (parseInt(dateSplit[1]) <= 0 || parseInt(dateSplit[1]) > 12) {
                Swal.fire(
                    "Birthdate error!",
                    'The month must be between 1 and 12.',
                    'error'
                );
                return false;
            }
            if (parseInt(dateSplit[2]) <= 0 || parseInt(dateSplit[2]) > 31) {
                Swal.fire(
                    "Birthdate error!",
                    'The date must be between 1 and 31.',
                    'error'
                );
                return false;
            }
        }
        else {
            Swal.fire(
                "Birthdate error!",
                'You need to provide a valid birth date.',
                'error'
            );
            return false;
        }


        if (obj.VerificationType === "phone") {
            obj.PhoneCountry = $('#SignUpModal_PhoneCountry').val();
            obj.PhoneNumber = $('#SignUpModal_Phone').val();
            obj.FullPhone = "+" + $('#SignUpModal_countryCode').html() + obj.PhoneNumber;
            if (obj.PhoneNumber === null || obj.PhoneNumber === "" || obj.PhoneNumber.trim() === "") {
                Swal.fire(
                    "Phone number mandatory",
                    'To validate your account with Text, you will need to provide a phone number.',
                    'error'
                );
                return false;
            }
        }




        if (obj.Password === obj.RepeatPassword) {
            var strongRegex = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})");
            if (strongRegex.test(obj.Password)) {
                FullPageLoaderShow();
                let url = $('#RegisterUserURL').val();

                $.ajax({
                    url: url,
                    type: 'POST',
                    dataType: 'JSON',
                    data: obj,
                    success: function (data) {
                        let success = data.status;
                        if (success) {
                            Swal.fire({ title: '' + data.messageTitle, text: '' + data.message, type: 'success', allowOutsideClick: false, allowEscapeKey: false }).then(() => {
                                location.reload();
                            });
                        }
                        else {
                            Swal.fire({ title: '' + data.messageTitle, text: '' + data.message, type: 'error' });
                        }
                    },
                    complete: function () {
                        FullPageLoaderHide();
                    }
                });
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

    });

});