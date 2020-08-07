$(document).ready(function () {

    $('#VerifySMSCodeForm').submit(function (e) {
        e.preventDefault();
        let obj = {
            Email: $('#EmailToVerify').val(),
            Code: $('#SMSVerificationCode').val().toUpperCase()
        };

        if (obj.Code === null || obj.Code === "" || obj.Code.trim() === "" || obj.Email === null || obj.Email === "" || obj.Email.trim() === "") {
            Swal.fire(
                "Fill in information",
                'Enter your code and try again!',
                'error'
            );
            return false;
        }
        else {
            FullPageLoaderShow();
            let url = $('#ValidateVerificationCodeURL').val();

            $.ajax({
                url: url,
                type: 'POST',
                dataType: 'JSON',
                data: obj,
                success: function (data) {
                    let success = data.status;
                    if (success) {
                        Swal.fire({
                            title: '' + data.messageTitle,
                            text: '' + data.message,
                            type: 'success',
                            showCancelButton: false
                        }).then((result) => {
                            if (result.value) {
                                document.location.href = $('#AfterLoginHomeIndexURL').val();
                            }
                        });
                        
                    }
                    else {
                        Swal.fire({ title: '' + data.messageTitle, text: '' + data.message, type: 'error' });
                    }
                },
                complete: function () {
                    FullPageLoaderHide();
                }
            });
        }
    });


    $('.reSendSMSCode').click(function (e) {
        e.preventDefault();
        let obj = {
            Email: $('#EmailToVerify').val(),
        };
        FullPageLoaderShow();
        let url = $('#ResendSMSVerificationCodeURL').val();

        $.ajax({
            url: url,
            type: 'POST',
            dataType: 'JSON',
            data: obj,
            success: function (data) {
                FullPageLoaderHide();
                let type = data.status ? 'success' : 'error';
                Swal.fire({ title: '' + data.messageTitle, text: '' + data.message, type: type });
            }
        });
    });
});