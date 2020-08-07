$(document).ready(function () {

    $('.CheckoutButton').click(function (e) {
        e.preventDefault();

        let obj = {
            CheckoutType: $(this).data('type')
        };

        let userAgreedOnTermsAndPrivacyPolicy = ValidateAgreements();
        if (userAgreedOnTermsAndPrivacyPolicy) {
            if (obj.CheckoutType === "wallet") {
                let url = $('#CheckoutByWalletUrl').val();
                let redirectUrl = $('#RedirectUrl').val();
                AJAXpost(url, { TimezoneOffset: new Date().getTimezoneOffset() }, false, redirectUrl);
            }
            else {
                $('#paypalButtonsWrapper').show(150);
            }
        }
        else {
            return Swal.fire({ title: 'Checkout incomplete!', text: 'In order to proceed, you will need to agree with our Terms & Conditions and our Privacy Policy.', type: 'error' });
        }
    });





    paypal.Buttons(PaypalConfigSetupObj()).render('#paypal-button-container');

});

function ValidateAgreements() {
    let result = false;
    let termsAndConditionsAgreement = $('#TermsAndConditionsAgreement').prop('checked');
    let privacyPolicyAgreement = $('#PrivacyPolicyAgreement').prop('checked');

    if (termsAndConditionsAgreement && privacyPolicyAgreement) result = true;

    return result;
}

function PaypalConfigSetupObj() {
    let obj = {};

    obj.createOrder = function (data, actions) {
        // This function sets up the details of the transaction, including the amount and line item details.

        let orderValue = parseFloat($('#fundsNeededToCompleteOrder').text().trim()).toFixed(2);
        let order = {
            amount: {
                value: orderValue
            }
        };
        return actions.order.create({
            purchase_units: [order]
        });
    }

    obj.onApprove = function (data, actions) {
        // This function captures the funds from the transaction.
        return actions.order.capture().then(function (details) {


            // This function shows a transaction success message to your buyer.
            FullPageLoaderShow();
            let url = $('#CheckoutByPaypalUrl').val();
            let redirectUrl = $('#RedirectUrl').val();
            let orderObj = {
                orderID: data.orderID,
                TimezoneOffset: new Date().getTimezoneOffset()
            };
            AJAXpost(url, orderObj, false, redirectUrl);
            //Swal.fire({ title: 'Transaction completed!', text: 'Transaction completed by ' + details.payer.name.given_name, type: 'error' });
        });
    }

    return obj;
}