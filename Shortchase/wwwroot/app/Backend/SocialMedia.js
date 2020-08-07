$(document).ready(function () {

    const table = StartDataTables("SocialMediaList", [[1, "asc"]]);



    $('.AddSocialMediaModalBtn').click(function (e) {
        e.preventDefault();
        $('#AddSocialMediaModal').modal('show');
    });

    $('#AddSocialMediaModalForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            Name: $('#AddName').val(),
            Link: $('#AddLink').val(),
            Icon: $('#AddIcon').val(),
        };
        let url = $('#AddSocialMediaModalURL').val();
        let reloadAfterAjax = true;
        AJAXpost(url, obj, reloadAfterAjax);
    });

    $('#AddIcon').on('keyup paste input change',function () {
        let value = $(this).val();
        $('#addIconPreview').attr("class", "");
        if (!value || value === "" || value.trim() === "") $('#addIconPreview').attr("class", "fab fa-facebook-f fa-2x");
        else $('#addIconPreview').attr("class", value + " fa-2x");
    });




    $(document).on('click', '.ActivateItemBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: parseInt($(this).data('id')),
            NewStatus: true
        };
        let url = $('#SwitchStatusSocialMediaURL').val();
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
        let url = $('#SwitchStatusSocialMediaURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to deactivate this item?", "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });
    });



    $(document).on('click', '.EditItemBtn', function (e) {
        e.preventDefault();

        let id = parseInt($(this).data('id'));

        $('#EditSocialMediaId').val(id);
        $('#EditName').val($('#Name-' + id).data('value'));
        $('#EditIcon').val($('#Icon-' + id).data('value'));
        $('#EditLink').val($('#Link-' + id).data('value'));
        $('#editIconPreview').attr("class", $('#Icon-' + id).data('value') + " fa-2x");

        $('#EditSocialMediaModal').modal('show');

    });


    $('#EditIcon').on('keyup paste input change', function () {
        let value = $(this).val();
        $('#editIconPreview').attr("class", "");
        if (!value || value === "" || value.trim() === "") $('#editIconPreview').attr("class", "fab fa-facebook-f fa-2x");
        else $('#editIconPreview').attr("class", value + " fa-2x");
    });


    $('#EditSocialMediaModalForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            Id: $('#EditSocialMediaId').val(),
            Name: $('#EditName').val(),
            Icon: $('#EditIcon').val(),
            Link: $('#EditLink').val(),
        };
        let url = $('#EditSocialMediaModalURL').val();
        let reloadAfterAjax = true;
        AJAXpost(url, obj, reloadAfterAjax);
    });
});