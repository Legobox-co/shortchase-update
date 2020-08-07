$(document).ready(function () {
    $(document).on('click', '.toggleHiddenArea', function (e) {
        e.preventDefault();

        let isclicked = $(this).data('isclicked');
        if (isclicked) {
            $(this).removeClass("inset-border-thin");
            $(this).addClass("outset-border-thin");
        }
        else {
            $(this).removeClass("outset-border-thin");
            $(this).addClass("inset-border-thin");
        }

        $(this).data('isclicked', !isclicked);
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