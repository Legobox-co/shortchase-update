$(document).ready(function () {

    var elementToScroll = $('#messagesWrapper');
    var heightToScroll = elementToScroll[0].scrollHeight;

    elementToScroll.scrollTop(heightToScroll);


    $(document).on('submit', '#MessagerForm', function (e) {
        e.preventDefault();
        BackendSendMessageFormSubmit();
    });

    $(document).on('keypress', '#MessagerMessageToSend', function (e) {
        if (e.keyCode === 13) BackendSendMessageFormSubmit();
    });


});




function BackendSendMessageFormSubmit() {

    let obj = {
        MessageContent: $('#MessagerMessageToSend').val(),
        TimezoneOffset: new Date().getTimezoneOffset(),
        Id: $('#ToId').val()
    };

    if (IsEmptyString(obj.MessageContent)) {
        return Swal.fire({ title: 'No content provided!', text: 'You need to write something before you send a message!', type: 'error' });
    }
    else {
        let url = $('#SendMessageURL').val();
        AJAXpost(url, obj, true);
    }
}