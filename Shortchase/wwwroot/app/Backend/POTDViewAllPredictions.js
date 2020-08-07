$(document).ready(function () {
    StartDataTables("POTDDynamicPredictionsTable", [[0, 'desc']]);

    $(document).on('click', '.PredictionValidationBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: ($(this).data('id')),
            Type: $(this).data('type'),
        };

        let url = $('#POTDPredictionValidationURL').val();
        let reloadAfterAjax = true;
        let messageType = obj.Type ? "validate" : "invalidate";
        let message = "Are you sure you want to " + messageType + " this prediction?";
        ConfirmBox(message, "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });

    });

});

