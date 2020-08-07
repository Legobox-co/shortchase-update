$(document).ready(function () {

    $('#UpdateFeesAndTaxesForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            Fee: FromMaskedStringToNumber($('#ServiceFee').val()),
            Taxes: FromMaskedStringToNumber($('#TaxesValue').val()),
            BoisterousFee: FromMaskedStringToNumber($('#BoisterousServiceFee').val()),
        };
        let isValid = ValidateValues(obj.Fee, obj.Taxes);
        if (isValid.status) {
            let url = $('#UpdateServiceFeeAndTaxesURL').val();
            let reloadAfterAjax = true;
            AJAXpost(url, obj, reloadAfterAjax);
        }
        else {
            return Swal.fire({
                title: 'Incorrect values!',
                text: isValid.message,
                type: 'error',
                showCancelButton: false
            });
        }
        
    });

    /*
    $('#ServiceFee').on('keyup paste input change', function () {
        let value = FromMaskedStringToNumber($(this).val());
        CalculatorUpdateFee(value);
    });
    
    $('#TaxesValue').on('keyup paste input change', function () {
        let value = FromMaskedStringToNumber($(this).val());
        CalculatorUpdateTaxes(value);
    });*/

});

function FromMaskedStringToNumber(value) {
    let returnValue = 0;
    let parser = value.replace(/,/g, "");
    returnValue = parseFloat(parser);
    return returnValue;
}

function ValidateValues(fees, taxes) {
    let result = {
        status: false,
        message: ""
    }
    let resultFees = ValidateDecimalPercent(fees, "Service Fee");
    let resultTaxes = ValidateDecimalPercent(taxes, "Estimated Taxes");
    if (resultFees.status && resultTaxes.status) {
        result.status = true;
    }
    else {
        if (!resultFees.status) {
            result.message += resultFees.message;
        }
        if (!resultTaxes.status) {
            result.message += resultTaxes.message;
        }
    }
    return result;

}

function ValidateDecimalPercent(value, valueName) {
    let result = {
        status: false,
        message:""
    };
    if (parseFloat(value) <= 0.00) {
        result.message = valueName + " must be greater than 0.00! "
    }
    else {
        if (parseFloat(value) > 100.00) {
            result.message = valueName + " must be less than 100.00! "
        }
        else {
            result.status = true;
        }
    }
    return result;
}
