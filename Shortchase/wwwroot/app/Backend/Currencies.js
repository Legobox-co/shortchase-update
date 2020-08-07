$(document).ready(function () {
    StartDataTables("CurrenciesList", [[1, 'asc']]);

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


    $('.AddCurrencyModalBtn').click(function (e) {
        e.preventDefault();
        $('#AddCurrencyModal').modal('show');
    });
    $('#AddCurrencyModalForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            Name: $('#AddName').val(),
            Acronym: $('#AddAcronym').val(),
        };
        let url = $('#AddCurrencyModalURL').val();
        let reloadAfterAjax = true;
        AJAXpost(url, obj, reloadAfterAjax);
    });



    $(document).on('click', '.DeleteItemBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: ($(this).data('id')),
        };
        let url = $('#DeleteCurrencyURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to delete this item?", "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });
    });



    $(document).on('click', '.ActivateItemBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: ($(this).data('id')),
        };
        let url = $('#ActivateCurrencyURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to activate this item?", "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });
    });


    $(document).on('click', '.EditItemBtn', function (e) {
        e.preventDefault();

        let id = ($(this).data('id'));

        $('#EditCurrencyId').val(id);
        $('#EditName').val($('#Name-' + id).data('value'));
        $('#EditAcronym').val($('#Acronym-' + id).data('value'));

        $('#EditCurrencyModal').modal('show');

    });


    $('#EditCurrencyModalForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            Id: $('#EditCurrencyId').val(),
            Name: $('#EditName').val(),
            Acronym: $('#EditAcronym').val(),
        };
        let url = $('#EditCurrencyModalURL').val();
        let reloadAfterAjax = true;
        AJAXpost(url, obj, reloadAfterAjax);
    });
});