$(document).ready(function () {
    StartDataTables("SubscriptionPlansList");

    //$('.AddSubscriptionPlanModalBtn').click(function (e) {
    //    e.preventDefault();
    //    $('#AddSubscriptionPlanModal').modal('show');
    //});



    //$('#AddSubscriptionPlanModal').submit(function (e) {
    //    e.preventDefault();

    //    let obj = {
    //        Type: $('#AddType').val(),
    //        Name: $('#AddName').val(),
    //        DurationInMonths: parseInt($('#AddDurationInMonths').val()),
    //        ValuePerMonth: parseFloat($('#AddValuePerMonth').val()),
    //    };

    //    if (obj.DurationInMonths <= 0) {
    //        return Swal.fire({
    //            title: 'Form Incomplete!',
    //            text: 'Duration needs to be at least 1 month!',
    //            type: 'error',
    //            showCancelButton: false
    //        });
    //    }

    //    if (obj.ValuePerMonth <= 0) {
    //        return Swal.fire({
    //            title: 'Form Incomplete!',
    //            text: 'Price cannot be negative!',
    //            type: 'error',
    //            showCancelButton: false
    //        });
    //    }

    //    let url = $('#AddSubscriptionPlanModalURL').val();
    //    let reloadAfterAjax = true;
    //    AJAXpost(url, obj, reloadAfterAjax);
    //});


    $(document).on('click', '.CreatePaypalProductBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: parseInt($(this).data('id')),
        };

        let paypalCreateProductObj = {
            name: $(this).data('name'),
            description: $(this).data('description'),
            type: 'SERVICE',
            category: 'SOFTWARE'
        };

        obj.PaypalRequestBody = JSON.stringify(paypalCreateProductObj);

        let url = $('#CreatePaypalProductForSubscriptionPlanURL').val();
        let reloadAfterAjax = true;
        AJAXpost(url, obj, reloadAfterAjax);

    });


    $(document).on('click', '.CreatePaypalSubscriptionPlanBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: parseInt($(this).data('id')),
        };

        let paypalCreateSubscriptionPlanObj = {
            name: $(this).data('name'),
            description: $(this).data('description'),
            product_id: $(this).data('product'),
            status: "ACTIVE",
            billing_cycles: [
                {
                    frequency: {
                        interval_unit: "MONTH",
                        interval_count: $(this).data('intervalcount')
                    },
                    tenure_type: "REGULAR",
                    sequence: 1,
                    total_cycles: 0,
                    pricing_scheme: {
                        fixed_price: {
                            value: $(this).data('price'),
                            currency_code: "CAD"
                        }
                    }
                }
            ],
            payment_preferences: {
                auto_bill_outstanding: true,
                setup_fee: {
                    value: "0",
                    currency_code: "CAD"
                },
                setup_fee_failure_action: "CONTINUE",
                payment_failure_threshold: 3
            },
            taxes: {
                percentage: "0",
                inclusive: true
            }
        };


        obj.PaypalRequestBody = JSON.stringify(paypalCreateSubscriptionPlanObj);

        let url = $('#CreatePaypalSubscriptionPlanForSubscriptionPlanURL').val();
        let reloadAfterAjax = true;
        AJAXpost(url, obj, reloadAfterAjax);

    });




    $(document).on('click', '.ActivateItemBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: parseInt($(this).data('id')),
            NewStatus: true
        };
        let url = $('#SwitchStatusSubscriptionPlanURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to activate this item?", "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });

    });



    $(document).on('click', '.DeactivateItemBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: parseInt($(this).data('id')),
            NewStatus: false
        };
        let url = $('#SwitchStatusSubscriptionPlanURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to deactivate this item?", "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });
    });




    $(document).on('click', '.EditItemBtn', function (e) {
        e.preventDefault();

        let id = parseInt($(this).data('id'));

        $('#EditSubscriptionPlanId').val(id);
        $('#EditName').val($('#Name-' + id).data('value'));
        $('#EditType').selectpicker('val', $('#Type-' + id).data('value'));
        $('#EditDurationInMonths').val(parseInt($('#DurationInMonths-' + id).data('value')));
        $('#EditValuePerMonth').val(parseFloat($('#ValuePerMonth-' + id).data('value')));

        $('#EditSubscriptionPlanModal').modal('show');

    });

    $('#EditSubscriptionPlanModalForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            Id: $('#EditSubscriptionPlanId').val(),
            Type: $('#EditType').val(),
            Name: $('#EditName').val(),
            DurationInMonths: parseInt($('#EditDurationInMonths').val()),
            ValuePerMonth: parseFloat($('#EditValuePerMonth').val()),
        };
        let url = $('#EditSubscriptionPlanModalURL').val();
        let reloadAfterAjax = true;
        AJAXpost(url, obj, reloadAfterAjax);
    });
});