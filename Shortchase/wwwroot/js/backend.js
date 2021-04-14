// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    //INPUT MASKS START
    $(document).on("focus", ".datetimemask", function () {
        Inputmask({
            mask: "99/99/9999 99:99:99",
            showMaskOnHover: false,
            showMaskOnFocus: false
        }).mask($('.datetimemask'));
    });

    $(document).on("focus", ".dollar-inputmask", function () {
        $(this).maskMoney();
    });


    $('.charCount').keydown(function (e) {
        let id = $(this).attr('id');
        let totalChars = $(this).val().length;
        $('#' + id + '_CharCount').html(totalChars);
    });


    /*$(document).on('click', '.timezoneoffset', function (e) {
        e.preventDefault();
        let element = this;
        InsertBackendTimezoneOffsetRequest(element);
    });*/

    $.each($('.timezoneoffset'), (index, element) => {
        if ($(element).prop('href')) {
            $(element).prop('href', insertParameter($(element).prop('href'), 'TimeOffset', new Date().getTimezoneOffset()));
        }
    });
    AdminKnockoutTimer();

    $('.menu-section-toggle').click(function (e) {
        e.preventDefault();

        let data = $(this).data();
        if (data.status === "closed") {
            $(this).data('status', 'open');
            $('#' + data.section + "-icon").removeClass("fa-chevron-circle-down");
            $('#' + data.section + "-icon").addClass("fa-chevron-circle-up");
            $('#' + data.section).show(150);
        }
        else {
            $(this).data('status', 'closed');
            $('#' + data.section + "-icon").removeClass("fa-chevron-circle-up");
            $('#' + data.section + "-icon").addClass("fa-chevron-circle-down");
            $('#' + data.section).hide(150);
        }

    });



    $(document).on('click', '.OpenSelectMediaModal', function (e) {
        e.preventDefault();
        if (!IsEmptyString($('#MediaPickerValueChecker').val())) {
            ClearMediaPickerModal();
        }
        let submitObj = {
            TimezoneOffset: new Date().getTimezoneOffset()
        };
        submitObj.Width = parseFloat($(this).data('width')) > 0 ? parseFloat($(this).data('width')) : null;
        submitObj.Height = parseFloat($(this).data('height')) > 0 ? parseFloat($(this).data('height')) : null;

        console.log(submitObj);

        let elementToSubmitInfoId = $(this).data('submits');
        let elementToPreviewImageId = $(this).data('image');
        $('#MediaPickerImagePreviewId').val(elementToPreviewImageId);
        $('#MediaPickerModalSubmitsDataTo').val(elementToSubmitInfoId);
        let url = $('#GetMediaPickerDataURL').val();
        AJAXHTMLretriever(url, submitObj, function (data) {
            $('#MediaPickerModalContent').html("");
            $('#MediaPickerModalContent').html(data);

            $('#MediaPickerModal').modal('show');
        });

    });

    $(document).on('click', '.SelectMediaPickerOption', function (e) {
        e.preventDefault();
        let selectedMediaId = $(this).data('id');
        let media = $(this).data('media');
        let elementToSubmitValueId = $('#MediaPickerModalSubmitsDataTo').val();
        let previewImageId = $('#MediaPickerImagePreviewId').val();
        $('#' + previewImageId).attr('src', media);
        $('#MediaPickerTitle').val($(this).data('title'));
        $("#" + elementToSubmitValueId).val(selectedMediaId);
        $('#MediaPickerValueChecker').val(selectedMediaId);
        UpdateMediaSelectorLabel(elementToSubmitValueId);
        $('#MediaPickerModal').modal('hide');
    });

    $('#MediaPickerModal').on('hidden.bs.modal', function (e) {
        if (IsEmptyString($('#MediaPickerValueChecker').val())) {
            ClearMediaPickerModal();
        }
    });




    $(document).on('click', '.OpenSelectMediaModal2', function (e) {
        e.preventDefault();
        if (!IsEmptyString($('#MediaPickerValueChecker2').val())) {
            ClearMediaPickerModal2();
        }
        let elementToSubmitInfoId = $(this).data('submits');
        let elementToPreviewImageId = $(this).data('image');
        $('#MediaPickerImagePreviewId2').val(elementToPreviewImageId);
        $('#MediaPickerModalSubmitsDataTo2').val(elementToSubmitInfoId);
        let url = $('#GetMediaPickerDataURL2').val();
        AJAXHTMLretriever(url, { TimezoneOffset: new Date().getTimezoneOffset() }, function (data) {
            $('#MediaPickerModalContent2').html("");
            $('#MediaPickerModalContent2').html(data);

            $('#MediaPickerModal2').modal('show');
        });

    });

    $(document).on('click', '.SelectMediaPickerOption2', function (e) {
        e.preventDefault();
        let selectedMediaId = $(this).data('id');
        let media = $(this).data('media');
        let elementToSubmitValueId = $('#MediaPickerModalSubmitsDataTo2').val();
        let previewImageId = $('#MediaPickerImagePreviewId2').val();
        $('#' + previewImageId).attr('src', media);
        $('#MediaPickerTitle2').val($(this).data('title'));
        $("#" + elementToSubmitValueId).val(selectedMediaId);
        $('#MediaPickerValueChecker2').val(selectedMediaId);
        UpdateMediaSelectorLabel2(elementToSubmitValueId);
        $('#MediaPickerModal2').modal('hide');
    });

    $('#MediaPickerModal2').on('hidden.bs.modal', function (e) {
        if (IsEmptyString($('#MediaPickerValueChecker2').val())) {
            ClearMediaPickerModal2();
        }
    });

    $(document).on('click', '.rowSelectors', function (e) {
        e.preventDefault();

        RowSelectorFunction(this);
    });



    $(document).on('click', '#SelectAllRowsBtn', function (e) {
        e.preventDefault();

        SelectAllToggleText(this);
        SelectAllSelectableElements(this);
    });

});

