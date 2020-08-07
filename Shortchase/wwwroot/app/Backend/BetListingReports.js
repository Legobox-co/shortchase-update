$(document).ready(function () {

    StartDataTables("ListingReportsList", [[0, 'desc']]);

    $('#AddReportContent').summernote();

    $('.AddListingReportModalBtn').click(function (e) {
        e.preventDefault();
        $('#AddBetListingReportModal').modal('show');
    });



    $('#AddBetListingReportModalForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            ReportContent: $('#AddReportContent').val(),
            ReportedListingId: $('#AddReportedListingId').val(),
            ReportedById: $('#AddReportedById').val(),
            TimezoneOffset: new Date().getTimezoneOffset()
        };

        let url = $('#AddBetListingReportItemModalURL').val();
        let reloadAfterAjax = true;
        AJAXpost(url, obj, reloadAfterAjax);
    });
    

    $(document).on('click', '.ViewReportContentBtn', function (e) {
        e.preventDefault();
        let content = $(this).data('value');
        $('#ViewBetListingReportModalForm').html(content);
        $('#ViewBetListingReportModal').modal('show');
    });


    $(document).on('click', '.ValidateItemBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: parseInt($(this).data('id')),
            NewStatus: true
        };
        let url = $('#SwitchStatusBetListingReportURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to validate this report?", "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });

    });


    $(document).on('click', '.ValidateItemBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: ($(this).data('id')),
            NewStatus: true
        };
        let url = $('#SwitchStatusBetListingReportURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to validate this report?", "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });

    });


    $(document).on('click', '.InvalidateItemBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: ($(this).data('id')),
            NewStatus: false
        };
        let url = $('#SwitchStatusBetListingReportURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to invalidate this report?", "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });

    });

    $(document).on('click', '.DeleteItemBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: ($(this).data('id'))
        };
        let url = $('#DeleteBetListingReportURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to delete this report?", "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });

    });
});

