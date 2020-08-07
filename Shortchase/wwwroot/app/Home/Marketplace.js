$(document).ready(function () {

    $(document).on('click', '.marketplace-item-link', function (e) {
        e.preventDefault();
        let filters = {
            Category: $(this).data('id'),
            TimeOffset: new Date().getTimezoneOffset()
        };
        const url = $('#SearchResultsUrl').val();

        loadFilteredPage(url, filters);
    });

    $(".marketplace-hr-hover").on('mouseenter', function () {
        //alert('i am here');
        var line = $(this).find(".marketplace-hr-hover-change-color");
        line.css({ "width": "80px" });
        line.css({ "border-color": "#FFBE0C" })
    });
    $(".marketplace-hr-hover").on('mouseleave', function () {
        //alert('i am here');
        var line = $(this).find(".marketplace-hr-hover-change-color");
        line.css({ "width": "38px" });
        line.css({ "border-color": "#31303852" })
    })


});