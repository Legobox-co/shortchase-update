﻿$(document).ready(function() {
    StartDataTables("AdministratorsList", [[1, 'asc']]);

    $(document).on('click', '.batchaction', function (e) {
        e.preventDefault();

        let elementClicked = this;
        let obj = {
            Ids: GetAllSelectedRowsData()
        };
        let isActionPermitted = BatchActionChecker2(obj.Ids.length);
        if (isActionPermitted) {
            let actionType = $(this).data('actiontype');
            let url = $(this).data('action');
            let reloadAfterAjax = true;
            switch (actionType) {
                case "delete":
                    return ConfirmBox("Are you sure you want to delete these items?", "", function () {
                        AJAXpost(url, obj, reloadAfterAjax);
                    });

                case "switchstatus":
                    obj.Status = $(elementClicked).data('status');
                    let correctWord = "";
                    if (obj.Status) {
                        correctWord = "activate";
                    }
                    else correctWord = "deactivate";
                    return ConfirmBox("Are you sure you want to " + correctWord + " these items?", "", function () {
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

    $('.AddAdministratorsModalBtn').click(function(e) {
        e.preventDefault();
        $('#AddAdministratorModal').modal('show');
    });


    $('#AddCountry').change(function() {
        let id = $(this).val();

        $('#AddPhoneCode').val("+" + $('#AddCountryOption-' + id).data('code'));
    });


    $('#EditCountry').change(function() {
        let id = $(this).val();

        $('#EditPhoneCode').val("+" + $('#EditCountryOption-' + id).data('code'));
    });

    $('#AddAdministratorModalForm').submit(function(e) {
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
            role: $('#AddRole').val()
        };

        if (obj.Password === obj.RepeatPassword) {

            //let url = $('#AddAdministratorModalURL').val();
            let url = '/Backend/AddAdministrator?FirstName=' + obj.FirstName + '&LastName=' + obj.LastName + '&Email=' + obj.Email + '&Password=' + obj.Password + '&RepeatPassword=' + obj.RepeatPassword + '&role=' + obj.role + '&Country=' + obj.Country+ '';
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

    $(document).on('click', '.ActivateItemBtn', function(e) {
        e.preventDefault();

        let obj = {
            Id: $(this).data('id'),
            NewStatus: true
        };
        let url = $('#SwitchStatusAdministratorURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to activate this item?", "", function() {
            AJAXpost(url, obj, reloadAfterAjax);
        });

    });


    $(document).on('click', '.DeactivateItemBtn', function(e) {
        e.preventDefault();

        let obj = {
            Id: $(this).data('id'),
            NewStatus: false
        };
        let url = $('#SwitchStatusAdministratorURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to deactivate this item?", "", function() {
            AJAXpost(url, obj, reloadAfterAjax);
        });
    });



    $(document).on('click', '.EditItemBtn', function(e) {
        e.preventDefault();

        let id = $(this).data('id');
        let countryId = $(this).data('country-id');


        $('#EditAdministratorId').val(id);
        $('#EditFirstName').val($('#FirstName-' + id).data('value'));
        $('#EditLastName').val($('#LastName-' + id).data('value'));
        $('#EditEmail').val($('#Email-' + id).data('value'));
        $('#EditCountry').selectpicker('val', countryId);
        $('#EditPhoneCode').val("+" + $('#EditCountryOption-' + countryId).data('code'));
        $('#EditPhoneNumber').val($('#PhoneNumber-' + id).data('value'));

        $('#EditAdministratorModal').modal('show');

    });


    $('#EditAdministratorModalForm').submit(function(e) {
        e.preventDefault();

        let obj = {
            Id: $('#EditAdministratorId').val(),
            FirstName: $('#EditFirstName').val(),
            LastName: $('#EditLastName').val(),
            Email: $('#EditEmail').val(),
            Country: $('#EditCountry').val(),
            PhoneCode: $('#EditPhoneCode').val(),
            PhoneNumber: $('#EditPhoneNumber').val(),
            Password: $('#EditPassword').val(),
            RepeatPassword: $('#EditRepeatPassword').val(),
            role: $('#EditRole').val()
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


            let url = $('#EditAdministratorModalURL').val();
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