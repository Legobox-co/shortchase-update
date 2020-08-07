$(document).ready(function () {



    $('.charCount').keydown(function (e) {
        let id = $(this).attr('id');
        let totalChars = $(this).val().length;
        $('#' + id + '_CharCount').html(totalChars);
    });


    $('#AddBetListingForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            Title: $('#AddListingTitle').val(),
            CategoryId: $('#AddCategory').val(),
            SubCategoryId: $('#AddSubCategory').val(),
            Price: $('#AddListingPrice').val(),
            Pick: $('#AddPick').val(),
            PickType: $('#AddListingPickType').val(),
            Description: $('#AddListingDescription').val(),
            OddsFormat: $('#AddListingOddFormat').val(),
            Odds: $('#AddListingOdds').val(),
            Stake: $('#AddStake').val(),
            Profit: $('#AddProfit').val(),
            Bookmaker: $('#AddListingWhereToPlay').val(),
            StartTime: $('#AddDateTimeStart').val(),
            FinishTime: $('#AddDateTimeFinish').val(),
            Analysis: $('#AddListingAnalysis').val(),
            Market: $('#AddMarket').val(),
            Tip: $('#AddTip').val(),
            PostedbyId: $('#LoggedInUserID').val(),
            HasSubcategories: $('#AddHasSubcategories').val(),
            TimezoneOffset: new Date().getTimezoneOffset(),
        };

        let validation = ValidateSubmissionObject(obj);

        if (validation.isValid) {
            let url = $('#AddBetListingURL').val();
            let reloadAfterAjax = true;
            AJAXpost(url, obj, reloadAfterAjax);
        }
        else {
            return Swal.fire({ title: 'Form incomplete!', html: validation.errorMessage, type: 'error' });
        }
    });

    $('#AddListingPickType').change(function () {
        let obj = {
            PickType: $(this).val()
        };
        if (obj.PickType.toLowerCase() !== "free") {
            $('#AddListingPrice').prop('disabled', false);
        }
        else {
            $('#AddListingPrice').val("0.00");
            $('#AddListingPrice').prop('disabled', true);
        }
    });

    $('#AddCategory').change(function () {

        CategoryChangeListener(this, 'add');
    });

    $('#AddMarket').change(function () {
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

    $('#AddPick').change(function () {
        let val = parseInt($(this).val());
        if (val !== 0) {
            $('#AddDateTimeStart').val($('#pickopt_' + val).data('start'));
            $('#AddDateTimeFinish').val($('#pickopt_' + val).data('finish'));
        }
    });
});


function ValidateSubmissionObject(obj, validateId = false) {
    let result = {
        isValid: false,
        errorMessage: "The following fields are missing: <br />"
    };

    let subcategoryValidation = true;
    let hasSubcategories = parseInt(obj.HasSubcategories);
    if (hasSubcategories === 1) {
        subcategoryValidation = (IsEmptyString(obj.SubCategoryId) || parseInt(obj.SubCategoryId) === 0);
    }
    else {
        subcategoryValidation = false;
    }

    let validatedId = true;
    if (validateId) {
        validatedId = IsEmptyString(obj.Id);
    }

    if (
        IsEmptyString(obj.Analysis)
        || IsEmptyString(obj.CategoryId)
        || IsEmptyString(obj.FinishTime)
        || (IsEmptyString(obj.Odds) || parseFloat(obj.Odds) <= 0)
        || IsEmptyString(obj.OddsFormat)
        || (IsEmptyString(obj.Pick) || parseInt(obj.Pick) === 0)
        || IsEmptyString(obj.PickType)
        || IsEmptyString(obj.PostedbyId)
        || (IsEmptyString(obj.Price) || (obj.PickType.toLowerCase() !== "free" && parseFloat(obj.Price) <= 0))
        || IsEmptyString(obj.StartTime)
        || (IsEmptyString(obj.Description) || obj.Description.length < 100)
        || subcategoryValidation
        || IsEmptyString(obj.Title)
        || (IsEmptyString(obj.Bookmaker) || parseInt(obj.Bookmaker) === 0)
        || (IsEmptyString(obj.Market) || parseInt(obj.Market) === 0)
        || (IsEmptyString(obj.Tip) || parseInt(obj.Tip) === 0)
    ) {
        if (IsEmptyString(obj.Analysis)) result.errorMessage += "<br /> - Analysis";
        if (IsEmptyString(obj.CategoryId)) result.errorMessage += "<br /> - Category";
        if (IsEmptyString(obj.Description) || obj.Description.length < 100) result.errorMessage += "<br /> - Preview (minimum of 100 characters)";
        if (IsEmptyString(obj.FinishTime)) result.errorMessage += "<br /> - Finish Time";
        if ((IsEmptyString(obj.Odds) || parseFloat(obj.Odds) <= 0)) result.errorMessage += "<br /> - Odds";
        if (IsEmptyString(obj.OddsFormat)) result.errorMessage += "<br /> - Odds Format";
        if ((IsEmptyString(obj.Pick) || parseInt(obj.Pick) === 0)) result.errorMessage += "<br /> - Pick";
        if (IsEmptyString(obj.PickType)) result.errorMessage += "<br /> - Pick Type";
        if (IsEmptyString(obj.PostedbyId)) result.errorMessage += "<br /> - Posted by";
        if ((IsEmptyString(obj.Market) || parseInt(obj.Market) === 0)) result.errorMessage += "<br /> - Market";
        if ((IsEmptyString(obj.Tip) || parseInt(obj.Tip) === 0)) result.errorMessage += "<br /> - Tip";
        if ((IsEmptyString(obj.Price) || parseFloat(obj.Price) <= 0)) result.errorMessage += "<br /> - Price";
        if (IsEmptyString(obj.StartTime)) result.errorMessage += "<br /> - Start Time";
        if (subcategoryValidation) result.errorMessage += "<br /> - SubCategory";
        if (IsEmptyString(obj.Title)) result.errorMessage += "<br /> - Title";
        if ((IsEmptyString(obj.Bookmaker) || parseInt(obj.Bookmaker) === 0)) result.errorMessage += "<br /> - Where To Play";
    }
    else {
        result.isValid = true;
    }
    return result;
}

