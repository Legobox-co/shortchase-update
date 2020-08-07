

$(document).ready(function () {
    StartDataTables("PicksList", [[1, 'asc']]);



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


    $('.AddPickModalBtn').click(function (e) {
        e.preventDefault();
        $('#AddPickModal').modal('show');
    });


    
    $('#Team1PhotoFile').change(function () {
      let filename1 = $('#Team1PhotoFile').val().split('\\').pop();
        if (filename1.trim() === "") filename1 = 'Choose Photo';
        $('#pictureLabel1').html(filename1);

      
    });

    $('#Team2PhotoFile').change(function () {
        let filename2 = $('#Team2PhotoFile').val().split('\\').pop();
        if (filename2.trim() === "") filename2 = 'Choose Photo';
        $('#pictureLabel2').html(filename2);

       
    });


    $('#AddPickModalForm').submit(function (e) {
        e.preventDefault();
        var formData = new FormData();

      let   obj = {
            Team1: $('#AddTeam1').val(),
            Team2: $('#AddTeam2').val(),
            StartTime: $('#AddDateTimeStart').val(),
            FinishTime: $('#AddDateTimeFinish').val(),
            CategoryId: $('#AddCategory').val(),
            Team1PhotoFile: '',
            Team2PhotoFile: '',
            TimezoneOffset: new Date().getTimezoneOffset(),
           
      };

        var fileInput1 = document.getElementById('Team1PhotoFile');
        if (fileInput1.files.length > 0) {
            obj.Team1PhotoFile = fileInput1.files[0];
            pictureName1 = obj.Team1PhotoFile.name;
           // console.log(obj.Team1PhotoFile);
        }

        var fileInput2 = document.getElementById('Team2PhotoFile');
        if (fileInput2.files.length > 0) {
            obj.Team2PhotoFile = fileInput2.files[0];
            pictureName2 = obj.Team2PhotoFile.name;
            //console.log(obj.Team1PhotoFile);
        }

        formData.append('data.Team1', obj.Team1);
        formData.append('data.Team2', obj.Team2);
        formData.append('data.StartTime', obj.StartTime);
        formData.append('data.FinishTime', obj.FinishTime);
        formData.append('data.CategoryId', obj.CategoryId);
        formData.append('data.TimezoneOffset', obj.TimezoneOffset);
        formData.append('data.Team1PhotoFile', obj.Team1PhotoFile, pictureName1);
        formData.append('data.Team2PhotoFile', obj.Team2PhotoFile, pictureName2);
        console.log(formData)
        let url = $('#AddPickModalURL').val();
       //let url = 'https://localhost:44328/api/pick';
        let reloadAfterAjax = true;
        AJAXpost(url, formData, reloadAfterAjax);
    });
    


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
        ConfirmBox("Are you sure you want to delete this item?", "", function () {
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
    });

   /* $('input[type="file"]').change(function (e) {
        var file1 = e.target.files[0].name;
        //alert(file1);
        //$('#Team1PhotoFileImg').attr('src', file1);
       $('#Team1Photo').val( file1);
        var myfile1 = $('#Team1Photo').val(); 
        alert('The file "' + myfile1 + '" has been selected.');
    });

    $('input[type="file"]').change(function (e) {
        var file2 = e.target.files[1].name;
       // $('#Team2PhotoFileImg').attr('src', file2);
        $('#Team2Photo').val(file2);

        var myfile2 = $('#Team2Photo').val();
        alert('The file "' + myfile2 + '" has been selected.'  );
    });*/

   

});