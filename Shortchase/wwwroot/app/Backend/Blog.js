$(document).ready(function() {
    StartDataTables("BlogList");

    $(document).on('click', '.PublishNowBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: parseInt($(this).data('id')),
        };
        let url = $('#PublishBlogPostNowURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to publish this post now?", "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });

    });

});