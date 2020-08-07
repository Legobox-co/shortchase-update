$(document).ready(function () {
    StartDataTables("AddressesList");

    $('.AddAddressModalBtn').click(function (e) {
        e.preventDefault();
        $('#AddAddressModal').modal('show');
    });

    $('#AddAddressModalForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            Location: $('#AddLocationTitle').val(),
            StreetAddress: $('#AddStreetAddress').val(),
            City: $('#AddCity').val(),
            ZipCode: $('#AddZipCode').val(),
            Province: $('#AddProvince').val(),
            CountryId: $('#AddCountry').val(),
            IsPrimary: $('#AddIsPrimary').prop('checked')
        };
        let url = $('#AddAddressModalURL').val();
        let reloadAfterAjax = true;
        AJAXpost(url, obj, reloadAfterAjax);
    });


    $(document).on('click', '.ActivateItemBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: parseInt($(this).data('id')),
            NewStatus: true
        };
        let url = $('#SwitchStatusAddressURL').val();
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
        let url = $('#SwitchStatusAddressURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to deactivate this item?", "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });
    });

    $(document).on('click', '.MakeItPrimaryBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: parseInt($(this).data('id')),
        };
        let url = $('#SwitchPrimaryAddressURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to make this item the primary address?", "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });
    });


    $(document).on('click', '.EditItemBtn', function (e) {
        e.preventDefault();

        let id = parseInt($(this).data('id'));

        $('#EditAddressId').val(id);
        $('#EditLocationTitle').val($('#LocationTitle-' + id).data('value'));
        $('#EditStreetAddress').val($('#StreetAddress-' + id).data('value'));
        $('#EditCity').val($('#City-' + id).data('value'));
        $('#EditZipCode').val($('#ZipCode-' + id).data('value'));
        $('#EditProvince').val($('#Province-' + id).data('value')); 
        $('#EditCountry').selectpicker('val', $('#Country-' + id).data('value'));
        $('#EditIsPrimary').prop('checked', $('#IsPrimary-' + id).data('value'));

        $('#EditAddressModal').modal('show');

    });

    $('#ChangeDisplaySettings').click(function (e) {
        e.preventDefault();
        let obj = {
            Value : $('#DisplayOnSite').prop('checked')
        };

        let url = $('#SwitchDisplayAddressFlagURL').val();
        let reloadAfterAjax = true;
        let message = obj.Value ? "Are you sure you want to display the address information on the website footer?" : "Are you sure you want to hide the address information on the website footer?";
        ConfirmBox(message, "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });
    });

    $('#EditAddressModalForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            Id: $('#EditAddressId').val(),
            Location: $('#EditLocationTitle').val(),
            StreetAddress: $('#EditStreetAddress').val(),
            City: $('#EditCity').val(),
            ZipCode: $('#EditZipCode').val(),
            Province: $('#EditProvince').val(),
            CountryId: $('#EditCountry').val(),
            IsPrimary: $('#EditIsPrimary').prop('checked')
        };
        let url = $('#EditAddressModalURL').val();
        let reloadAfterAjax = true;
        AJAXpost(url, obj, reloadAfterAjax);
    });
});