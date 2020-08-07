$(document).ready(function () {
   
    $('.AddMediaFolderModalBtn').click(function (e) {
        e.preventDefault();
        $('#AddMediaFolderModal').modal('show');
    });

    $('.AddMediaModalBtn').click(function (e) {
        e.preventDefault();
        $('#AddMediaFileModal').modal('show');
    });

    $('.MoveMediaFormBtn').click(function (e) {
        e.preventDefault();
        $('#MoveFileForm').toggle(150);
    });
    $('#EditMediaFileModal').on('hidden.bs.modal', function (e) {
        $('#MoveFileForm').hide(150);
    });

    $('.EditMediaFolderModalBtn').click(function (e) {
        e.preventDefault();
        let id = $(this).data('id');
        let name = $(this).data('name');

        $('#EditName').val(name);
        $('#EditFolderId').val(id);

        $('#EditMediaFolderModal').modal('show');
    });
    

    $('.OpenEditFileModal').click(function (e) {
        e.preventDefault();
        let id = $(this).data('id');
        let title = $(this).data('title');
        let lastUpdated = $(this).data('lastupdated');
        let size = $(this).data('size');
        let media = $(this).data('media');
        let url = $(this).data('url');
        let width = $(this).data('width');
        let height = $(this).data('height');

        $('#EditFileId').val(id);
        $('#FileIdDownload').val(id);
        $('#EditFileTitle').val(title);
        $('#EditFileLastUpdated').html(lastUpdated);
        $('#EditFileWidth').html(width);
        $('#EditFileHeight').html(height);
        $('#EditFileSize').html(size);
        $('#EditFilePreviewMedia').attr('src',media);
        $('#EditFileURL').attr('href', url);

        $('#EditMediaFileModal').modal('show');
    });

    $('#AddMediaFolderModalForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            Name: $('#AddName').val(),
            ParentFolderId: $('#ParentFolderId').val(),
        };
        let url = $('#AddMediaFolderModalURL').val();
        let reloadAfterAjax = true;
        AJAXpost(url, obj, reloadAfterAjax);
    });


    $('#EditMediaFolderModalForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            Name: $('#EditName').val(),
            Id: $('#EditFolderId').val(),
        };
        let url = $('#EditMediaFolderModalURL').val();
        let reloadAfterAjax = true;
        AJAXpost(url, obj, reloadAfterAjax);
    });

    $('.DeleteMediaFolderModalBtn').click(function (e) {
        e.preventDefault();

        let obj = {
            Id: $(this).data('id'),
        };
        let url = $('#DeleteMediaFolderURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to delete this folder?", "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });
    });


    $('#AddFileMedia').change(function () {
        var filename = $('#AddFileMedia').val().split('\\').pop();
        if (filename.trim() === "") filename = 'Select media';
        $('#AddFileMediaLabel').html(filename);
    });


    $('#EditMediaFileModalForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            Title: $('#EditFileTitle').val(),
            Id: $('#EditFileId').val(),
        };
        let url = $('#EditMediaFileModalURL').val();
        let reloadAfterAjax = true;
        AJAXpost(url, obj, reloadAfterAjax);
    });


    $('.DeleteMediaFIleBtn').click(function (e) {
        e.preventDefault();

        let obj = {
            Id: $('#EditFileId').val(),
        };
        let url = $('#DeleteMediaFileURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to delete this file?", "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });
    });


    $('.CropMediaBtn').click(function (e) {
        e.preventDefault();

        let obj = {
            Media: $('#EditFileId').val(),
            TimezoneOffset: new Date().getTimezoneOffset()
        };
        let url = $('#CropMediaURL').val();
        loadFilteredPage(url, obj);
    });

    $('#AddMediaFileModalForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            Title: $('#AddFileTitle').val(),
            FolderId: IsEmptyString($('#ParentFolderId').val()) ? null : $('#ParentFolderId').val(),
        };
        obj.File = null;

        var fileInput = document.getElementById('AddFileMedia');
        if (fileInput.files.length > 0) {
            obj.File = {
                File: fileInput.files[0],
                Name: fileInput.files[0].name
            };
        }


        if (!obj.File && obj.File === null) {
            return Swal.fire({
                title: 'Form Incomplete!',
                text: 'You need to provide file.',
                type: 'error',
                showCancelButton: false
            });
        }
        else {
            var formData = new FormData();

            formData.append('Item.Title', obj.Title);
            formData.append('Item.FolderId', obj.FolderId);
            if (obj.File === null) formData.append('Item.File', null);
            else formData.append('Item.File', obj.File.File, obj.File.Name);

            let url = $('#AddMediaFileModalURL').val();
            AJAXFilePost(url, formData, true);
        }
        
    });


    $('.FinishMoveMediaFormBtn').click(function (e) {
        e.preventDefault();

        let obj = {
            Id: $('#EditFileId').val(),
            FolderToId: $('#MoveToId').val(),
        };

        if (obj.FolderToId === "default") obj.FolderToId = null;
        let url = $('#MoveMediaFileModalURL').val();
        AJAXpost(url, obj, true);
        
    });


    /*
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
    });*/
});