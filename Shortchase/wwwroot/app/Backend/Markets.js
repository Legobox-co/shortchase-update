$(document).ready(function () {
    StartDataTables("MarketsList", [[1, 'asc']]);


    $(document).on('click', '.batchaction', function (e) {
        e.preventDefault();

        let obj = {
            Ids: GetAllSelectedRowsData()
        };
        let isActionPermitted = BatchActionChecker2(obj.Ids.length);
        if (isActionPermitted) {
            let actionType = $(this).data('actiontype');
            let url = $(this).data('action');
            switch (actionType) {
                case "delete":
                    let reloadAfterAjax = true;
                    return ConfirmBox("Are you sure you want to delete these items?", "", function () {
                        AJAXpost(url, obj, reloadAfterAjax);
                    });

                default:
                    return Swal.fire({ title: 'No elements selected!', html: "You need to select at least one element for batch actions!", type: 'error' });
            }
        }
        else {
            return Swal.fire({ title: 'No elements selected!', html: "You need to select at least one element for batch actions!", type: 'error' });
        }
    });

    $('.AddMarketModalBtn').click(function (e) {
        e.preventDefault();
        $('#AddMarketModal').modal('show');
    });

    $('#AddMarketModalForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            Name: $('#AddName').val(),
            CategoryId: $('#AddCategory').val(),
        };
        let url = $('#AddMarketModalURL').val();
        let reloadAfterAjax = true;
        AJAXpost(url, obj, reloadAfterAjax);
    });


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
        ConfirmBox("Are you sure you want to delete this item?", "", function () {
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
    });
});