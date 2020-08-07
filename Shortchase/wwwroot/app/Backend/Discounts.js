$(document).ready(function () {
    StartDataTables("DiscountsList", [[0, 'desc']]);
    
    $('.AddDiscountModalBtn').click(function (e) {
        e.preventDefault();
        $('#AddDiscountModal').modal('show');
    });

    $('#AddDiscountModalForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            EquivalentAmount: $('#AddEquivalentAmount').val(),
            UserId: $('#AddUserId').val(),
        };
        let url = $('#AddDiscountModalURL').val();
        let reloadAfterAjax = true;
        AJAXpost(url, obj, reloadAfterAjax);
    });

    /*
    $(document).on('click', '.ActivateItemBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: parseInt($(this).data('id')),
            NewStatus: true
        };
        let url = $('#SwitchStatusMarketURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to activate this item?", "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });
        
    });
    

    $(document).on('click', '.DeactivateItemBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: parseInt($(this).data('id')),
            NewStatus: false
        };
        let url = $('#SwitchStatusMarketURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to deactivate this item?", "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });
    });

    $(document).on('click', '.EditItemBtn', function (e) {
        e.preventDefault();

        let id = parseInt($(this).data('id'));

        $('#EditMarketId').val(id);
        $('#EditName').val($('#Name-' + id).data('value'));
        $('#EditCategory').selectpicker('val', $('#CategoryId-' + id).data('value'));

        $('#EditMarketModal').modal('show');

    });

    $('#EditMarketModalForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            Id: $('#EditMarketId').val(),
            Name: $('#EditName').val(),
            CategoryId: $('#EditCategory').val(),
        };
        let url = $('#EditMarketModalURL').val();
        let reloadAfterAjax = true;
        AJAXpost(url, obj, reloadAfterAjax);
    });*/
});