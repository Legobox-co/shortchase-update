$(document).ready(function () {
    StartDataTables("UsersList");


    $('.SendIndividualBtn').click(function (e) {
        e.preventDefault();
        let email = $(this).data('email');
        let UserIdValue = $(this).data('id');

        $('#PaypalEmailToSend').val(email);
        $('#UserIdValue').val(UserIdValue);
        $('#SendMoneyToUsers_IndividualModal').modal('show');
    });


    $('#SendMoneyToUsersIndividualModalForm').submit(function (e) {
        e.preventDefault();
        let obj = {
            PaypalEmailToSend: $('#PaypalEmailToSend').val(),
            AmountToBeSend: $('#AmountToBeSend').val(),
            BatchStamp: $('#BatchStamp').val(),
            UserId: $('#UserIdValue').val(),
        };
        if (!IsEmptyString(obj.PaypalEmailToSend) && !IsEmptyString(obj.AmountToBeSend)) {
            let amountValidator = parseFloat(obj.AmountToBeSend);
            if (amountValidator <= 0.00) {
                return Swal.fire({ title: 'Form incomplete!', text: 'In order to proceed, you will need to provide an Amount greater than $ 0.00.', type: 'error' });
            }
            else {
                obj.AmountToBeSend = amountValidator;
                let paypalPayoutObj = {
                    sender_batch_header: {
                        sender_batch_id: obj.BatchStamp,
                        email_subject: "You have a payout!",
                        email_message: "You have received a payout! Thanks for using Shortchase!",
                        recipient_type: "EMAIL"
                    },
                    items: [
                        {
                            amount: {
                                value: obj.AmountToBeSend,
                                currency: "CAD"
                            },
                            receiver: obj.PaypalEmailToSend
                        }
                    ]
                };
                obj.stringifiedFullObj = JSON.stringify(paypalPayoutObj);
                let url = $('#MakeIndividualPaypalPayoutURL').val();
                AJAXpost(url, obj, true);

            }

        }
        else {
            return Swal.fire({ title: 'Form incomplete!', text: 'In order to proceed, you will need to provide a valid Paypal Account and an Amount greater than $ 0.00.', type: 'error' });
        }
    });
});