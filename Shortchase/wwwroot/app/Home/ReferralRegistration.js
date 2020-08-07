$(document).ready(function () {
    $('.verification-selector-email-referral').click(function (e) {
        $('#SignUpModalReferral_VerificationType').val('email');
        $('#SignUpModalReferral_ValidateEmail, #SignUpModalReferral_ValidateEmail2').prop('checked', true);
        $('#SignUpModalReferral_PhoneInfo').hide(150);
    });

    $('.verification-selector-text-referral').click(function (e) {
        $('#SignUpModalReferral_VerificationType').val('phone');
        $('#SignUpModalReferral_ValidateText, #SignUpModalReferral_ValidateText2').prop('checked', true);
        $('#SignUpModalReferral_PhoneInfo').show(150);
    });

    $('#SignUpModalReferral_Form').submit(function (e) {
        e.preventDefault();
        let obj = {
            Email: $('#SignUpModalReferral_Email').val(),
            FirstName: $('#SignUpModalReferral_FirstName').val(),
            LastName: $('#SignUpModalReferral_LastName').val(),
            BirthDate: $('#SignUpModalReferral_BirthDate').val(),
            Password: $('#SignUpModalReferral_Password').val(),
            RepeatPassword: $('#SignUpModalReferral_RepeatPassword').val(),
            VerificationType: $('#SignUpModalReferral_VerificationType').val(),
            PhoneCountry: null,
            PhoneNumber: null,
            FullPhone: null,
            Boisterous: $('#SignUpModalReferral_Boisterous').val(),
            ShortchasePro: $('#SignUpModalReferral_ShortchasePro').val(),
            ReferredByEmail: $('#SignUpModalReferral_ReferredByEmail').val(),
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
            obj.PhoneCountry = $('#SignUpModalReferral_PhoneCountry').val();
            obj.PhoneNumber = $('#SignUpModalReferral_Phone').val();
            obj.FullPhone = "+" + $('#SignUpModalReferral_countryCode').html() + obj.PhoneNumber;
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
            
            FullPageLoaderShow();
            let url = $('#RegisterReferredUserUrl').val();

            $.ajax({
                url: url,
                type: 'POST',
                dataType: 'JSON',
                data: obj,
                success: function (data) {
                    let success = data.status;
                    if (success) {
                        Swal.fire({ title: '' + data.messageTitle, text: '' + data.message, type: 'success', allowOutsideClick: false, allowEscapeKey: false }).then(() => {
                            loadFilteredPage($('#RedirectUrl').val(), { TimeOffset: new Date().getTimezoneOffset() });
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
                "Passwords don't match",
                'Re-enter password and repeat the same password on the Repeat Password field.',
                'error'
            );
            return false;
        }

    });
});