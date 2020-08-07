$(document).ready(function () {
    StartDataTables("BookmakersList", [[1,'asc']]);

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

    $('.AddBookmakerModalBtn').click(function (e) {
        e.preventDefault();
        $('#AddBookmakerModal').modal('show');
    });

    $('#AddBookmakerModalForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            Description: $('#AddDescription').val(),
        };
        let url = $('#AddBookmakerModalURL').val();
        let reloadAfterAjax = true;
        AJAXpost(url, obj, reloadAfterAjax);
    });


    $(document).on('click', '.ActivateItemBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: parseInt($(this).data('id')),
            NewStatus: true
        };
        let url = $('#SwitchStatusBookmakerURL').val();
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
        let url = $('#SwitchStatusBookmakerURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to delete this item?", "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });
    });

    $(document).on('click', '.EditItemBtn', function (e) {
        e.preventDefault();

        let id = parseInt($(this).data('id'));

        $('#EditBookmakerId').val(id);
        $('#EditDescription').val($('#Description-' + id).data('value'));

        $('#EditBookmakerModal').modal('show');

    });

    $('#EditBookmakerModalForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            Id: $('#EditBookmakerId').val(),
            Description: $('#EditDescription').val(),
        };
        let url = $('#EditBookmakerModalURL').val();
        let reloadAfterAjax = true;
        AJAXpost(url, obj, reloadAfterAjax);
    });
});