function ClearSubcategoriesSelectOptions() {
    $('#AddSubCategory').html("");
    $('#AddSubCategory').prop("disabled", true);
    let defaultOption = document.createElement('option');
    $(defaultOption).val(0);
    $(defaultOption).prop("selected", true);
    $(defaultOption).prop("disabled", true);
    $(defaultOption).html("Select Subcategory");
    $('#AddSubCategory').append(defaultOption);
}


function RefillSubcategoriesSelectOptions(options) {
    ClearSubcategoriesSelectOptions();
    for (let i in options) {
        let option = document.createElement('option');
        $(option).val(options[i].Id);
        $(option).html(options[i].Name);
        $('#AddSubCategory').append(option);
    }

    $('#AddSubCategory').prop("disabled", false);

}


function ClearMarketSelectOptions() {
    $('#AddMarket').html("");
    $('#AddMarket').prop("disabled", true);
    let defaultOption = document.createElement('option');
    $(defaultOption).val(0);
    $(defaultOption).prop("selected", true);
    $(defaultOption).prop("disabled", true);
    $(defaultOption).html("Select Market");
    $('#AddMarket').append(defaultOption);
}

function RefillMarketSelectOptions(options) {
    ClearMarketSelectOptions();
    for (let i in options) {
        let option = document.createElement('option');
        $(option).val(options[i].Id);
        $(option).html(options[i].Name);
        $('#AddMarket').append(option);
    }

    $('#AddMarket').prop("disabled", false);

}


function ClearPickSelectOptions() {
    $('#AddPick').html("");
    $('#AddPick').prop("disabled", true);
    let defaultOption = document.createElement('option');
    $(defaultOption).val(0);
    $(defaultOption).prop("selected", true);
    $(defaultOption).prop("disabled", true);
    $(defaultOption).html("Select Pick");
    $('#AddPick').append(defaultOption);
}

function RefillPickSelectOptions(options) {
    ClearPickSelectOptions();
    for (let i in options) {
        let option = document.createElement('option');
        $(option).val(options[i].Id);
        $(option).attr('id', "pickopt_" + options[i].Id);
        $(option).html(options[i].Team1 + " vs " + options[i].Team2);
        $(option).attr("data-start", options[i].StartTime);
        $(option).attr("data-finish", options[i].FinishTime);
        $('#AddPick').append(option);
    }

    $('#AddPick').prop("disabled", false);

}


function ClearTipsSelectOptions() {
    $('#AddTip').html("");
    $('#AddTip').prop("disabled", true);
    let defaultOption = document.createElement('option');
    $(defaultOption).val(0);
    $(defaultOption).prop("selected", true);
    $(defaultOption).prop("disabled", true);
    $(defaultOption).html("Select Tip");
    $('#AddTip').append(defaultOption);
}

function RefillTipsSelectOptions(options) {
    ClearTipsSelectOptions();
    for (let i in options) {
        let option = document.createElement('option');
        $(option).val(options[i].Id);
        $(option).html(options[i].Description);
        $('#AddTip').append(option);
    }

    $('#AddTip').prop("disabled", false);

}


function ClearDatesOptions() {
    $('#AddDateTimeStart').val("");
    $('#AddDateTimeFinish').val("");
}


function ClearEditSubcategoriesSelectOptions() {
    $('#EditSubCategory').html("");
    $('#EditSubCategory').prop("disabled", true);
    let defaultOption = document.createElement('option');
    $(defaultOption).val(0);
    $(defaultOption).prop("selected", true);
    $(defaultOption).prop("disabled", true);
    $(defaultOption).html("Select Subcategory");
    $('#EditSubCategory').append(defaultOption);
}


function RefillEditSubcategoriesSelectOptions(options) {
    ClearSubcategoriesSelectOptions();
    for (let i in options) {
        let option = document.createElement('option');
        $(option).val(options[i].Id);
        $(option).html(options[i].Name);
        $('#EditSubCategory').append(option);
    }

    $('#EditSubCategory').prop("disabled", false);

}



