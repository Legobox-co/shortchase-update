$(document).ready(function () {
    StartDataTables("OrdersList", [[0, 'desc']]);


    $(document).on('click', '.PayNowBtn', function (e) {
        e.preventDefault();

        let jsonObject = $(this).data('payoutinfo');
        if (jsonObject.length <= 0) return Swal.fire({ title: 'Form incomplete!', text: 'In order to proceed, you will need to provide valid payouts information.', type: 'error' });
        else {
            let obj = {
                OrderId: $(this).data('id'),
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

    });
});