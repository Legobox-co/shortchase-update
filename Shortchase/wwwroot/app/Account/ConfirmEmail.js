$('#ResendVerification').click(function () {
    $('#ResendVerificationFeedback').html("Sending...");
    $.ajax({
        url: $("#ResendCodeUrl").val(),
        type: 'POST',
        success: function (data) {
            var Message = "There was a problem resolving your request, please try again in a minute";
            if (data === true) {
                Message = "Your previous code has been revoked and a new confirmation email has been sent with a new code.";
            }
            bootbox.alert({
                size: "small",
                message: Message
            });
            $('#ResendVerificationFeedback').html("");
        },
        error: function () {
            bootbox.alert({
                size: "small",
                message: "There was a problem resolving your request, please try again in a minute"
            });
            $('#ResendVerificationFeedback').html("");
        }
    });
});