function ClearMediaPickerModal() {
    let elementToUpdateValue = $('#MediaPickerModalSubmitsDataTo').val();
    $('#MediaPickerModalSubmitsDataTo').val("");
    $('#MediaPickerImagePreviewId').val("");
    $('#MediaPickerTitle').val("");
    $('#' + elementToUpdateValue).val("");
    $('#MediaPickerModalContent').html("");
    UpdateMediaSelectorLabel(elementToUpdateValue);
}

function UpdateMediaSelectorLabel(elementId) {
    let element = '#' + elementId;
    let labelWrapper = '#' + $(element).data('labelwrapper');
    let label = '#' + $(element).data('label');
    let value = $(element).val();
    let title = $('#MediaPickerTitle').val();
    if (IsEmptyString(value)) {
        $(label).html("");
        $(labelWrapper).hide(150);
    }
    else {
        $(label).html(title);
        $(labelWrapper).show(150);
    }
}

function ClearMediaPickerModal2() {
    let elementToUpdateValue = $('#MediaPickerModalSubmitsDataTo2').val();
    $('#MediaPickerModalSubmitsDataTo2').val("");
    $('#MediaPickerImagePreviewId2').val("");
    $('#MediaPickerTitle2').val("");
    $('#' + elementToUpdateValue).val("");
    $('#MediaPickerModalContent2').html("");
    UpdateMediaSelectorLabel(elementToUpdateValue);
}

function UpdateMediaSelectorLabel2(elementId) {
    let element = '#' + elementId;
    let labelWrapper = '#' + $(element).data('labelwrapper');
    let label = '#' + $(element).data('label');
    let value = $(element).val();
    let title = $('#MediaPickerTitle2').val();
    if (IsEmptyString(value)) {
        $(label).html("");
        $(labelWrapper).hide(150);
    }
    else {
        $(label).html(title);
        $(labelWrapper).show(150);
    }
}

function loadPage(url) {
    window.location.href = url;
}

function loadFilteredPage(url, json) {
    var fullurl = url + "?" + $.param(json);
    loadPage(fullurl);
}


function IsEmptyString(str) {
    if (!str || !str.trim() || str.trim() === "" || str.length <= 0) return true;
    else return false;
}


function FullPageLoaderShow() {
    $('#FullPageLoader').addClass("d-flex");
    $('#FullPageLoader').removeClass("d-none");
    $('body').addClass("new-page-loading");
}

function FullPageLoaderHide() {
    $('#FullPageLoader').addClass("d-none");
    $('#FullPageLoader').removeClass("d-flex");
    $('body').removeClass("new-page-loading");
}

