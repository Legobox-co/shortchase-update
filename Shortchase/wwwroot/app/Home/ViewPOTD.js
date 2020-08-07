$(document).ready(function () {
    $(document).on('click', '.UserInteractionBtn', function (e) {
        e.preventDefault();

        let obj = {
            POTDLiveReportId: $(this).data('report'),
            InteractedById: $(this).data('user'),
            InteractionType: $(this).data('interaction'),
        };

        let url = $('#EnterUserInteractionURL').val();

        let reload = true;
        SilentAJAXpost(url, obj, reload);
    });
});