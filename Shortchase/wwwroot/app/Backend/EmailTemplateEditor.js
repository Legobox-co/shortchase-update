$(document).ready(function () {
    StartDataTables("SecondaryEmailTemplatesList");
    
    $('.SendEmailBtn').click(function (e) {
        e.preventDefault();
        let id = $(this).data('id');

        $('#SendToUserEmailSubject').html($('#Subject-'+id).data('value'));
        $('#SendToUserEmailTitle').html($('#Title-'+id).data('value'));
        $('#SendToUserEmailTemplateId').val(id);
        $('#SendToUserEmailModal').modal('show');
    });

    $('.SendEmailAnonymousBtn').click(function (e) {
        e.preventDefault();
        let id = $(this).data('id');

        $('#SendToAnonymousEmailSubject').html($('#Subject-' + id).data('value'));
        $('#SendToAnonymousEmailTitle').html($('#Title-' + id).data('value'));
        $('#SendToAnonymousEmailTemplateId').val(id);
        $('#SendToAnonymousModal').modal('show');
    });

    $('#SendToUserEmailModalForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            TemplateId: $('#SendToUserEmailTemplateId').val(),
            Email: $('#useropt-' + $('#SendToUserEmailUserSelected').val()).data('email'),
            Name: $('#useropt-' + $('#SendToUserEmailUserSelected').val()).data('name'),
        };
        let url = $('#SendEmailTemplateToUserModalURL').val();
        let reloadAfterAjax = true;

        AJAXpost(url, obj, reloadAfterAjax);
    });

    $('#SendToAnonymousModalForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            TemplateId: $('#SendToAnonymousEmailTemplateId').val(),
            Email: $('#SendToAnonymousModalFormEmail').val(),
            Name: $('#SendToAnonymousModalFormName').val(),
        };
        let url = $('#SendAnonymousEmailTemplateToUserModalURL').val();
        let reloadAfterAjax = true;

        AJAXpost(url, obj, reloadAfterAjax);
    });


/*

    $(document).on('click', '.ActivateItemBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: parseInt($(this).data('id')),
            NewStatus: true
        };
        let url = $('#SwitchStatusPickURL').val();
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
        let url = $('#SwitchStatusPickURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to deactivate this item?", "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });
    });

    $(document).on('click', '.EditItemBtn', function (e) {
        e.preventDefault();

        let id = parseInt($(this).data('id'));

        $('#EditPickId').val(id);
        $('#EditTeam1').val($('#Team1-' + id).data('value'));
        $('#EditTeam2').val($('#Team2-' + id).data('value'));
        let dateStart = $('#StartTime-' + id).data('value');
        let dateEnd = $('#FinishTime-' + id).data('value');

        $('#EditDateTimeStart').val(dateStart);
        $('#EditDateTimeFinish').val(dateEnd);
        $('#EditCategory').selectpicker('val', $('#CategoryId-' + id).data('value'));

        $('#EditPickModal').modal('show');

    });

    $('#EditPickModalForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            Id: $('#EditPickId').val(),
            Team1: $('#EditTeam1').val(),
            Team2: $('#EditTeam2').val(),
            StartTime: $('#EditDateTimeStart').val(),
            FinishTime: $('#EditDateTimeFinish').val(),
            CategoryId: $('#EditCategory').val(),
            TimezoneOffset: new Date().getTimezoneOffset(),
        };
        let url = $('#EditPickModalURL').val();
        let reloadAfterAjax = true;
        AJAXpost(url, obj, reloadAfterAjax);
    });*/
});