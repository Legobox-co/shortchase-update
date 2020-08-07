$(document).ready(function () {
    StartDataTables("TipsList", [[1, 'asc']]);

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


    $('.AddTipModalBtn').click(function (e) {
        e.preventDefault();
        $('#AddTipModal').modal('show');
    });

    $('#AddTipModalForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            Name: $('#AddName').val(),
            Description: $('#AddDescription').val(),
            MarketId: $('#AddMarket').val(),
        };
        let url = $('#AddTipModalURL').val();
        let reloadAfterAjax = true;
        AJAXpost(url, obj, reloadAfterAjax);
    });


    $(document).on('click', '.ActivateItemBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: parseInt($(this).data('id')),
            NewStatus: true
        };
        let url = $('#SwitchStatusTipURL').val();
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
        let url = $('#SwitchStatusTipURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to delete this item?", "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });
    });



    $(document).on('click', '.EditItemBtn', function (e) {
        e.preventDefault();

        let id = parseInt($(this).data('id'));

        $('#EditTipId').val(id);
        $('#EditName').val($('#Name-' + id).data('value'));
        $('#EditDescription').val($('#Description-' + id).data('value'));
        $('#EditMarket').selectpicker('val', $('#Market-' + id).data('value'));

        $('#EditTipModal').modal('show');

    });

    $('#EditTipModalForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            Id: $('#EditTipId').val(),
            Name: $('#EditName').val(),
            MarketId: $('#EditMarket').val(),
            Description: $('#EditDescription').val(),
        };
        let url = $('#EditTipModalURL').val();
        let reloadAfterAjax = true;
        AJAXpost(url, obj, reloadAfterAjax);
    });
});