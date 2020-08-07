$(document).ready(function () {
    
    $(document).on('click', '.PayAllNowBtn', function (e) {
        e.preventDefault();

        let jsonObject = $('#PayAllNowInfo').val();
        if (IsEmptyString(jsonObject)) {
            return Swal.fire({ title: 'Form incomplete!', text: 'In order to proceed, you will need to provide valid payouts information.', type: 'error' });
        }
        else {
            jsonObject = JSON.parse(jsonObject);
            if (jsonObject.length <= 0) return Swal.fire({ title: 'Form incomplete!', text: 'In order to proceed, you will need to provide valid payouts information.', type: 'error' });
            else {
                let obj = {
                    OrderId: $(this).data('order'),
                    BatchStamp: $('#BatchStamp').val(),
                };

                let paypalPayoutObj = {
                    sender_batch_header: {
                        sender_batch_id: obj.BatchStamp,
                        email_subject: "You have a payout!",
                        email_message: "You have received a payout! Thanks for using Shortchase!",
                        recipient_type: "EMAIL"
                    },
                    items: []
                };

                $.each(jsonObject, function (index, element) {
                    let itemObj = {
                        amount: {
                            value: element.ValueToPay.toFixed(2),
                            currency: "CAD"
                        },
                        receiver: element.UserEmailToPay
                    };
                    paypalPayoutObj.items.push(itemObj);
                });
                obj.stringifiedFullObj = JSON.stringify(paypalPayoutObj);

                let url = $('#MakeOrderBatchPaypalPayoutURL').val();
                AJAXpost(url, obj, true);
            }
        }
    });

    $(document).on('click', '.PayIndividuallyBtn' ,function (e) {
        e.preventDefault();
        let obj = {
            PaypalEmailToSend: $(this).data('email'),
            AmountToBeSend: $(this).data('amount'),
            OrderId: $(this).data('order'),
            BatchStamp: $('#BatchStamp').val(),
            UserId: $(this).data('userid'),
        };

        if (!IsEmptyString(obj.PaypalEmailToSend) && !IsEmptyString("" + obj.AmountToBeSend)) {
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
                let url = $('#MakeOrderIndividualPaypalPayoutURL').val();
                AJAXpost(url, obj, true);

            }

        }
        else {
            return Swal.fire({ title: 'Form incomplete!', text: 'In order to proceed, you will need to provide a valid Paypal Account and an Amount greater than $ 0.00.', type: 'error' });
        }
    });
});