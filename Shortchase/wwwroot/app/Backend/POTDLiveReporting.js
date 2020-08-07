$(document).ready(function () {


    $('#AddReport').summernote();


    $('.AddLiveReportModalBtn').click(function (e) {
        e.preventDefault();
        let dateString = $('#dateValueNow').val();
        $('#AddDateTimeReported').val(dateString.substr(0, dateString.length - 3));
        $('#AddPOTDListingLiveReportModal').modal('show');
    });


    $('#AddPOTDListingLiveReportModalForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            POTDId: $('#POTDId').val(),
            DateTimeReported: $('#AddDateTimeReported').val(),
            ReportedById: $('#LoggedInUserId').val(),
            Report: $('#AddReport').val(),
            TimezoneOffset: new Date().getTimezoneOffset(),
        };
        let url = $('#AddPOTDListingLiveReportItemModalURL').val();
        let reloadAfterAjax = true;

        AJAXpost(url, obj, reloadAfterAjax);
    });




    $(document).on('click', '.DeleteItemBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: ($(this).data('id')),
        };
        let url = $('#DeletePOTDLiveReportingURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to delete this item?", "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });

    });

    $(document).on('click', '.ViewInteractionsBtn', function (e) {
        e.preventDefault();

        let obj = {
            ReportId: ($(this).data('reporting')),
            Type: ($(this).data('type')),
            TimeOffset: new Date().getTimezoneOffset()
        };
        let url = $('#POTDReportingLoadInteractionDataURL').val();

        $('#InteractionTableWrapper').html("");
        $('#InteractionTypeWrapper').html("");
        AJAXHTMLretriever(url, obj, function (data) {
            $('#InteractionTypeWrapper').html(obj.Type);
            $('#InteractionTableWrapper').html(data);
            //StartDataTables("POTDDynamicPredictionsTable", [[0, 'desc']]);
            $('#ViewInteractionsModal').modal('show');
        });


    });

});

