$(document).ready(function () {
    StartDataTables("ListingCategoriesList", [[1, 'asc']]);



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


    $('.AddListingCategoryModalBtn').click(function (e) {
        e.preventDefault();
        $('#AddListingCategoryModal').modal('show');
    });


    $('#AddDisplayImage').change(function () {
        var filename = $('#AddDisplayImage').val().split('\\').pop();
        if (filename.trim() === "") filename = 'Category Image';
        $('#Label-AddDisplayImage').html(filename);
    });
    $('#AddMarketplaceImage').change(function () {
        var filename = $('#AddMarketplaceImage').val().split('\\').pop();
        if (filename.trim() === "") filename = 'Marketplace Image';
        $('#Label-AddMarketplaceImage').html(filename);
    });


    $('#AddListingCategoryModalForm').submit(function (e) {
        e.preventDefault();

        let isTimeToSubmit = false;

        let obj = {
            Name: $('#AddName').val(),
            //Description: $('#AddDescription').val(),
        };
        obj.File1 = $('#AddDisplayImage').val();
        obj.File2 = $('#AddMarketplaceImage').val();

        if (IsEmptyString(obj.File1)) {
            return Swal.fire({
                title: 'Form Incomplete!',
                text: 'You need to provide a category image before saving the new category.',
                type: 'error',
                showCancelButton: false
            });
        }
        if (IsEmptyString(obj.File2)) {
            return Swal.fire({
                title: 'Form Incomplete!',
                text: 'You need to provide a marketplace image before saving the new category.',
                type: 'error',
                showCancelButton: false
            });
        }

        SubmitNewCategoryData(obj);


    });



    $(document).on('click', '.ActivateItemBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: parseInt($(this).data('id')),
            NewStatus: true
        };
        let url = $('#SwitchStatusListingCategoryURL').val();
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
        let url = $('#SwitchStatusListingCategoryURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to delete this item?", "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });
    });


    $(document).on('click', '.EditItemBtn', function (e) {
        e.preventDefault();

        let id = parseInt($(this).data('id'));
        let obj = {
            Id: id,
            Name: $('#Name-' + id).data('value'),
            //Description: $('#Description-' + id).data('value'),
            DisplayImage: $('#DisplayImage-' + id).data('value'),
            MarketplaceImage: $('#MarketplaceImage-' + id).data('value')
        };
        $('#EditListingCategoryId').val(obj.Id);
        $('#EditName').val(obj.Name);
        //$('#EditDescription').val(obj.Description);
        $('#EditDisplayImageHolder').attr('src', obj.DisplayImage);
        $('#EditMarketplaceImageHolder').attr('src', obj.MarketplaceImage);

        $('#EditDisplayImageHolder').show(0);
        $('#EditMarketplaceImageHolder').show(0);
        $('#EditListingCategoryModal').modal('show');

    });


    $('#EditDisplayImage').change(function () {
        var filename = $('#EditDisplayImage').val().split('\\').pop();
        if (filename.trim() === "") filename = 'Category Image';
        $('#Label-EditDisplayImage').html(filename);
        $('#EditDisplayImageHolder').hide(150);
    });
    $('#EditMarketplaceImage').change(function () {
        var filename = $('#EditMarketplaceImage').val().split('\\').pop();
        if (filename.trim() === "") filename = 'Marketplace Image';
        $('#Label-EditMarketplaceImage').html(filename);
        $('#EditMarketplaceImageHolder').hide(150);
    });



    $('#EditListingCategoryModalForm').submit(function (e) {
        e.preventDefault();

        let isTimeToSubmit = false;

        let obj = {
            Id: $('#EditListingCategoryId').val(),
            Name: $('#EditName').val(),
            //Description: $('#EditDescription').val(),
        };
        obj.DisplayImage = $('#EditDisplayImage').val();
        obj.MarketplaceImage = $('#EditMarketplaceImage').val();


        SubmitUpdatedCategoryData(obj);

    });


});



function SubmitNewCategoryData(obj) {

    var formData = TransformObjToFormData(obj);

    let url = $('#AddListingCategoryModalURL').val();
    AJAXFilePost(url, formData, true);
}

function SubmitUpdatedCategoryData(obj) {

    //var formData = TransformObjToFormData(obj, true);

    let url = $('#EditListingCategoryModalURL').val();
    AJAXpost(url, obj, true);
}


function TransformObjToFormData(obj, hasId = false) {

    var formData = new FormData();
    if (hasId) {
        formData.append('Post.Id', obj.Id);
    }
    formData.append('Post.Name', obj.Name);
    formData.append('Post.DisplayImage', obj.File1);
    formData.append('Post.MarketplaceImage', obj.File2);
    //formData.append('Post.Description', obj.Description);
    //if (obj.File1 === null) formData.append('Post.DisplayImage', null);
    //else formData.append('Post.DisplayImage', obj.File1.File, obj.File1.Name);
    //if (obj.File2 === null) formData.append('Post.MarketplaceImage', null);
    //else formData.append('Post.MarketplaceImage', obj.File2.File, obj.File2.Name);
    return formData;
}