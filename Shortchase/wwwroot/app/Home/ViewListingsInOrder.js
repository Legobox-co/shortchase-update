$(document).ready(function () {
    $('.GoToCurrentItemBtn').click(function (e) {
        e.preventDefault();

        let obj = {
            Order: $('#OrderParam').val(),
            CurrentItem: $(this).data('id')
        };

        let url = $('#ViewListingsInOrderURL').val();

        loadFilteredPage(url, obj);
    });
});

