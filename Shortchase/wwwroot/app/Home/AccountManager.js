$(document).ready(function () {

    $('.account-manager-navigation').click(function (e) {
        e.preventDefault();

        let navigateTo = $(this).data('navigate-to');
        if (navigateTo === "PersonalInfoTab") {
            $('#RewardsTab, #ReferralTab, #PaymentTab, #SubscriptionsTab').hide(150, function () {
                $('#' + navigateTo).show(150);
            });
        }
        else if (navigateTo === "RewardsTab") {
            $('#PersonalInfoTab, #ReferralTab, #PaymentTab, #SubscriptionsTab').hide(150, function () {
                $('#' + navigateTo).show(150);
            });
        }
        else if (navigateTo === "ReferralTab") {
            $('#PersonalInfoTab, #RewardsTab, #PaymentTab, #SubscriptionsTab').hide(150, function () {
                $('#' + navigateTo).show(150);
            });
        }
        else if (navigateTo === "SubscriptionsTab") {
            $('#PersonalInfoTab, #RewardsTab, #PaymentTab, #ReferralTab').hide(150, function () {
                $('#' + navigateTo).show(150);
            });
        }
        else {
            $('#PersonalInfoTab, #RewardsTab, #ReferralTab, #SubscriptionsTab').hide(150, function () {
                $('#' + navigateTo).show(150);
            });
        }
    });


    $('#inputGroupFile01').change(function () {
        var filename = $('#inputGroupFile01').val().split('\\').pop();
        if (filename.trim() === "") filename = 'Choose Photo';
        $('#pictureLabel').html(filename);
    });

    $('#SavePersonalInfoBtn').click(function (e) {
        e.preventDefault();

        var formData = new FormData();

        let obj = {
            Id: $('#AccountId').val(),
            FirstName: $('#AccountManager_FirstName').val(),
            LastName: $('#AccountManager_LastName').val(),
            Email: $('#AccountManager_Email').val(),
            Company: $('#AccountManager_Company').val(),
            PhoneCountry: parseInt($('#AccountManager_PhoneCountry').val()),
            Phone: $('#AccountManager_Phone').val(),
            ProfilePicture: $('#AccountProfilePicture').val(),
            DateOfBirth: $('#AccountManager_DateOfBirth').val(),
            BioDescription: $('#AccountManager_BioDescription').val(),
            OldPassword: $('#AccountManager_OldPassword').val(),
            NewPassword: $('#AccountManager_NewPassword').val(),
            PictureFile: null
        };

        if (obj.OldPassword.trim() === "" && obj.NewPassword.trim() === "") {
            obj.PasswordChanged = false;
        }
        else {
            obj.PasswordChanged = true;
        }
        let pictureName = null;
        if (($('#inputGroupFile01').val() === "" || obj.ProfilePicture === $('#inputGroupFile01').val())) {
            obj.PictureChanged = false;
        }
        else {
            obj.PictureChanged = true;
            var fileInput = document.getElementById('inputGroupFile01');
            if (fileInput.files.length > 0) {
                obj.PictureFile = fileInput.files[0];
                pictureName = obj.PictureFile.name;
            }
        }

        let correctDate = obj.DateOfBirth.split('/');
        obj.DateOfBirth = correctDate[2] + '-' + correctDate[0] + '-' + correctDate[1];
        formData.append('data.Id', obj.Id);
        formData.append('data.FirstName', obj.FirstName);
        formData.append('data.LastName', obj.LastName);
        formData.append('data.Email', obj.Email);
        formData.append('data.Company', obj.Company);
        formData.append('data.PhoneCountry', obj.PhoneCountry);
        formData.append('data.Phone', obj.Phone);
        formData.append('data.ProfilePicture', obj.ProfilePicture);
        formData.append('data.DateOfBirth', obj.DateOfBirth);
        formData.append('data.BioDescription', obj.BioDescription);
        formData.append('data.OldPassword', obj.OldPassword);
        formData.append('data.NewPassword', obj.NewPassword);
        formData.append('data.PasswordChanged', obj.PasswordChanged);
        formData.append('data.PictureChanged', obj.PictureChanged);
        if (obj.PictureChanged) {
            formData.append('data.PictureFile', obj.PictureFile, pictureName);
        }

        let url = $('#AccountManagerUpdatePersonalInfoURL').val();

        FullPageLoaderShow();
        $.ajax({
            url: url,
            type: 'POST',
            dataType: 'JSON',
            data: formData,
            success: function (data) {
                let success = data.status;
                if (success) {
                    Swal.fire({ title: '' + data.messageTitle, text: '' + data.message, type: 'success', allowOutsideClick: false, allowEscapeKey: false }).then(() => {
                        document.location.reload();
                    });
                }
                else {
                    Swal.fire({ title: '' + data.messageTitle, text: '' + data.message, type: 'error' });
                }
            },
            complete: function () {
                FullPageLoaderHide();
            },
            cache: false,
            contentType: false,
            processData: false
        });
    });

    $('.RedeemRewardPointsBtn').click(function (e) {
        e.preventDefault();
        $('#RedeemRewardPointsModal').modal('show');
    });


    $('.ReferAFriendOpenModal').click(function (e) {
        e.preventDefault();
        $('#ReferAFriendModal').modal('show');
    });



    $('.PreviousClaimsBtn').click(function (e) {
        e.preventDefault();
        $('#PreviousClaimsModal').modal('show');
    });

    $('#AccountManager_RedeemPoints').change(function (e) {
        if (parseInt($(this).val()) === 0) {
            $('#RedeemRewardPointsModal_Submit').prop('disabled', true);
            $('#AccountManager_EquivalentAmount').val(0);
        }
        else {
            $('#RedeemRewardPointsModal_Submit').prop('disabled', false);
            let id = $(this).val();
            $('#AccountManager_EquivalentAmount').val(id);
        }
    });
    
    $('#AccountManager_EquivalentAmount').change(function (e) {
        if (parseInt($(this).val()) === 0) {
            $('#RedeemRewardPointsModal_Submit').prop('disabled', true);
            $('#AccountManager_RedeemPoints').val(0);
        }
        else {
            $('#RedeemRewardPointsModal_Submit').prop('disabled', false);
            let id = $(this).val();
            $('#AccountManager_RedeemPoints').val(id);

        }
    });



    $('#RedeemRewardPointsModal_Form').submit(function (e) {
        e.preventDefault();
        let redeemPointsOptionId = $('#AccountManager_RedeemPoints').val();
        let obj = {
            PointsClaimed: parseInt($('#AccountManager_RedeemPoints_option_' + redeemPointsOptionId).data('points')),
            EquivalentAmount: parseFloat($('#AccountManager_RedeemPoints_option_' + redeemPointsOptionId).data('amount')),
            UserId: $('#AccountId').val()
        };
        let url = $('#AccountManagerClaimRewardURL').val();
        FullPageLoaderShow();
        $.ajax({
            url: url,
            type: 'POST',
            dataType: 'JSON',
            data: obj,
            success: function (data) {
                let success = data.status;
                if (success) {
                    Swal.fire({ title: '' + data.messageTitle, text: '' + data.message, type: 'success', allowOutsideClick: false, allowEscapeKey: false }).then(() => {
                        document.location.reload();
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
    });


    $('#ReferAFriendModalForm').submit(function (e) {
        e.preventDefault();
        let obj = {
            EmailReferred: $('#AccountManager_ReferAFriendEmail').val(),
            UserReferralCode: $('#UserReferralCodeValue').text().trim(),
        };
        let url = $('#SendReferralURL').val();
        if (!IsEmptyString(obj.EmailReferred)) {
            let emailRegex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
            if (emailRegex.test(obj.EmailReferred)) {

                AJAXpost(url, obj, true);
            }
            else {
                return Swal.fire({ title: 'Email format incorrect!', text: 'In order to proceed, you will need to provide a valid email address.', type: 'error' });
            }
        }
        else {
            return Swal.fire({ title: 'Form incomplete!', text: 'In order to proceed, you will need to provide a valid email address.', type: 'error' });
        }

    });

    StartDataTables('ClaimedRewardsTable', [[0, 'desc']]);

    $('#PaypalAccountEmailFormShow').click(function (e) {
        e.preventDefault();
        $('#PaypalAccountEmailFormShow').hide(150, function () {
            $('#PaypalAccountEmailFormWrapper').show(150);
        })
    });
    $('#PaypalAccountEmailFormHide').click(function (e) {
        e.preventDefault();
        $('#PaypalAccountEmailFormWrapper').hide(150, function () {
            $('#PaypalAccountEmailFormShow').show(150);
        })
    });


    $('#PaypalAccountEmailForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            Id: $('#AccountId').val(),
            PaypalAccountEmail: $('#PaypalAccountEmailValue').val()
        };

        let url = $('#ConnectUserPaypalAccountURL').val();

        let emailRegex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
        if (emailRegex.test(obj.PaypalAccountEmail)) {

            AJAXpost(url, obj, true);
        }
        else {
            return Swal.fire({ title: 'Email format incorrect!', text: 'In order to proceed, you will need to provide a valid email address.', type: 'error' });
        }
    });


    $('.ShowGmailContactsToReferral').click(function (e) {
        e.preventDefault();
        $('#GmailContactsDisplayWrapper').html("");
        let url = $('#GetGmailContactsURL').val();
        AJAXHTMLretriever(url, {}, function (data) {
            if (IsEmptyString(data)) {
                Swal.fire({ title: 'Could not retrieve Gmail Contacts!', text: 'No answer from controller.', type: 'error' });
            }
            else {
                $('#GmailContactsDisplayWrapper').html(data);
                $('#ReferAFriendModalForm, #ReferAFriendModalByOutlookForm').hide(150, function () {
                    $('#ReferAFriendModalByGmailForm').show(150);
                });
            }
        });
    });

    $('.ShowOutlookContactsToReferral').click(function (e) {
        e.preventDefault();

        $('#OutlookContactsDisplayWrapper').html("");
        let url = $('#GetOutlookContactsURL').val();

        AJAXHTMLretriever(url, {}, function (data) {
            if (IsEmptyString(data)) {
                Swal.fire({ title: 'Could not retrieve Outlook Contacts!', text: 'No answer from controller.', type: 'error' });
            }
            else {
                $('#OutlookContactsDisplayWrapper').html(data);
                $('#ReferAFriendModalForm, #ReferAFriendModalByGmailForm').hide(150, function () {
                    $('#ReferAFriendModalByOutlookForm').show(150);
                });
            }
        });
    });

    $('.CancelGmailContactsToReferral, .CancelOutlookContactsToReferral').click(function (e) {
        e.preventDefault();
        ResetReferralModal();
    });

    $('#ReferAFriendModal').on('hidden.bs.modal', function (e) {
        ResetReferralModal();
    });

    $('#ReferAFriendModalByGmailForm').submit(function (e) {
        e.preventDefault();
        let obj = {
            emails: []
        };
        $('.gmail-contact-checkbox').each(function (index, element) {
            if ($(element).prop('checked')) {
                let contact = $(element).data('email');
                obj.emails.push(contact);
            }
        });
        if (obj.emails.length <= 0) {
            return Swal.fire({ title: 'Form Incomplete!', text: 'You need to select at least one contact.', type: 'error' });
        }
        else {
            let url = $('#SendGmailReferralURL').val();
            AJAXpost(url, obj, true);
            console.log(obj);
        }
        
    });

    $('#ReferAFriendModalByOutlookForm').submit(function (e) {
        e.preventDefault();
        let obj = {
            emails: []
        };
        $('.outlook-contact-checkbox').each(function (index, element) {
            if ($(element).prop('checked')) {
                let contact = $(element).data('email');
                obj.emails.push(contact);
            }
        });
        if (obj.emails.length <= 0) {
            return Swal.fire({ title: 'Form Incomplete!', text: 'You need to select at least one contact.', type: 'error' });
        }
        else {
            let url = $('#SendOutlookReferralURL').val();
            AJAXpost(url, obj, true);
            console.log(obj);
        }
        
    });

    $(document).on('click', '.CancelSubscriptionBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: $(this).data('id')
        };
        let url = $('#CancelUserSubscriptionFrontendURL').val();


        ConfirmBox("Are you sure you want to cancel this subscription?", "", function () {
            AJAXpost(url, obj, true);
        });
    });

    $(document).on('click', '.CancelRenewSubscriptionBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: $(this).data('id')
        };
        let url = $('#CancelUserRenewSubscriptionFrontendURL').val();


        ConfirmBox("Are you sure you want to cancel this subscription auto-renewal?", "", function () {
            AJAXpost(url, obj, true);
        });
    });
});

function ResetReferralModal() {
    $('#GmailContactsDisplayWrapper').html("");
    $('#OutlookContactsDisplayWrapper').html("");
    $('#ReferAFriendModalByGmailForm, #ReferAFriendModalByOutlookForm').hide(150, function () {
        $('#ReferAFriendModalForm').show(150);
    });
}