function StartDataTables(tableId, orderObj = []) {
    if ($.fn.DataTable.isDataTable('#' + tableId)) {
        $('#' + tableId).DataTable().destroy();
    }
    let selectId = tableId + 'Select';
    let orderDefinition = [[0, 'asc']];
    if (orderObj.length > 0) {
        orderDefinition = orderObj;
    }
    let TheDataTable = $('#' + tableId).DataTable({
        "dom": "lftpi",
        responsive: {
            breakpoints: [
                { name: 'bigdesktop', width: Infinity },
                { name: 'meddesktop', width: 1480 },
                { name: 'smalldesktop', width: 1280 },
                { name: 'medium', width: 1188 },
                { name: 'tabletl', width: 1024 },
                { name: 'btwtabllandp', width: 848 },
                { name: 'tabletp', width: 768 },
                { name: 'mobilel', width: 480 },
                { name: 'mobilep', width: 320 }
            ]
        },
        language: {
            lengthMenu: '<span class="text-dark text-smaller"></span> <select id="' + selectId + '" class="selectpicker btn-xs-block text-smaller px-2 py-1 w-auto">' +
                '<option value="10">10</option>' +
                '<option value="25">25</option>' +
                '<option value="50">50</option>' +
                '<option value="100">100</option>' +
                '</select>',
            oPaginate: {
                sNext: '<i class="fas fa-chevron-right"></i>',
                sPrevious: '<i class="fas fa-chevron-left"></i>',
                sFirst: '<i class="fas fa-step-backward"></i>',
                sLast: '<i class="fas fa-step-forward"></i>'
            }
        },
        order: orderDefinition,
    });
    $('.selectpicker').selectpicker('refresh');
    return TheDataTable;
}


function AJAXpost(url, dataObj, reload = false, redirectUrl = null, customCallback = null) {
    try {
        FullPageLoaderShow();
        //console.log(url, JSON.stringify(dataObj));
        $.ajax({
            url: url,
            type: 'POST',
            contentType: 'application/json',
            dataType: 'JSON',
            cache: false,
            processData: false,
            //data: JSON.stringify(dataObj),
            success: function (data) {
                FullPageLoaderHide();
                if (customCallback === null) {
                    let success = data.status;
                    if (success) {
                        if (reload) {
                            Swal.fire({
                                title: '' + data.messageTitle,
                                text: '' + data.message,
                                type: 'success',
                                showCancelButton: false
                            }).then((result) => {
                                document.location.reload();
                            });
                        }
                        else {
                            if (redirectUrl === null) {
                                Swal.fire({
                                    title: '' + data.messageTitle,
                                    text: '' + data.message,
                                    type: 'success',
                                    showCancelButton: false
                                });
                            }
                            else {
                                Swal.fire({
                                    title: '' + data.messageTitle,
                                    text: '' + data.message,
                                    type: 'success',
                                    showCancelButton: false
                                }).then((result) => {
                                    if (result.value) {
                                        document.location.href = redirectUrl;
                                    }
                                });
                            }
                        }
                    }
                    else {
                        Swal.fire({ title: '' + data.messageTitle, text: '' + data.message, type: 'error' });
                    }
                }
                else {
                    customCallback(data);
                }

            },
            error: function (err) {
                FullPageLoaderHide();

                Swal.fire({ title: 'Error on Asynchronous call', text: 'Check console for more information', type: 'error' });
                console.log('-----------Error on Asynchronous call------------');
                console.log(err);
                console.log('-----------ENDOF Error on Asynchronous call------------');
            }

        });
    }
    catch (er) {
        Swal.fire({ title: 'Fatal Error', text: 'Check console for more information', type: 'error' });
        console.log('----------- Fatal Error ------------');
        console.log(er);
        console.log('-----------ENDOF Fatal Error------------');
    }
}




function AJAXFilePost(url, dataObj, reload = false, redirectUrl = null, customCallback = null) {
    try {
        FullPageLoaderShow();

        $.ajax({
            url: url,
            type: 'POST',
            //dataType: 'JSON',
            data: dataObj,
            success: function (data) {
                FullPageLoaderHide();
                if (customCallback === null) {
                    let success = data.status;
                    if (success) {
                        if (reload) {
                            Swal.fire({
                                title: '' + data.messageTitle,
                                text: '' + data.message,
                                type: 'success',
                                showCancelButton: false
                            }).then((result) => {
                                document.location.reload();
                            });
                        }
                        else {
                            if (redirectUrl === null) {
                                Swal.fire({
                                    title: '' + data.messageTitle,
                                    text: '' + data.message,
                                    type: 'success',
                                    showCancelButton: false
                                });
                            }
                            else {
                                Swal.fire({
                                    title: '' + data.messageTitle,
                                    text: '' + data.message,
                                    type: 'success',
                                    showCancelButton: false
                                }).then((result) => {
                                    if (result.value) {
                                        document.location.href = redirectUrl;
                                    }
                                });
                            }
                        }
                    }
                    else {
                        Swal.fire({ title: '' + data.messageTitle, text: '' + data.message, type: 'error' });
                    }
                }
                else {
                    customCallback(data);
                }

            },
            error: function (err) {
                FullPageLoaderHide();

                Swal.fire({ title: 'Error on Asynchronous call', text: 'Check console for more information', type: 'error' });
                console.log('-----------Error on Asynchronous call------------');
                console.log(err);
                console.log('-----------ENDOF Error on Asynchronous call------------');
            },
            cache: false,
            contentType: false,
            processData: false

        });
    }
    catch (er) {
        Swal.fire({ title: 'Fatal Error', text: 'Check console for more information', type: 'error' });
        console.log('----------- Fatal Error ------------');
        console.log(err);
        console.log('-----------ENDOF Fatal Error------------');
    }
}


