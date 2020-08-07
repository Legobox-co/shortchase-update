$(document).ready(function () {

    const table = StartDataTables("BetListingList", [[5, 'desc'], [6, 'desc']]);



    $(document).on('click', '.batchaction', function (e) {
        e.preventDefault();

        let obj = {
            Ids: GetAllSelectedRowsData()
        };
        let isActionPermitted = BatchActionChecker2(obj.Ids.length);
        if (isActionPermitted) {
            let actionType = $(this).data('actiontype');
            let url = $(this).data('action');
            switch (actionType) {
                case "delete":
                    let reloadAfterAjax = true;
                    return ConfirmBox("Are you sure you want to delete these items?", "", function () {
                        AJAXpost(url, obj, reloadAfterAjax);
                    });

                default:
                    return Swal.fire({ title: 'No elements selected!', html: "You need to select at least one element for batch actions!", type: 'error' });
            }
        }
        else {
            return Swal.fire({ title: 'No elements selected!', html: "You need to select at least one element for batch actions!", type: 'error' });
        }
    });

    $(document).on('click', '.ValidateThroughAPIBtn', function (e) {
        e.preventDefault();
        let obj = {
            Id: $(this).data('id'),
            Timezoneoffset: new Date().getTimezoneOffset()
        };
        let url = $('#ValidateThroughAPIURL').val();
        AJAXpost(url, obj, true);
    });


    $(document).on('click', '.ValidateManuallyBtn', function (e) {
        e.preventDefault();
        let id = $(this).data('id');
        $('#ValidateManuallyBetListingId').val(id);
        $('#PickValidationPlaceholder').html($('#PickId-' + id).text().trim());
        $('#StartValidationPlaceholder').html($('#StartTime-' + id).text().trim());
        $('#CategoryValidationPlaceholder').html($('#CategoryId-' + id).text().trim());
        $('#SubCategoryValidationPlaceholder').html($('#SubCategoryId-' + id).data('name'));
        $('#MarketValidationPlaceholder').html($('#Market-' + id).text().trim());
        $('#TipValidationPlaceholder').html($('#TipId-' + id).text().trim());

        $('#ValidateManuallyBetListingModal').modal('show');
    });

    $(document).on('click', '.CancelValidationBtn', function (e) {
        e.preventDefault();
        let id = $(this).data('id');
        ConfirmBox("Are you sure you want to cancel this listing validation?", "", function () {
            let url = $('#CancelBetListingValidationURL').val();
            let reloadAfterAjax = true;
            AJAXpost(url, { Id: id }, reloadAfterAjax);
        });

    });



    $('#ValidateManuallyBetListingModalForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            Id: $('#ValidateManuallyBetListingId').val(),
            IsCorrect: $('#IsCorrectValidateManually').val().toLowerCase() === "true" ? true : false,
        };
        ConfirmBox("Are you sure?", "", function () {
            let url = $('#ValidateBetListingManuallyURL').val();
            let reloadAfterAjax = true;
            AJAXpost(url, obj, reloadAfterAjax);
        });

    });


    $('.AddBetListingModalBtn').click(function (e) {
        e.preventDefault();
        $('#AddBetListingModal').modal('show');
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


    $('#AddBetListingModalForm').submit(function (e) {
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
            TimezoneOffset: new Date().getTimezoneOffset()
        };
        let validation = ValidateSubmissionObject(obj);

        if (validation.isValid) {

            let url = $('#AddBetListingModalURL').val();
            let reloadAfterAjax = true;
            AJAXpost(url, obj, reloadAfterAjax);
        }
        else {
            return Swal.fire({ title: 'Form incomplete!', html: validation.errorMessage, type: 'error' });
        }
    });


    $(document).on('click', '.DeleteItemBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: ($(this).data('id')),
        };
        let url = $('#DeleteBetListingURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to delete this item?", "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });

    });




    $('#EditListingPickType').change(function () {
        let obj = {
            PickType: $(this).val()
        };
        if (obj.PickType.toLowerCase() !== "free") {
            $('#EditListingPrice').prop('disabled', false);
        }
        else {
            $('#EditListingPrice').val("0.00");
            $('#EditListingPrice').prop('disabled', true);
        }
    });

    $('#EditCategory').change(function () {

        CategoryChangeListener(this, 'edit');
    });


    $('#EditMarket').change(function () {
        let obj = {
            Id: parseInt($(this).val()),
        };
        if (obj.Id !== 0) {

            let url = $('#GetDependentDataFromMarketURL').val();
            let reloadAfterAjax = false;
            AJAXpost(url, obj, reloadAfterAjax, null, function (data) {
                ClearEditTipsSelectOptions();
                let tips = JSON.parse(data.tips);

                if (tips.length > 0) {
                    RefillEditTipsSelectOptions(tips);
                }
                else {
                    ClearEditTipsSelectOptions();
                }
            });
        }
    });

    $('#EditPick').change(function () {
        let val = parseInt($(this).val());
        if (val !== 0) {
            $('#EditDateTimeStart').val($('#pickeditopt_' + val).data('start'));
            $('#EditDateTimeFinish').val($('#pickeditopt_' + val).data('finish'));
        }
    });

    $(document).on('click', '.EditItemBtn', function (e) {
        e.preventDefault();

        let id = $(this).data('id');

        $('#EditBetListingId').val(id);
        $('#EditBetListingPostedById').val($('#PostedbyId-' + id).data('value'));
        $('#EditListingTitle').val($('#Title-' + id).data('value'));
        $('#EditCategory').val($('#CategoryId-' + id).data('value'));

        let obj = {
            Id: parseInt($('#CategoryId-' + id).data('value')),
            TimezoneOffset: new Date().getTimezoneOffset()
        };
        if (obj.Id !== 0) {

            let url = $('#GetDependentDataFromCategoryURL').val();
            let reloadAfterAjax = false;
            AJAXpost(url, obj, reloadAfterAjax, null, function (data) {
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
                    $('#EditSubCategory').val($('#SubCategoryId-' + id).data('value'));
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
                $('#EditPick').val($('#PickId-' + id).data('value'));
                $('#EditDateTimeStart').val($('#pickeditopt_' + $('#PickId-' + id).data('value')).data('start'));
                $('#EditDateTimeFinish').val($('#pickeditopt_' + $('#PickId-' + id).data('value')).data('finish'));
                $('#EditMarket').val($('#Market-' + id).data('value'));
                $('#EditListingPrice').val($('#Price-' + id).data('value'));

                let marketobj = {
                    Id: parseInt($('#Market-' + id).data('value')),
                };
                if (marketobj.Id !== 0) {

                    let url = $('#GetDependentDataFromMarketURL').val();
                    let reloadAfterAjax = false;
                    AJAXpost(url, marketobj, reloadAfterAjax, null, function (data) {
                        ClearEditTipsSelectOptions();
                        let tips = JSON.parse(data.tips);

                        if (tips.length > 0) {
                            RefillEditTipsSelectOptions(tips);
                        }
                        else {
                            ClearEditTipsSelectOptions();
                        }
                        $('#EditTip').val($('#TipId-' + id).data('value'));


                        $('#EditListingPrice').val($('#Price-' + id).data('value'));
                        $('#EditListingPickType').val($('#PickType-' + id).data('value'));
                        $('#EditListingDescription').val($('#Description-' + id).data('value'));
                        $('#EditListingOddFormat').val($('#Odds-' + id).data('format'));
                        $('#EditListingOdds').val($('#Odds-' + id).data('value'));
                        $('#EditStake').val($('#StakeProfit-' + id).data('stake'));
                        $('#EditProfit').val($('#StakeProfit-' + id).data('profit'));
                        $('#EditListingWhereToPlay').val($('#BookmakerId-' + id).data('value'));

                        $('#EditListingAnalysis').val($('#Analysis-' + id).data('value'));

                        $('#EditBetListingModal').modal('show');
                    });
                }
            });
        }


    });



    $('#EditBetListingModalForm').submit(function (e) {
        e.preventDefault();
        let obj = {
            Id: $('#EditBetListingId').val(),
            Title: $('#EditListingTitle').val(),
            CategoryId: $('#EditCategory').val(),
            SubCategoryId: $('#EditSubCategory').val(),
            Price: $('#EditListingPrice').val(),
            Pick: $('#EditPick').val(),
            PickType: $('#EditListingPickType').val(),
            Description: $('#EditListingDescription').val(),
            OddsFormat: $('#EditListingOddFormat').val(),
            Odds: $('#EditListingOdds').val(),
            Stake: $('#EditStake').val(),
            Profit: $('#EditProfit').val(),
            Bookmaker: $('#EditListingWhereToPlay').val(),
            StartTime: $('#EditDateTimeStart').val(),
            FinishTime: $('#EditDateTimeFinish').val(),
            Analysis: $('#EditListingAnalysis').val(),
            Market: $('#EditMarket').val(),
            Tip: $('#EditTip').val(),
            PostedbyId: $('#LoggedInUserID').val(),
            HasSubcategories: $('#EditHasSubcategories').val(),
            TimezoneOffset: new Date().getTimezoneOffset()
        };

        let validation = ValidateSubmissionObject(obj, true);

        if (validation.isValid) {

            let url = $('#EditBetListingModalURL').val();
            let reloadAfterAjax = true;
            AJAXpost(url, obj, reloadAfterAjax);
        }
        else {
            return Swal.fire({ title: 'Form incomplete!', html: validation.errorMessage, type: 'error' });
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
        || subcategoryValidation
        || IsEmptyString(obj.Title)
        || (IsEmptyString(obj.Bookmaker) || parseInt(obj.Bookmaker) === 0)
        || (IsEmptyString(obj.Market) || parseInt(obj.Market) === 0)
        || (IsEmptyString(obj.Tip) || parseInt(obj.Tip) === 0)
    ) {
        if (IsEmptyString(obj.Analysis)) result.errorMessage += "<br /> - Analysis";
        if (IsEmptyString(obj.CategoryId)) result.errorMessage += "<br /> - Category";
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
}