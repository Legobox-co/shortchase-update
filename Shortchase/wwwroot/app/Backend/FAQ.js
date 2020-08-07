$(document).ready(function () {
    StartDataTables("FAQList");

    $('#AddAnswer').summernote();
    

    $('.AddFAQModalBtn').click(function (e) {
        e.preventDefault();
        $('#AddFAQModal').modal('show');
    });

    
    $('#AddFAQModalForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            Question: $('#AddQuestion').val(),
            Answer: $('#AddAnswer').val(),
            Type: $('#AddType').val(),
        };
        let url = $('#AddFAQItemModalURL').val();
        let reloadAfterAjax = true;
        AJAXpost(url, obj, reloadAfterAjax);
    });

    
    $(document).on('click', '.ActivateItemBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: parseInt($(this).data('id')),
            NewStatus: true
        };
        let url = $('#SwitchStatusFAQItemURL').val();
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
        let url = $('#SwitchStatusFAQItemURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to delete this item?", "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });
    });

    $(document).on('click', '.ViewAnswerBtn', function(e) {
        e.preventDefault();

        let id = parseInt($(this).data('id'));

        $('#ViewAnswerAnswer').html($('#Answer-' + id).data('value'));
        $('#ViewAnswerQuestion').html($('#Question-' + id).data('value'));

        $('#ViewAnswerFAQModal').modal('show');

    });

    $(document).on('click', '.EditItemBtn', function (e) {
        e.preventDefault();

        let id = parseInt($(this).data('id'));

        $('#EditFAQId').val(id);
        $('#EditQuestion').val($('#Question-' + id).data('value'));
        $('#EditAnswer').html($('#Answer-' + id).data('value'));
        $('#EditAnswer').summernote();
        $('#EditType').selectpicker('val', $('#Type-' + id).data('value'));

        $('#EditFAQModal').modal('show');
    });
    
    $('#EditFAQModal').on('hidden.bs.modal', function(e) {
        $('#EditAnswer').summernote('destroy');
    });


    $('#EditFAQModalForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            Id: $('#EditFAQId').val(),
            Question: $('#EditQuestion').val(),
            Answer: $('#EditAnswer').val(),
            Type: $('#EditType').val(),
        };
        let url = $('#UpdateFAQItemModalURL').val();
        let reloadAfterAjax = true;
        AJAXpost(url, obj, reloadAfterAjax);
    });
});