function ConfirmBox(title, text, callback) {
    Swal.fire({
        title: title,
        text: text,
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes',
        cancelButtonText: 'Cancel'
    }).then((result) => {
        if (result.value) {
            callback();
        }
    });
}

function slugify(string) {
    const a = 'àáâäæãåāăąçćčđďèéêëēėęěğǵḧîïíīįìłḿñńǹňôöòóœøōõőṕŕřßśšşșťțûüùúūǘůűųẃẍÿýžźż·/_,:;'
    const b = 'aaaaaaaaaacccddeeeeeeeegghiiiiiilmnnnnoooooooooprrsssssttuuuuuuuuuwxyyzzz------'
    const p = new RegExp(a.split('').join('|'), 'g')

    return string.toString().toLowerCase()
        .replace(/\s+/g, '-') // Replace spaces with -
        .replace(p, c => b.charAt(a.indexOf(c))) // Replace special characters
        .replace(/&/g, '-and-') // Replace & with 'and'
        .replace(/[^\w\-]+/g, '') // Remove all non-word characters
        .replace(/\-\-+/g, '-') // Replace multiple - with single -
        .replace(/^-+/, '') // Trim - from start of text
        .replace(/-+$/, '') // Trim - from end of text
}



function InsertBackendTimezoneOffsetRequest(element) {
    window.location.href = insertParameter($(element).prop('href'), 'TimeOffset', new Date().getTimezoneOffset());
}





function AJAXHTMLretriever(url, dataObj, customCallback = null) {
    try {
        FullPageLoaderShow();

        $.ajax({
            url: url,
            type: 'POST',
            dataType: 'HTML',
            data: dataObj,
            success: function (data) {
                FullPageLoaderHide();
                customCallback(data);
            },
            error: function (err) {
                FullPageLoaderHide();

                Swal.fire({ title: 'Error on Asynchronous call', text: 'Check console for more information', type: 'error' });
                console.log('-----------Error on Asynchronous call------------');
                console.log(err);
                console.log('-----------ENDOF Error on Asynchronous call------------');
            }

        });
    }
    catch (er) {
        Swal.fire({ title: 'Fatal Error', text: 'Check console for more information', type: 'error' });
        console.log('----------- Fatal Error ------------');
        console.log(err);
        console.log('-----------ENDOF Fatal Error------------');
    }
}


function AdminKnockoutTimer() {
    let nowTime = new Date();
    let timeoutValueInMinutes = parseInt($('#SessionTimeoutValueHolder').data('value'));
    let countDownDate = new Date(nowTime.getTime() + timeoutValueInMinutes * 60000);
    // Update the count down every 1 second
    var knockoutInterval = setInterval(function () {

        // Get today's date and time
        var now = new Date().getTime();

        // Find the distance between now and the count down date
        var distance = countDownDate - now;

        // Time calculations for days, hours, minutes and seconds
        var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
        var seconds = Math.floor((distance % (1000 * 60)) / 1000);

        if (minutes < 10) {
            minutes = "0" + minutes;
        }
        if (seconds < 10) {
            seconds = "0" + seconds;
        }


        $('#CountDownKnockOutWrapper').html(minutes + ":" + seconds);

        // If the count down is over, write some text 
        if (distance < 0) {
            clearInterval(knockoutInterval);
            LogoutAction();
        }
    }, 1000);
}



