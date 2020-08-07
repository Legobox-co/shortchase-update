$(document).ready(function () {

    $(document).on('click', '.toggleHiddenAreaOnClick', function (e) {
        e.preventDefault();
        let toggleId = $(this).data('area');
        let iconClass = $(this).data('icon');
        $('.' + iconClass).each(function (index, element) {
            if ($(element).hasClass("fa-chevron-down")) {
                $(element).removeClass('fa-chevron-down');
                $(element).addClass('fa-chevron-up');
            }
            else {
                $(element).removeClass('fa-chevron-up');
                $(element).addClass('fa-chevron-down');
            }
        });
        $('#' + toggleId).toggle(150);
    });

});