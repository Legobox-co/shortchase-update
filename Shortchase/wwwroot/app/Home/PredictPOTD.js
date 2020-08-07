$(document).ready(function () {
    $('#NewPredictionPOTDForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            Market: $('#PredictMarket').val(),
            Tip: $('#PredictTip').val(),
            POTDId: $('#POTDId').val(),
        };

        let url = $('#SavePredictionPOTDURL').val();

        let reload = true;
        AJAXpost(url, obj, reload);
    });


    $('#PredictMarket').change(function () {
        let obj = {
            Id: parseInt($(this).val()),
        };
        if (obj.Id !== 0) {

            let url = $('#GetDependentDataFromMarketURL').val();
            let reloadAfterAjax = false;
            AJAXpost(url, obj, reloadAfterAjax, null, function (data) {
                ClearTipsSelectOptions();
                let tips = JSON.parse(data.tips);

                if (tips.length > 0) {
                    RefillTipsSelectOptions(tips);
                }
                else {
                    ClearTipsSelectOptions();
                }
            });
        }
    });
});



function ClearTipsSelectOptions() {
    $('#PredictTip').html("");
    $('#PredictTip').prop("disabled", true);
    let defaultOption = document.createElement('option');
    $(defaultOption).val(0);
    $(defaultOption).prop("selected", true);
    $(defaultOption).prop("disabled", true);
    $(defaultOption).html("Select Tip");
    $('#PredictTip').append(defaultOption);
}

function RefillTipsSelectOptions(options) {
    ClearTipsSelectOptions();
    for (let i in options) {
        let option = document.createElement('option');
        $(option).val(options[i].Id);
        $(option).html(options[i].Description);
        $('#PredictTip').append(option);
    }

    $('#PredictTip').prop("disabled", false);

}