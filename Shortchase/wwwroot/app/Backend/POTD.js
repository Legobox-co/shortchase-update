$(document).ready(function () {

    StartDataTables("POTDListingList", [[2, 'desc'], [3, 'desc']]);

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


    $('.AddPOTDListingModalBtn').click(function (e) {
        e.preventDefault();
        $('#AddPOTDListingModal').modal('show');
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



    $('#AddBackgroundImage').change(function () {
        var filename = $('#AddBackgroundImage').val().split('\\').pop();
        if (filename.trim() === "") filename = 'Background Image';
        $('#Label-AddBackgroundImage').html(filename);
    });

    $('#AddPOTDListingModalForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            Title: $('#AddTitle').val(),
            CategoryId: $('#AddCategory').val(),
            SubCategoryId: $('#AddSubCategory').val(),
            PostedbyId: $('#LoggedInUserID').val(),
            Venue: $('#AddVenue').val(),
            StartTime: $('#AddDateTimeStart').val(),
            FinishTime: $('#AddDateTimeFinish').val(),
            HasSubcategories: parseInt($('#AddHasSubcategories').val()),
            Pick: $('#AddPick').val(),
            Market: $('#AddMarket').val(),
            Tip: $('#AddTip').val(),
            File: $('#AddBackgroundImage').val(),
            TimezoneOffset: new Date().getTimezoneOffset()
        };
       
        let validation = ValidateAddSubmissionObject(obj);

        if (validation.isValid) {
            let url = $('#AddPOTDListingModalURL').val();
            let reloadAfterAjax = true;
            AJAXFilePost(url, ParseFormData(obj), reloadAfterAjax);
        }
        else {
            return Swal.fire({ title: 'Form incomplete!', html: validation.errorMessage, type: 'error' });
        }
    });


    $(document).on('click', '.ViewPredictionsItemBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: ($(this).data('id')),
            TimeOffset: new Date().getTimezoneOffset()
        };
        let url = $('#GetPredictionsForPOTDURL').val();
        $('#loadDynamicPredictionsTable').html("");
        AJAXHTMLretriever(url, obj, function (data) {

            $('#loadDynamicPredictionsTable').html(data);
            StartDataTables("POTDDynamicPredictionsTable", [[0, 'desc']]);
            $('#ViewPredictionPOTDListingModal').modal('show');
        });
    });



    $(document).on('click', '.DeleteItemBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: ($(this).data('id')),
        };
        let url = $('#DeletePOTDListingURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to delete this item?", "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });

    });


    $(document).on('click', '.ValidateAllPredictionsBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: ($(this).data('id')),
        };
        let url = $('#POTDValidateAllPredictionsURL').val();
        let reloadAfterAjax = true;
        ConfirmBox("Are you sure you want to validate all predictions on this POTD?", "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });

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
        $('#editPreviewImage').attr('src', '');
        let id = $(this).data('id');

        $('#EditPOTDId').val(id);
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
                $('#EditMarket').val($('#MarketId-' + id).data('value'));

                let marketobj = {
                    Id: parseInt($('#MarketId-' + id).data('value')),
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



                        $('#EditCategory').val($('#CategoryId-' + id).data('value'));
                        $('#editPreviewImage').attr('src', '');

                        $('#EditVenue').val($('#Venue-' + id).data('value'));
                        $('#EditTitle').val($('#Title-' + id).data('value'));
                        $('#editPreviewImage').attr('src', $('#BackgroundImage-' + id).data('value'));
                        $('#EditPOTDListingModal').modal('show');
                    });
                }
            });
        }


    });


    $('#EditBackgroundImage').change(function () {
        var filename = $('#EditBackgroundImage').val().split('\\').pop();
        if (filename.trim() === "") filename = 'Background Image';
        $('#Label-EditBackgroundImage').html(filename);
    });
    
    $('#EditPOTDListingModalForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            Id: $('#EditPOTDId').val(),
            Title: $('#EditTitle').val(),
            CategoryId: $('#EditCategory').val(),
            SubCategoryId: $('#EditSubCategory').val(),
            PostedbyId: $('#LoggedInUserID').val(),
            Venue: $('#EditVenue').val(),
            StartTime: $('#EditDateTimeStart').val(),
            FinishTime: $('#EditDateTimeFinish').val(),
            HasSubcategories: parseInt($('#EditHasSubcategories').val()),
            Pick: $('#EditPick').val(),
            Market: $('#EditMarket').val(),
            Tip: $('#EditTip').val(),
            File: null,
            TimezoneOffset: new Date().getTimezoneOffset()
        };
        var fileInput = document.getElementById('EditBackgroundImage');
        if (fileInput.files.length > 0) {
            obj.File = {
                File: fileInput.files[0],
                Name: fileInput.files[0].name
            };
        }


        let validation = ValidateSubmissionObject(obj, false, false);

        if (validation.isValid) {
            let url = $('#EditPOTDListingModalURL').val();
            let reloadAfterAjax = true;
            AJAXFilePost(url, ParseFormData(obj, false, true), reloadAfterAjax);
        }
        else {
            return Swal.fire({ title: 'Form incomplete!', html: validation.errorMessage, type: 'error' });
        }
    });


    $(document).on('click', '.PredictionValidationBtn', function (e) {
        e.preventDefault();

        let obj = {
            Id: ($(this).data('id')),
            Type: $(this).data('type'),
        };

        let url = $('#POTDPredictionValidationURL').val();
        let reloadAfterAjax = true;
        let messageType = obj.Type ? "validate" : "invalidate";
        let message = "Are you sure you want to " + messageType + " this prediction?";
        ConfirmBox(message, "", function () {
            AJAXpost(url, obj, reloadAfterAjax);
        });

    });



    $(document).on('click', '.InformResultBtn', function (e) {
        e.preventDefault();
        let id = $(this).data('id');

        $('#InformResultPOTDId').val(id);
        $('#InformResultResult').val($('#Result-'+id).data('value'));
        $('#InformResultPOTDListingModal').modal('show');

    });


    $('#InformResultPOTDListingModalForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            Id: $('#InformResultPOTDId').val(),
            Result: $('#InformResultResult').val(),
        };
        let url = $('#InformResultPOTDListingModalURL').val();
        let reloadAfterAjax = true;
        AJAXpost(url, obj, reloadAfterAjax);
    });
});


