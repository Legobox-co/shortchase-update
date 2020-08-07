$(document).ready(function () {


    $('#UploadCSFFile').change(function () {
        var filename = $('#UploadCSFFile').val().split('\\').pop();
        if (filename.trim() === "") filename = 'Select CSV File';
        $('#UploadCSFFileLabel').html(filename);
    });


    $('#UploadCSFFileForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            CsvFile: null
        };

        var fileInput = document.getElementById('UploadCSFFile');
        if (fileInput.files.length > 0) {
            obj.CsvFile = {
                File: fileInput.files[0],
                Name: fileInput.files[0].name
            };
        }



        if (!obj.CsvFile && obj.CsvFile === null) {
            return Swal.fire({
                title: 'Form Incomplete!',
                text: 'You need to provide file.',
                type: 'error',
                showCancelButton: false
            });
        }
        else {
            var formData = new FormData();
            formData.append('template.CsvFile', obj.CsvFile.File, obj.CsvFile.Name);

            let url = $('#SendMoneyToUsersInMassParseMassPayoutTemplateURL').val();
            AJAXFilePost(url, formData, null, null, function (data) {
                $('#ReviewInfoWrapper').html("");
                $('#ReviewInfoWrapper').html(data);
            });
        }

    });

    $(document).on('click', '.FinishMassPayoutBtn', function (e) {
        e.preventDefault();
        let submissionObj = {
            stringifiedFullObj: "",
            BatchStamp: $('#BatchStamp').val(),
            Payouts: []
        };
        let paypalPayoutObj = {
            sender_batch_header: {
                sender_batch_id: submissionObj.BatchStamp,
                email_subject: "You have a payout!",
                email_message: "You have received a payout! Thanks for using Shortchase!",
                recipient_type: "EMAIL"
            },
            items: []
        };
        $('.mass-payout-information').each(function (index, element) {
            let obj = $(element).data();
            let payoutItem = {
                amount: {
                    value: obj.value,
                    currency: "CAD"
                },
                receiver: obj.paypalAccount
            };
            paypalPayoutObj.items.push(payoutItem);
            let payoutUser = {
                UserId: obj.id,
                Value: obj.value
            };
            submissionObj.Payouts.push(payoutUser);
        });


        submissionObj.stringifiedFullObj = JSON.stringify(paypalPayoutObj);
        let url = $('#FinishMassPayoutURL').val();
        AJAXpost(url, submissionObj, true);
    });
});