$(document).ready(function () {

    $(document).on('click', '.RemoveItemFromCartBtn', function (e) {
        e.preventDefault();
        let obj = {
            Id: $(this).data('item'),
        };
        let url = $('#RemoveItemCartUrl').val();

        ConfirmBox("Are you sure you want to remove this item from your cart?", "", function () {
            AJAXpost(url, obj, true);
        });
       
    });


    $(document).on('click', '.CleanCartBtn', function (e) {
        e.preventDefault();
        let obj = {};
        let url = $('#CleanCartUrl').val();

        ConfirmBox("Are you sure you want to remove all items from your cart?", "", function () {
            AJAXpost(url, obj, true);
        });
    });
});