function ClearEditMarketSelectOptions() {
    $('#EditMarket').html("");
    $('#EditMarket').prop("disabled", true);
    let defaultOption = document.createElement('option');
    $(defaultOption).val(0);
    $(defaultOption).prop("selected", true);
    $(defaultOption).prop("disabled", true);
    $(defaultOption).html("Select Market");
    $('#EditMarket').append(defaultOption);
}

function RefillEditMarketSelectOptions(options) {
    ClearEditMarketSelectOptions();
    for (let i in options) {
        let option = document.createElement('option');
        $(option).val(options[i].Id);
        $(option).html(options[i].Name);
        $('#EditMarket').append(option);
    }

    $('#EditMarket').prop("disabled", false);

}


function ClearEditPickSelectOptions() {
    $('#EditPick').html("");
    $('#EditPick').prop("disabled", true);
    let defaultOption = document.createElement('option');
    $(defaultOption).val(0);
    $(defaultOption).prop("selected", true);
    $(defaultOption).prop("disabled", true);
    $(defaultOption).html("Select Pick");
    $('#EditPick').append(defaultOption);
}

function RefillEditPickSelectOptions(options) {
    ClearEditPickSelectOptions();
    for (let i in options) {
        let option = document.createElement('option');
        $(option).val(options[i].Id);
        $(option).attr('id', "pickeditopt_" + options[i].Id);
        $(option).html(options[i].Team1 + " vs " + options[i].Team2);
        $(option).attr("data-start", options[i].StartTime);
        $(option).attr("data-finish", options[i].FinishTime);
        $('#EditPick').append(option);
    }

    $('#EditPick').prop("disabled", false);

}


function ClearEditTipsSelectOptions() {
    $('#EditTip').html("");
    $('#EditTip').prop("disabled", true);
    let defaultOption = document.createElement('option');
    $(defaultOption).val(0);
    $(defaultOption).prop("selected", true);
    $(defaultOption).prop("disabled", true);
    $(defaultOption).html("Select Tip");
    $('#EditTip').append(defaultOption);
}

function RefillEditTipsSelectOptions(options) {
    ClearEditTipsSelectOptions();
    for (let i in options) {
        let option = document.createElement('option');
        $(option).val(options[i].Id);
        $(option).html(options[i].Description);
        $('#EditTip').append(option);
    }

    $('#EditTip').prop("disabled", false);

}


function ClearEditDatesOptions() {
    $('#EditDateTimeStart').val("");
    $('#EditDateTimeFinish').val("");
}


function CategoryChangeListener(element, type) {
    let obj = {
        Id: parseInt($(element).val()),
        TimezoneOffset: new Date().getTimezoneOffset()
    };
    if (obj.Id !== 0) {
        if (!!$('#LoggedInUserID').val()) {
            let url = $('#GetDependentDataFromCategoryURL').val();
            let reloadAfterAjax = false;
            AJAXpost(url, obj, reloadAfterAjax, null, function (data) {
                if (type === "edit") {
                    ClearEditDatesOptions();
                    ClearEditPickSelectOptions();
                    ClearEditMarketSelectOptions();
                    ClearEditSubcategoriesSelectOptions();
                    ClearEditTipsSelectOptions();

                    let subcategories = JSON.parse(data.subcategories);
                    let markets = JSON.parse(data.markets);
                    let picks = JSON.parse(data.picks);


                    if (subcategories.length > 0) {
                        $('#EditHasSubcategories').val(1);
                        RefillEditSubcategoriesSelectOptions(subcategories);
                    }
                    else {
                        $('#EditHasSubcategories').val(0);
                        ClearEditSubcategoriesSelectOptions();
                    }
                    if (markets.length > 0) {
                        RefillEditMarketSelectOptions(markets);
                    }
                    else {
                        ClearEditMarketSelectOptions();
                    }
                    if (picks.length > 0) {
                        RefillEditPickSelectOptions(picks);
                    }
                    else {
                        ClearEditPickSelectOptions();
                    }
                }
                else {
                    ClearDatesOptions();
                    ClearPickSelectOptions();
                    ClearMarketSelectOptions();
                    ClearSubcategoriesSelectOptions();
                    ClearTipsSelectOptions();

                    let subcategories = JSON.parse(data.subcategories);
                    let markets = JSON.parse(data.markets);
                    let picks = JSON.parse(data.picks);


                    if (subcategories.length > 0) {
                        $('#AddHasSubcategories').val(1);
                        RefillSubcategoriesSelectOptions(subcategories);
                    }
                    else {
                        $('#AddHasSubcategories').val(0);
                        ClearSubcategoriesSelectOptions();
                    }
                    if (markets.length > 0) {
                        RefillMarketSelectOptions(markets);
                    }
                    else {
                        ClearMarketSelectOptions();
                    }
                    if (picks.length > 0) {
                        RefillPickSelectOptions(picks);
                    }
                    else {
                        ClearPickSelectOptions();
                    }
                }

            });
        }
        else {
            return OpenNotSignedInModal("You can list a pick only after signing up.");
        }
        
    }
}