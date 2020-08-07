$('#FPSendEmailBtn').click(function () {
    $('#FPSendEmailBtn').prop('disabled', true);
    $('#FPSendEmailBtn').text('Sending...');
    $.ajax({
        url: $("#ForgotPasswordEmailUrl").val(),
        type: 'POST',
        data: { Email: $('#ForgotPasswordEmailValue').val() },
        success: function (data) {
            $('#FPSendEmailBtn').prop('disabled', false);
            $('#FPSendEmailBtn').text('Send Email');
            var Message = "There was a problem resolving your request, please try again in a minute";
            if (data === true) {
                Message = "An email has been sent to the address provided with a new code. Previous request codes have been revoked.";
            }
            bootbox.alert({
                size: "small",
                message: Message
            });
        },
        error: function () {
            $('#FPSendEmailBtn').prop('disabled', false);
            $('#FPSendEmailBtn').text('Send Email');
            bootbox.alert({
                size: "small",
                message: "There was a problem resolving your request, please try again in a minute"
            });
        }
    });
});