function LogoutAction() {
    FullPageLoaderShow();
    $.ajax({
        url: $('#LogoutURL').val(),
        type: 'POST',
        dataType: 'JSON',
        data: {},
        success: function (data) {
            let success = data.status;
            if (success) {
                location.reload();
            }
            else {
                Swal.fire({ title: '' + data.messageTitle, text: '' + data.message, type: 'error' });
            }
        },
        complete: function () {
            FullPageLoaderHide();
        }
    });
}



function ShowBackendToast(message = "") {
    if (message === "") {
        message = "Processing, please wait...";
    }

    $('#BackendToastMessageWrapper').html(message);
    $('#BackendToastWrapper').removeClass("d-none");
    $('#BackendToastWrapper').addClass("d-flex");
}

function HideBackendToast() {
    $('#BackendToastMessageWrapper').html("");
    $('#BackendToastWrapper').removeClass("d-flex");
    $('#BackendToastWrapper').addClass("d-none");
}


function SelectAllToggleText(element) {
    let text = $(element).html();
    if (text === "Select All") {
        text = "Deselect All";
        $(element).removeClass("btn-dark-as-homepage");
        $(element).addClass("btn-light");
        $(element).data('power', 'deselect');
    }
    else {
        text = "Select All";
        $(element).removeClass("btn-light");
        $(element).addClass("btn-dark-as-homepage");
        $(element).data('power', 'select');
    }
    $(element).html(text);
}



function MakeItSelectAllButton(element) {

    let text = "Select All";
    $(element).removeClass("btn-light");
    $(element).addClass("btn-dark-as-homepage");
    $(element).data('power', 'select');
    $(element).html(text);

}


function MakeItDeselectAllButton(element) {
    let text = "Deselect All";
    $(element).removeClass("btn-dark-as-homepage");
    $(element).addClass("btn-light");
    $(element).data('power', 'deselect');
    $(element).html(text);

}




function SelectAllSelectableElements(ButtonElement) {
    $('.rowSelectors').each(function (index, element) {
        let totalselected = parseInt($(ButtonElement).data('totalselected'));
        let IsSelectAction = $(ButtonElement).data('power') === "select" ? false : true;
        if (IsSelectAction) {

            $(element).data('selected', true);
            $(element).html('<i class="fas fa-check"></i>');
            totalselected++;
        }
        else {

            $(element).data('selected', false);
            $(element).html('');
            totalselected--;
        }
        $(ButtonElement).data('totalselected', totalselected);
    });
}

function RowSelectorFunction(element) {
    let ButtonElement = "#" + $(element).data("controller");
    let totalselected = parseInt($(ButtonElement).data('totalselected'));
    let isSelected = $(element).data('selected');
    if (!isSelected) {

        $(element).data('selected', true);
        $(element).html('<i class="fas fa-check"></i>');
        totalselected++;
    }
    else {

        $(element).data('selected', false);
        $(element).html('');
        totalselected--;
    }
    $(ButtonElement).data('totalselected', totalselected);

    let totalElements = parseInt($(ButtonElement).data('totalelements'));
    if (totalselected === totalElements || totalselected > 0) {

        MakeItDeselectAllButton(ButtonElement);
    }
    else {
        MakeItSelectAllButton(ButtonElement);

    }
}



function IsDataTableRowVisible(datatable, element) {
    var tr = $(element).parents('tr');

    var row = datatable.row(tr);
    if (row.child.isShown()) {
        return true;
    }
    else {
        return false;
    }
}


function BatchActionChecker(element) {
    let result = false;

    let mainControllerElement = "#" + $(element).data('maincontroller');
    let totalElementsSelected = parseInt($(mainControllerElement).data('totalselected'));
    let totalElements = parseInt($(mainControllerElement).data('totalelements'));

    if (totalElements > 0) {
        if (totalElementsSelected > 0) {
            return true;
        }
    }

    return result;
}
function BatchActionChecker2(totalElements) {
    let result = false;

    if (totalElements > 0) {
        return true;
    }

    return result;
}


function GetAllSelectedRowsData() {
    let elementsArray = [];
    $('.rowSelectors').each(function (index, element) {
        let IsSelected = $(element).data('selected');
        if (IsSelected) {
            let value = $(element).data('itemid');
            elementsArray.push(value);
        }
    });
    return elementsArray;
}


function GetAllSelectedRowsSubscriptionData() {
    let elementsArray = [];
    $('.rowSelectors').each(function (index, element) {
        let IsSelected = $(element).data('selected');
        if (IsSelected) {
            let value = $(element).data('subscriptionid');
            elementsArray.push(value);
        }
    });
    return elementsArray;
}
