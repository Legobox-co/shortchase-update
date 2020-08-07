$(document).ready(function () {
    StartDataTables("UserPayoutsList");

    $('.CheckAgainItemBtn').click(function (e) {
        e.preventDefault();

        let obj = {
            url: $(this).data('link'),
            PayoutId: $(this).data('id'),
        };

        let url = $('#CheckIndividualPaypalPayoutURL').val();
        AJAXpost(url, obj, true);
    });
});