function ValidateSubmissionObject(obj, isImageRequired = true, isPostedByRequired = true, validateId = false) {
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

    let ImageRequiredValidation = true;
    if (isImageRequired) {
        ImageRequiredValidation = obj.File === null;
    }
    let PostedByValidation = true;
    if (isPostedByRequired) {
        PostedByValidation = IsEmptyString(obj.PostedbyId);
    }


    if (
        IsEmptyString(obj.Title)
        || IsEmptyString(obj.CategoryId)
        || !PostedByValidation
        || IsEmptyString(obj.StartTime)
        || subcategoryValidation
        || !validatedId
        || IsEmptyString(obj.FinishTime)
        || (IsEmptyString(obj.Market) || parseInt(obj.Market) === 0)
        || (IsEmptyString(obj.Tip) || parseInt(obj.Tip) === 0)
        || (IsEmptyString(obj.Pick) || parseInt(obj.Pick) === 0)
        || IsEmptyString(obj.Venue)
        || !ImageRequiredValidation
    ) {
        if (IsEmptyString(obj.Title)) result.errorMessage += "<br /> - Title";
        if (IsEmptyString(obj.CategoryId)) result.errorMessage += "<br /> - Category";
        if (subcategoryValidation) result.errorMessage += "<br /> - Subcategory";
        if (!validatedId) result.errorMessage += "<br /> - Id";
        if (!ImageRequiredValidation) result.errorMessage += "<br /> - Background Image";
        if (!PostedByValidation) result.errorMessage += "<br /> - PostedbyId";
        if (IsEmptyString(obj.Venue)) result.errorMessage += "<br /> - Venue";
        if (IsEmptyString(obj.StartTime)) result.errorMessage += "<br /> - Start Time";
        if (IsEmptyString(obj.FinishTime)) result.errorMessage += "<br /> - Finish Time";
        if (IsEmptyString(obj.Pick) || parseInt(obj.Pick) === 0) result.errorMessage += "<br /> - Pick";
        if (IsEmptyString(obj.Market) || parseInt(obj.Market) === 0) result.errorMessage += "<br /> - Market";
        if (IsEmptyString(obj.Tip) || parseInt(obj.Tip) === 0) result.errorMessage += "<br /> - Tip";
    }
    else {
        result.isValid = true;
    }
    return result;
}




