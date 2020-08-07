$(document).ready(function () {
    StartDataTables("UserSubscriptionsList");

    $('.AddUserSubscriptionModalBtn').click(function (e) {
        e.preventDefault();
        $('#AddUserSubscriptionModal').modal('show');
    });

    StartEndDate();
    $('#AddSubscriptionId').change(function (e) {
        let id = $(this).val();
        let startDate = $('#AddStartDate').val();
        let months = parseInt($('#SubscriptionOption-' + id).data('months'));

        let endDate = CalculateEndDate(startDate, months);
        $('#AddEndDateWrapper').hide(150, function () {
            $('#AddEndDate').html(endDate);
            $('#AddEndDateWrapper').show(150);
        });
    });






    $('#AddUserSubscriptionModalForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            UserId: $('#AddUserId').val(),
            GiftById: $('#AddGiftById').val(),
            StartDate: $('#AddStartDate').val(),
            SubscriptionId: parseInt($('#AddSubscriptionId').val()),
        };



        let url = $('#AddUserSubscriptionBackendModalURL').val();
        let reloadAfterAjax = true;
        AJAXpost(url, obj, reloadAfterAjax);

    });

    $(document).on('click', '.CancelItemBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: $(this).data('id'),
        };
        let url = $('#CancelUserSubscriptionBackendURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to cancel this item?", "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });

    });


    $(document).on('click', '.DeleteItemBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: $(this).data('id'),
        };
        let url = $('#DeleteUserSubscriptionBackendURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to delete this item?", "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });

    });
    /*

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
   });*/
});


function CalculateEndDate(startDate, numberOfMonths) {
    let StartDate = startDate.split('-');
    StartDate = new Date(parseInt(StartDate[0]), parseInt(StartDate[1]) - 1, parseInt(StartDate[2]));
    var endDate = new Date(StartDate);
    endDate = new Date(endDate.setMonth(endDate.getMonth() + numberOfMonths));

    const monthNames = ["January", "February", "March", "April", "May", "June",
        "July", "August", "September", "October", "November", "December"
    ];

    let endDateStr = monthNames[endDate.getMonth()] + " " + endDate.getDate() + ", " + endDate.getFullYear();
    return endDateStr;
}



function StartEndDate() {
    let id = $('#AddSubscriptionId').val();
    let startDate = $('#AddStartDate').val();
    let months = parseInt($('#SubscriptionOption-' + id).data('months'));

    let endDate = CalculateEndDate(startDate, months);
    $('#AddEndDateWrapper').hide(150, function () {
        $('#AddEndDate').html(endDate);
        $('#AddEndDateWrapper').show(150);
    });
}

