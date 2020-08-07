$(document).ready(function() {
    StartDataTables("PromotionList");

    $(document).on('click', '.PublishNowBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: parseInt($(this).data('id')),
        };
        let url = $('#PublishPromotionPostNowURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to publish this post now?", "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });

    });

});