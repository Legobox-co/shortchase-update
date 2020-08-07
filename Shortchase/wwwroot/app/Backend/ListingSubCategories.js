$(document).ready(function () {
    StartDataTables("ListingSubCategoriesList");

    $('.AddListingSubCategoryModalBtn').click(function (e) {
        e.preventDefault();
        $('#AddListingSubCategoryModal').modal('show');
    });



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

    $('#AddListingSubCategoryModalForm').submit(function (e) {
        e.preventDefault();


        let obj = {
            Name: $('#AddName').val(),
            Description: "",//$('#AddDescription').val(),
            CategoryId: $('#AddCategoryId').val(),
        };

        let url = $('#AddListingSubCategoryModalURL').val();
        let reloadAfterAjax = true;
        AJAXpost(url, obj, reloadAfterAjax);

    });




    $(document).on('click', '.ActivateItemBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: parseInt($(this).data('id')),
            NewStatus: true
        };
        let url = $('#SwitchStatusListingSubCategoryURL').val();
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
        let url = $('#SwitchStatusListingSubCategoryURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to delete this item?", "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });
    });


    $(document).on('click', '.EditItemBtn', function (e) {
        e.preventDefault();

        let id = parseInt($(this).data('id'));
        $('#EditListingSubCategoryId').val(id);
        $('#EditName').val($('#Name-' + id).data('value'));
        //$('#EditDescription').val($('#Description-' + id).data('value'));
        $('#EditCategoryId').selectpicker('val', $('#Category-' + id).data('value'));

        $('#EditListingSubCategoryModal').modal('show');

    });



    $('#EditListingSubCategoryModalForm').submit(function (e) {
        e.preventDefault();


        let obj = {
            Id: $('#EditListingSubCategoryId').val(),
            Name: $('#EditName').val(),
            Description: "",//$('#EditDescription').val(),
            CategoryId: $('#EditCategoryId').val(),
        };

        let url = $('#EditListingSubCategoryModalURL').val();
        let reloadAfterAjax = true;
        AJAXpost(url, obj, reloadAfterAjax);

    });
});
