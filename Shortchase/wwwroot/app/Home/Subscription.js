$(document).ready(function () {

    $(document).on('click', '.ProceedToConfirmationBtn', function (e) {
        e.preventDefault();

        let obj = {
            PlanId: null,
            TimeOffset: new Date().getTimezoneOffset()
        };

        $('.SubscriptionPlanPicker').each(function (index, element) {
            if ($(element).prop('checked')) obj.PlanId = $(element).val();
        });

        let url = $('#SubscriptionConfirmationUrl').val();

        loadFilteredPage(url, obj);
    });

});