function ValidateAddSubmissionObject(obj) {
    let result = {
        isValid: false,
        errorMessage: "The following fields are missing: <br />"
    };



    let subcategoryValidation = false;
    let hasSubcategories = parseInt(obj.HasSubcategories);
    if (hasSubcategories === 1) {
        subcategoryValidation = (IsEmptyString(obj.SubCategoryId) || parseInt(obj.SubCategoryId) === 0);
    }



    if (
        IsEmptyString(obj.Title)
        || IsEmptyString(obj.CategoryId)
        || IsEmptyString(obj.PostedbyId)
        || IsEmptyString(obj.StartTime)
        || subcategoryValidation
        || IsEmptyString(obj.FinishTime)
        || (IsEmptyString(obj.Pick) || parseInt(obj.Pick) === 0)
        || (IsEmptyString(obj.Market) || parseInt(obj.Market) === 0)
        || (IsEmptyString(obj.Tip) || parseInt(obj.Tip) === 0)
        || IsEmptyString(obj.Venue)
        || IsEmptyString(obj.File)
    ) {
        if (IsEmptyString(obj.Title)) result.errorMessage += "<br /> - Title";
        if (IsEmptyString(obj.CategoryId)) result.errorMessage += "<br /> - Category";
        if (subcategoryValidation) result.errorMessage += "<br /> - Subcategory";
        if (IsEmptyString(obj.File)) result.errorMessage += "<br /> - Background Image";
        if (IsEmptyString(obj.PostedbyId)) result.errorMessage += "<br /> - PostedbyId";
        if (IsEmptyString(obj.Venue)) result.errorMessage += "<br /> - Venue";
        if (IsEmptyString(obj.StartTime)) result.errorMessage += "<br /> - Start Time";
        if (IsEmptyString(obj.FinishTime)) result.errorMessage += "<br /> - Finish Time";
        if ((IsEmptyString(obj.Pick) || parseInt(obj.Pick) === 0)) result.errorMessage += "<br /> - Pick";
        if ((IsEmptyString(obj.Market) || parseInt(obj.Market) === 0)) result.errorMessage += "<br /> - Market";
        if ((IsEmptyString(obj.Tip) || parseInt(obj.Tip) === 0)) result.errorMessage += "<br /> - Tip";
    }
    else {
        result.isValid = true;
    }
    return result;
}

function ParseFormData(obj, WillAlwaysHaveFile = true, hasId = false) {
    var formData = new FormData();

    formData.append('newListingInfo.Title', obj.Title);
    formData.append('newListingInfo.CategoryId', obj.CategoryId);
    formData.append('newListingInfo.SubCategoryId', obj.SubCategoryId);
    formData.append('newListingInfo.PostedbyId', obj.PostedbyId);
    formData.append('newListingInfo.Venue', obj.Venue);
    formData.append('newListingInfo.HasSubcategories', obj.HasSubcategories);
    formData.append('newListingInfo.Tip', obj.Tip);
    formData.append('newListingInfo.Pick', obj.Pick);
    formData.append('newListingInfo.Market', obj.Market);
    formData.append('newListingInfo.TimezoneOffset', new Date().getTimezoneOffset());
    if (WillAlwaysHaveFile) {
        formData.append('newListingInfo.BackgroundImage', obj.File);
    }
    else {
        if (obj.File === null) {
            formData.append('newListingInfo.BackgroundImage', null);
        }
        else {
            formData.append('newListingInfo.BackgroundImage', obj.File);
        }
    }
    if (hasId) {
        formData.append('newListingInfo.Id', obj.Id);
    }
    return formData;
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