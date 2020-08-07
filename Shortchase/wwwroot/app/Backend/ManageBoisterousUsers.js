$(document).ready(function() {
    StartDataTables("UsersList", [[1, 'asc']]);

    $(document).on('click', '.batchaction', function (e) {
        e.preventDefault();

        let elementClicked = this;
        let obj = {
            Ids: null
        };

        if ($(this).data('actiontype') === "switchsubscriptionstatus") {
            obj.Ids = GetAllSelectedRowsSubscriptionData();
        }
        else {
            obj.Ids = GetAllSelectedRowsData();
        }
        let isActionPermitted = BatchActionChecker2(obj.Ids.length);
        if (isActionPermitted) {
            let actionType = $(this).data('actiontype');
            let url = $(this).data('action');
            let correctWord = "";
            let reloadAfterAjax = true;
            switch (actionType) {
                case "delete":
                    return ConfirmBox("Are you sure you want to delete these items?", "", function () {
                        AJAXpost(url, obj, reloadAfterAjax);
                    });

                case "switchstatus":
                    obj.Status = $(elementClicked).data('status');
                    if (obj.Status) {
                        correctWord = "activate";
                    }
                    else correctWord = "deactivate";
                    return ConfirmBox("Are you sure you want to " + correctWord + " these items?", "", function () {
                        AJAXpost(url, obj, reloadAfterAjax);
                    });

                case "switchsubscriptionstatus":
                    obj.Status = $(elementClicked).data('status');
                    if (obj.Status) {
                        correctWord = "activate";
                    }
                    else correctWord = "cancel";
                    return ConfirmBox("Are you sure you want to " + correctWord + " these subscriptions Auto Renewal?", "", function () {
                        AJAXpost(url, obj, reloadAfterAjax);
                    });

                default:
                    return Swal.fire({ title: 'No elements selected!', html: "You need to select at least one element for batch actions!", type: 'error' });
            }
        }
        else {
            return Swal.fire({ title: 'No elements selected!', html: "You need to select at least one element for batch actions!", type: 'error' });
        }
    });

    $('.AddUsersModalBtn').click(function(e) {
        e.preventDefault();
        $('#AddUserModal').modal('show');
    });

    $('#AddCountry').change(function() {
        let id = $(this).val();

        $('#AddPhoneCode').val("+" + $('#AddCountryOption-' + id).data('code'));
    });


    $('#EditCountry').change(function() {
        let id = $(this).val();

        $('#EditPhoneCode').val("+" + $('#EditCountryOption-' + id).data('code'));
    });

    $('#AddUserModalForm').submit(function(e) {
        e.preventDefault();

        let obj = {
            FirstName: $('#AddFirstName').val(),
            LastName: $('#AddLastName').val(),
            Email: $('#AddEmail').val(),
            Country: $('#AddCountry').val(),
            PhoneCode: $('#AddPhoneCode').val(),
            PhoneNumber: $('#AddPhoneNumber').val(),
            Password: $('#AddPassword').val(),
            RepeatPassword: $('#AddRepeatPassword').val(),
        };

        if (obj.Password === obj.RepeatPassword) {

            let url = $('#AddUserModalURL').val();
            let reloadAfterAjax = true;
            AJAXpost(url, obj, reloadAfterAjax);

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


    $(document).on('click', '.ActivateItemBtn', function(e) {
        e.preventDefault();

        let obj = {
            Id: $(this).data('id'),
            NewStatus: true
        };
        let url = $('#SwitchStatusUsersURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to activate this item?", "", function() {
            AJAXpost(url, obj, reloadAfterAjax);
        });

    });

    $(document).on('click', '.CancelAutoRenewalBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: $(this).data('id'),
        };
        let url = $('#CancelUserRenewSubscriptionURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to cancel this subscription auto-renewal?", "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });

    });

    $(document).on('click', '.UnCancelAutoRenewalBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: $(this).data('id'),
        };
        let url = $('#UnCancelUserRenewSubscriptionURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to activate this subscription auto-renewal?", "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });

    });


    $(document).on('click', '.DeactivateItemBtn', function(e) {
        e.preventDefault();

        let obj = {
            Id: $(this).data('id'),
            NewStatus: false
        };
        let url = $('#SwitchStatusUsersURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to deactivate this item?", "", function() {
            AJAXpost(url, obj, reloadAfterAjax);
        });
    });


    $(document).on('click', '.DeleteItemBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: $(this).data('id'),
        };
        let url = $('#SoftDeleteUsersURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to delete this item?", "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });
    });



    $(document).on('click', '.EditItemBtn', function(e) {
        e.preventDefault();

        let id = $(this).data('id');
        let countryId = $(this).data('country-id');


        $('#EditUserId').val(id);
        $('#EditFirstName').val($('#FirstName-' + id).data('value'));
        $('#EditLastName').val($('#LastName-' + id).data('value'));
        $('#EditEmail').val($('#Email-' + id).data('value'));
        $('#EditCountry').selectpicker('val', countryId);
        $('#EditPhoneCode').val("+" + $('#EditCountryOption-' + countryId).data('code'));
        $('#EditPhoneNumber').val($('#PhoneNumber-' + id).data('value'));

        $('#EditUserModal').modal('show');

    });


    $('#EditUserModalForm').submit(function(e) {
        e.preventDefault();

        let obj = {
            Id: $('#EditUserId').val(),
            FirstName: $('#EditFirstName').val(),
            LastName: $('#EditLastName').val(),
            Email: $('#EditEmail').val(),
            Country: $('#EditCountry').val(),
            PhoneCode: $('#EditPhoneCode').val(),
            PhoneNumber: $('#EditPhoneNumber').val(),
            Password: $('#EditPassword').val(),
            RepeatPassword: $('#EditRepeatPassword').val(),
        };

        if (IsEmptyString(obj.Password) && IsEmptyString(obj.RepeatPassword)) obj.ChangePassword = false;
        else obj.ChangePassword = true;

        let timeToSubmitTheForm = false;

        if (!obj.ChangePassword) {
            timeToSubmitTheForm = true;
        }
        else {
            if (obj.Password === obj.RepeatPassword) {
                timeToSubmitTheForm = true;
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

        if (timeToSubmitTheForm) {


            let url = $('#EditUserModalURL').val();
            let reloadAfterAjax = true;
            AJAXpost(url, obj, reloadAfterAjax);

        }
        else {
            Swal.fire(
                "Incomplete form",
                'Fill all the information and then try again.',
                'error'
            );
            return false;
        }
    });
});