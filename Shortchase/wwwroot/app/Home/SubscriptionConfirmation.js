$(document).ready(function () {

    $(document).on('click', '.FinishSubscriptionWalletBtn', function (e) {
        e.preventDefault();
        let userAgreedOnTermsAndPrivacyPolicy = ValidateAgreements();
        if (userAgreedOnTermsAndPrivacyPolicy) {
            let obj = {
                SubscriptionId: $('#SubscriptionIdValue').val(),
                TimezoneOffset: new Date().getTimezoneOffset()
            };
            let url = $('#FinishSubscriptionWalletUrl').val();
            let redirectUrl = $('#RedirectUrl').val();
            AJAXpost(url, obj, false, false, function (data) {
                if (data.status) {
                    Swal.fire({
                        title: '' + data.messageTitle,
                        text: '' + data.message,
                        type: 'success',
                        showCancelButton: false
                    }).then((result) => {
                        loadFilteredPage(redirectUrl, { TimeOffset: new Date().getTimezoneOffset() });
                    });
                }
                else {
                    Swal.fire({ title: '' + data.messageTitle, text: '' + data.message, type: 'error' });
                }

            });
        }
        else {
            return Swal.fire({ title: 'Checkout incomplete!', text: 'In order to proceed, you will need to agree with our Terms & Conditions and our Privacy Policy.', type: 'error' });
        }


    });


    $(document).on('click', '.FinishSubscriptionPaypalBtn', function (e) {
        e.preventDefault();
        let userAgreedOnTermsAndPrivacyPolicy = ValidateAgreements();
        if (userAgreedOnTermsAndPrivacyPolicy) {
            $('#paypalButtonsWrapper').show(150);

        }
        else {
            $('#paypalButtonsWrapper').hide(150);
            return Swal.fire({ title: 'Checkout incomplete!', text: 'In order to proceed, you will need to agree with our Terms & Conditions, our Privacy Policy and Auto-Renewal Policy.', type: 'error' });
        }


    });

    paypal.Buttons(PaypalConfigSetupObj()).render('#paypal-button-container');
});


function ValidateAgreements() {
    let result = false;
    let termsAndConditionsAgreement = $('#TermsAndConditionsAgreement').prop('checked');
    let privacyPolicyAgreement = $('#PrivacyPolicyAgreement').prop('checked');
    let renewalAgreement = $('#AutoRenewalAgreement').prop('checked');

    if (termsAndConditionsAgreement && privacyPolicyAgreement && renewalAgreement) result = true;

    return result;
}

function PaypalConfigSetupObj() {
    let obj = {};

    obj.createSubscription = function (data, actions) {
        // This function sets up the details of the transaction, including the amount and line item details.

        /*let orderValue = parseFloat($('#fundsNeededToCompleteOrder').val()).toFixed(2);
        let order = {
            amount: {
                value: orderValue,
            }
        };*/
        let planId = $('#PaypalSubscriptionPlanIdValue').val();

        return actions.subscription.create({
            plan_id: planId
        });
    }

    obj.onApprove = function (data, actions) {
        // This function captures the funds from the transaction.




        FullPageLoaderShow();
        let obj = {
            SubscriptionId: $('#SubscriptionIdValue').val(),
            OrderId: data.orderID,
            PaypalSubscriptionID: data.subscriptionID,
            FacilitatorAccessToken: data.facilitatorAccessToken,
            TimezoneOffset: new Date().getTimezoneOffset()
        };
        let url = $('#FinishSubscriptionPaypal2Url').val();
        let redirectUrl = $('#RedirectUrl').val();
        AJAXpost(url, obj, false, false, function (data) {
            if (data.status) {
                Swal.fire({
                    title: '' + data.messageTitle,
                    text: '' + data.message,
                    type: 'success',
                    showCancelButton: false
                }).then((result) => {
                    loadFilteredPage(redirectUrl, { TimeOffset: new Date().getTimezoneOffset() });
                });
            }
            else {
                Swal.fire({ title: '' + data.messageTitle, text: '' + data.message, type: 'error' });
            }

        });





    }

    return obj;
}