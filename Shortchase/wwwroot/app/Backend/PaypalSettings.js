$(document).ready(function () {
    StartDataTables("PaypalSettingssList");

    $('.AddPaypalSettingsModalBtn').click(function (e) {
        e.preventDefault();
        $('#AddPaypalSettingsModal').modal('show');
    });

    $('#AddPaypalSettingsModalForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            ClientID: $('#AddClientID').val(),
            Secret: $('#AddSecret').val(),
            Name: $('#AddName').val(),
            IsDefault: $('#AddIsDefault').prop('checked'),
        };
        let url = $('#AddPaypalSettingsModalURL').val();
        let reloadAfterAjax = true;
        AJAXpost(url, obj, reloadAfterAjax);
    });


    $(document).on('click', '.ActivateItemBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: parseInt($(this).data('id')),
            NewStatus: true
        };
        let url = $('#SwitchStatusPaypalSettingsURL').val();
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
        let url = $('#SwitchStatusPaypalSettingsURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to deactivate this item?", "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });
    });
    $(document).on('click', '.EditItemBtn', function (e) {
        e.preventDefault();

        let id = parseInt($(this).data('id'));

        $('#EditPaypalSettingsId').val(id);
        $('#EditName').val($('#Name-' + id).data('value'));
        $('#EditClientID').val($('#ClientID-' + id).data('value'));
        $('#EditSecret').val($('#Secret-' + id).data('value'));
        $('#EditIsDefault').prop('checked', $('#Default-' + id).data('value'));

        $('#EditPaypalSettingsModal').modal('show');

    });

    $('#EditPaypalSettingsModalForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            Id: $('#EditPaypalSettingsId').val(),
            ClientID: $('#EditClientID').val(),
            Name: $('#EditName').val(),
            Secret: $('#EditSecret').val(),
            IsDefault: $('#EditIsDefault').prop('checked'),
        };
        let url = $('#EditPaypalSettingsModalURL').val();
        let reloadAfterAjax = true;
        AJAXpost(url, obj, reloadAfterAjax);
    });
});