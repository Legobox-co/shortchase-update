$(document).ready(function () {
    $('#ContactForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            Name: $('#ContactFullName').val(),
            Email: $('#ContactEmail').val(),
            Phone: $('#ContactTelephone').val(),
            Message: $('#ContactMessage').val(),
            TimezoneOffset: new Date().getTimezoneOffset()
        };

        if (IsEmptyString(obj.Name) || (IsEmptyString(obj.Email) && IsEmptyString(obj.Phone)) || IsEmptyString(obj.Message)) {
            return Swal.fire({ title: 'Form incomplete!', text: 'In order to proceed, you will need to provide: Full Name, Email or Phone and your message.', type: 'error' });
        }
        else {
            let okToSubmit = false;
            if (!IsEmptyString(obj.Email)) {
                let emailRegex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
                if (emailRegex.test(obj.Email)) {

                    okToSubmit = true;
                }
            }
            else {
                okToSubmit = true;
            }
            if (!okToSubmit) {
                return Swal.fire({ title: 'Email format incorrect!', text: 'In order to proceed, you will need to provide a valid email address.', type: 'error' });
            }
            else {
                let url = $('#SendContactMessageUrl').val();
                AJAXpost(url, obj, true);
            }
        }
    });
});

