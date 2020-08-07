// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {

    //INPUT MASKS START
    $(document).on("focus", ".phone-number", function () {
        Inputmask({
            mask: "999-999-9999",
            showMaskOnHover: false,
            showMaskOnFocus: false
        }).mask($('.phone-number'));
    });
    //INPUT MASKS START
    $(document).on("focus", ".date-masked-input", function () {
        Inputmask({
            mask: "9999-99-99",
            showMaskOnHover: false,
            showMaskOnFocus: false
        }).mask($('.date-masked-input'));
    });
    //INPUT MASKS START
    $(document).on("focus", ".date-masked-input-2", function () {
        Inputmask({
            mask: "99/99/9999",
            showMaskOnHover: false,
            showMaskOnFocus: false
        }).mask($('.date-masked-input-2'));
    });
    $(document).on("focus", ".dollar-inputmask", function () {
        $(this).maskMoney();
    });


    let height = 0;
    if ($('#FaqNavigator').length) {
        height = $("#FaqNavigator").offset().top - 50;
    }
    $(window).scroll(function () {
        if ($('#websiteNavigator').length) {
            if ($(this).scrollTop() > 150) {
                $('#websiteNavigator').removeClass('transparent-navigator');
                $('#websiteNavigator').addClass('grey-navigator');
            } else {
                $('#websiteNavigator').removeClass('grey-navigator');
                $('#websiteNavigator').addClass('transparent-navigator');
            }
        }
        if ($('#FaqNavigator').length) {
            if ($(this).scrollTop() >= height) {
                if ($("#FaqNavigator").css('position') !== "fixed") {
                    $("#FaqNavigator").css('position', 'fixed');
                    $("#FaqNavigator").css('top', '8em');
                    $("#FaqNavigator").css('left', '10em');
                }
            }
            else {
                if ($("#FaqNavigator").css('position') === "fixed") {
                    $("#FaqNavigator").css('position', 'static');
                }
            }
        }

    });

    $(document).on('click', '.BoisterousModalLink', function (e) {
        e.preventDefault();
        WebsiteCloseAllModalsBeforeOpeningAnotherModal();
        $('#BoisterousModal').modal('show');
    });

    $(document).on('click', '.ShortchaseProModalLink', function (e) {
        e.preventDefault();
        WebsiteCloseAllModalsBeforeOpeningAnotherModal();
        $('#ShortchaseProModal').modal('show');
    });

    $(document).on('click', '.LoginModalLink', function (e) {
        e.preventDefault();
        WebsiteCloseAllModalsBeforeOpeningAnotherModal();
        $('#LoginModal').modal('show');
    });

    $(document).on('click', '.ReadNotificationsBtn', function (e) {
        let obj = {
            Id: $(this).data('user')
        }
        let url = $(this).data('url');
        AJAXpost(url, obj, true);
    });


    $(document).on('click', '.SignUpModalLink', function (e) {
        e.preventDefault();
        WebsiteCloseAllModalsBeforeOpeningAnotherModal();
        $('#SignUpModal').modal('show');
    });


    $(document).on('click', '.PostAPredictionNotSignedIn', function (e) {
        e.preventDefault();
        OpenNotSignedInModal("You can post a prediction only after signing up.");
    });


    $(document).on('click', '.OpenNotAuthNotAllowedModal', function (e) {
        e.preventDefault();
        OpenNotSignedInModal("You can list a pick only after signing up.");
    });


    $(document).on('click', '.OpenAdminNotAllowedModal', function (e) {
        e.preventDefault();
        OpenAdminNotAllowedModal();
    });


    $(document).on('click', '.openNotEnabledForBettorModal', function (e) {
        e.preventDefault();
        OpenNotEnabledForBettorModal();
    });


    $(document).on('click', '.OpenNotAllowedCustomActionModal', function (e) {
        e.preventDefault();
        let message = $(this).data('message');
        OpenNotAllowedCustomActionModal(message);
    });



    $('.hoverablePick').hover(
        function () {
            $(this).removeClass('bg-grey');
            $(this).addClass('bg-yellow');
            $(this).addClass('shadow');
            $(this).addClass('scaleElementEffect');
        },
        function () {
            $(this).removeClass('scaleElementEffect');
            $(this).removeClass('shadow');
            $(this).removeClass('bg-yellow');
            $(this).addClass('bg-grey');
        }
    );


    $('.hoverable').hover(
        function () {
            $(this).removeClass('shadow-sm');
            $(this).addClass('shadow-lg');
        },
        function () {
            $(this).removeClass('shadow-lg');
            $(this).addClass('shadow-sm');
        }
    );


    $('.hoverable-lg').hover(
        function () {
            $(this).addClass('shadow-lg');
        },
        function () {
            $(this).removeClass('shadow-lg');
        }
    );
    $('.countryPhoneSelector').change(function (e) {
        let displayId = $(this).data('display');
        let value = $(this).val();
        let code = '';
        $(this).children().each(function (index, element) {
            if ($(element).val() === value) {
                code = $(element).attr('name');
            }
        });
        $('#' + displayId).html(code);

    });

    $('.CallLogOutAction').click(function (e) {
        e.preventDefault();
        LogoutAction();
    });

    $('.SwitchProfile').click(function (e) {
        e.preventDefault();
        let profile = $(this).data('profile');
        let message = "";
        if (profile === "Capper") {
            message = "You switched to your capper account.";
        }
        else {
            message = "You switched to your bettor account.";
        }
        ShowToast(message, function () {
            SwitchProfile(profile);
        });
    });

    $('.SwitchProfileAndGoToPage').click(function (e) {
        e.preventDefault();
        let profile = $(this).data('profile');
        let urlRedirection = $(this).data('url');
        let message = "";
        if (profile === "Capper") {
            message = "You switched to your capper account.";
        }
        else {
            message = "You switched to your bettor account.";
        }
        ShowToast(message, function () {
            SwitchProfile(profile, urlRedirection);
        });
    });



    $(document).on('click', '.BoisterousPlansRadio', function (e) {
        let isChecked = $(this).prop('checked');
        let value = $(this).val();
        if (isChecked) {
            $('#BoisterousPlanPicked').val(value);
        }
    });

    $(document).on('click', '.GetStartedBoisterous', function (e) {
        let value = parseInt($('#BoisterousPlanPicked').val());
        if (value !== 0) {
            $('#SignUpModal_Boisterous').val(value);
        }
    });

    $(document).on('click', '.GetStartedShortchasePro', function (e) {
        $('#SignUpModal_ShortchasePro').val($('#ShortChaseProPlanId').val());
    });

    $('#SignUpModal').on('hide.bs.modal', function (e) {
        $('.BoisterousPlansRadio').prop('checked', false);
        $('#BoisterousPlanPicked').val(0);
        $('#SignUpModal_Boisterous').val(0);
        $('#SignUpModal_ShortchasePro').val(0);
    });

    /*$(document).on('click', '.timezoneoffset', function (e) {
        e.preventDefault();
        let element = this;
        InsertSiteTimezoneOffsetRequest(element);
    });*/

    $.each($('.timezoneoffset'), (index, element) => {
        if ($(element).prop('href')) {
            $(element).prop('href', insertParameter($(element).prop('href'), 'TimeOffset', new Date().getTimezoneOffset()));
        }
    });

    $(document).on('click', '.paginationItemClick', function (e) {
        e.preventDefault();

        let obj = {
            pageSize: $(this).data('pagesize'),
            page: $(this).data('page'),
            TimeOffset: new Date().getTimezoneOffset(),
        };

        let url = $(this).data('url');

        if ($(this).hasClass('currentPageLink')) return false;
        else {
            loadFilteredPage(url, obj);
        }
    });

    $(document).on('click', '.profilepaginationItemClick', function (e) {
        e.preventDefault();

        let obj = {
            pageSize: $(this).data('pagesize'),
            page: $(this).data('page'),
            Id: $(this).data('id'),
            TimeOffset: new Date().getTimezoneOffset(),
        };

        let url = $(this).data('url');

        if ($(this).hasClass('currentPageLink')) return false;
        else {
            loadFilteredPage(url, obj);
        }
    });


    $(document).on('click', '.paginationMarketplaceItemClick', function (e) {
        e.preventDefault();
        let filters = {};
        $('.filters-marketplace').each(function (index, element) {
            if (!$(element).val()) filters[$(element).data('filter')] = null;
            else filters[$(element).data('filter')] = $(element).val();

        });
        let url = $(this).data('url');


        filters.pageSize = $(this).data('pagesize');
        filters.page = $(this).data('page');

        if ($(this).hasClass('currentPageLink')) return false;
        else {
            loadFilteredPage(url, filters);
        }
    });


    $(document).on('keypress', '#homeSearchBar', function (e) {
        let value = $(this).val();
        if (e.keyCode === 13) {
            let filters = {
                Keyword: value,
                TimeOffset: new Date().getTimezoneOffset()
            };
            const url = $('#SearchResultsPageURL').val();

            loadFilteredPage(url, filters);
        }
    });

    $('.AcceptCookies').click(function (e) {
        e.preventDefault();
        let url = $(this).data('method');
        AJAXpost(url, {}, true);
    });

    var countdownTime = $('#CountDownAdminTime').data('value');
    if (countdownTime) AdminKnockoutTimer();


    $(document).on('click', '#UserMessageBoardNotificationWrapper', function (e) {
        e.preventDefault();
        $('#UserMessageBoardNotificationWrapper').hide(150, function () {

            $('#UserMessageBoardContentWrapper').show(150, function () {
                ScrollToBottomOfMessageBox('UserMessageBoardMessagesExchangedWrapper');
            });
        });
    });


    $(document).on('click', '#UserMessageBoardContentHeader', function (e) {
        e.preventDefault();
        $('#UserMessageBoardContentWrapper').hide(150, function () {

            $('#UserMessageBoardNotificationWrapper').show(150);
        });
    });


    let hasToLoadMessages = parseInt($('#LoadMessagesToMessager').val()) === 1 ? true : false;

    if (hasToLoadMessages) {
        let obj = {
            TimezoneOffset: new Date().getTimezoneOffset()
        };

        let url = $('#RetrieveMessagesURL').val();
        AJAXpost(url, obj, false, false, function (data) {
            let parsedData = JSON.parse(data.messages);
            let unreadMessages = parseInt(data.unreadmessages);

            if (unreadMessages > 0) {
                $('#UnreadMessagesCountBadge').html(unreadMessages);
                $('#UnreadMessagesCountBadge').show(150);

            }
            else $('#UnreadMessagesCountBadge').hide(150);

            if (!!parsedData && parsedData.length > 0) {
                LoadMessagesToMessenger(parsedData);
            }
        });
    }

    $(document).on('submit', '#MessagerForm', function (e) {
        e.preventDefault();
        SendMessageFormSubmit();
    });

    $(document).on('keypress', '#MessagerMessageToSend', function (e) {
        if (e.keyCode === 13) SendMessageFormSubmit();
    });



    //setup before functions
    var typingTimer;                //timer identifier
    var doneTypingInterval = 500;  //time in ms, 5 second for example
    var $input = $('#homeSearchBar');

    //on keyup, start the countdown
    $input.on('keyup', function () {
        clearTimeout(typingTimer);
        typingTimer = setTimeout(doneTyping, doneTypingInterval);
    });

    //on keydown, clear the countdown 
    $input.on('keydown', function () {
        clearTimeout(typingTimer);
    });

    $('.CloseHomeSearchBarDropdown').click(function (e) {
        e.preventDefault();
        HideHeaderBarDropdown();
    });
    $(document).on('click', '.DropdownSearchCategory', function (e) {
        e.preventDefault();

        let filters = {
            Category: $(this).data('id'),
            TimeOffset: new Date().getTimezoneOffset()
        };
        const url = $('#SearchResultsPageURL').val();

        loadFilteredPage(url, filters);
    });
    $(document).on('click', '.DropdownSearchSubCategory', function (e) {
        e.preventDefault();

        let filters = {
            SubCategory: $(this).data('id'),
            Category: $(this).data('categoryid'),
            TimeOffset: new Date().getTimezoneOffset()
        };
        const url = $('#SearchResultsPageURL').val();

        loadFilteredPage(url, filters);
    });
    $(document).on('click', '.DropdownSearchPickType', function (e) {
        e.preventDefault();

        let filters = {
            SortByPickType: $(this).data('type'),
            TimeOffset: new Date().getTimezoneOffset()
        };
        const url = $('#SearchResultsPageURL').val();

        loadFilteredPage(url, filters);
    });
    $(document).on('click', '.DropdownSearchCapper', function (e) {
        e.preventDefault();

        let filters = {
            Keyword: $(this).data('search'),
            TimeOffset: new Date().getTimezoneOffset()
        };
        const url = $('#SearchResultsPageURL').val();

        loadFilteredPage(url, filters);
    });

    let cookieMessageAcceptance = getCookie("NotAuthUserAcceptedCookie");
    if (cookieMessageAcceptance === null) {
        setCookie("NotAuthUserAcceptedCookie", "false", 30);
    }
    else {
        if (cookieMessageAcceptance === "true") {
            $('#CookieAcceptanceNoteWrapper').hide(0);
        }
        else {
            $('#CookieAcceptanceNoteWrapper').show(0);
        }
    }

    $(document).on('click', '.AcceptCookiesJSOnly', function (e) {
        e.preventDefault();
        setCookie("NotAuthUserAcceptedCookie", "true", 30);
        document.location.reload();
    });



    $(document).on('click', '.CloseShortchaseToast', function (e) {
        e.preventDefault();
        HideToast();
    });
    $(document).on('click', '.CloseDangerShortchaseToast', function (e) {
        e.preventDefault();
        HideDangerToast();
    });

    window.addEventListener("beforeunload", function (e) {
        FullPageLoaderShow();
    }, false);


    $('#ContinueShoppingModal').on('hidden.bs.modal', function (e) {
        document.location.reload();
    })
    $(document).on('click', '.ContinueShoppingButton', function (e) {
        document.location.reload();
    })
});



//user is "finished typing," do something
function doneTyping() {
    //do something

    let obj = {
        Keyword: $('#homeSearchBar').val(),
    };
    const url = $('#HomeSearchBarDropdownResultsURL').val();
    if (!IsEmptyString(obj.Keyword)) {
        AJAXHTMLretriever(url, obj, function (data) {
            $('#HeaderBarDropdownResultsWrapper').html("");
            $('#HeaderBarDropdownResultsWrapper').html(data);
            ShowHeaderBarDropdown(FillSearchTerm());
        });

    }

}

/* When the user clicks on the button, 
toggle between hiding and showing the dropdown content */
function FillSearchTerm() {
    $('.searchTermPlaceholder').html($('#homeSearchBar').val());
}
function ShowHeaderBarDropdown(callback = null) {
    $('#HeaderBarDropdown').show(150, function () {
        if (callback !== null) callback();
    });
}
function HideHeaderBarDropdown() {
    $('#HeaderBarDropdown').hide(150);
}




function SendMessageFormSubmit() {

    let obj = {
        MessageContent: $('#MessagerMessageToSend').val(),
        TimezoneOffset: new Date().getTimezoneOffset()
    };

    if (IsEmptyString(obj.MessageContent)) {
        return Swal.fire({ title: 'No content provided!', text: 'You need to write something before you send a message!', type: 'error' });
    }
    else {
        let url = $('#SendMessageURL').val();
        AJAXpost(url, obj, false, false, function (data) {
            $('#MessagerMessageToSend').val("");
            let parsedData = JSON.parse(data.messages);
            let unreadMessages = parseInt(data.unreadmessages);

            if (unreadMessages > 0) {
                $('#UnreadMessagesCountBadge').html(unreadMessages);
                $('#UnreadMessagesCountBadge').show(150);
            }
            else $('#UnreadMessagesCountBadge').hide(150);

            if (!!parsedData && parsedData.length > 0) {
                LoadMessagesToMessenger(parsedData);
                ScrollToBottomOfMessageBox('UserMessageBoardMessagesExchangedWrapper');
            }


        });
    }
}

function LoadMessagesToMessenger(data) {
    FullPageLoaderShow();
    $('#UserMessageBoardMessagesExchangedWrapper').html('');
    $.each(data, function (index, element) {

        let row = document.createElement("div");
        $(row).attr('class', 'row pb-2');

        let col1 = document.createElement("div");
        $(col1).attr('class', 'col-sm-12 col-md-12 col-lg-6 MontserratBold');
        let col1p = document.createElement("p");
        $(col1p).attr('class', 'm-0');
        let col1img = document.createElement("img");
        $(col1img).attr('class', 'img-fluid rounded-circle mr-1');
        $(col1img).attr('width', '20px');
        $(col1img).attr('src', element.SentByProfilePicture);
        let col1span = document.createElement("span");
        $(col1span).html(element.SentByName);
        $(col1p).append(col1img);
        $(col1p).append(col1span);
        $(col1).append(col1p);

        let col2 = document.createElement("div");
        $(col2).attr('class', 'col-sm-12 col-md-12 col-lg-6 text-right text-muted');
        $(col2).html(element.ParsedDateSent);

        let col3 = document.createElement("div");
        $(col3).attr('class', 'col-sm-12 col-md-12 col-lg-12 py-1');
        let col3p = document.createElement("div");
        if (IsEmptyString(element.ParsedDateRead)) {
            $(row).attr('title', 'New message');
            $(col3p).attr('class', 'm-0 p-1 text-smaller');
        }
        else {
            $(row).attr('title', 'Read message');
            $(col3p).attr('class', 'm-0 p-1 text-smaller bg-read-message');
        }
        $(col3p).html(element.Content);
        $(col3).append(col3p);

        let col4 = document.createElement("div");
        $(col4).attr('class', 'col-sm-12 col-md-12 col-lg-12');

        let hr = document.createElement("hr");
        $(hr).attr('class', 'm-0');
        $(col4).append(hr);


        $(row).append(col1);
        $(row).append(col2);
        $(row).append(col3);
        $(row).append(col4);

        $('#UserMessageBoardMessagesExchangedWrapper').append(row);
    });
    FullPageLoaderHide();
}

function ScrollToBottomOfMessageBox(elementId) {
    var elementToScroll = $('#' + elementId);
    var heightToScroll = elementToScroll[0].scrollHeight;

    elementToScroll.scrollTop(heightToScroll);
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

                ShowToast("You have been successfully logged out!", function () { document.location.reload(); });
                
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



function SwitchProfile(toProfile, redirectUrl = null) {
    FullPageLoaderShow();
    $.ajax({
        url: $('#SwitchProfileURL').val(),
        type: 'POST',
        dataType: 'JSON',
        data: {
            Profile: toProfile
        },
        success: function (data) {
            let success = data.status;
            if (success) {
                FullPageLoaderHide();
                if (redirectUrl === null) {
                    document.location.reload();
                }
                else {
                    document.location.href = redirectUrl;
                }

            }
            else {
                FullPageLoaderHide();

                Swal.fire({ title: '' + data.messageTitle, text: '' + data.message, type: 'error' });
            }
        }
    });
}





function WebsiteCloseAllModalsBeforeOpeningAnotherModal() {
    $('.modal').modal('hide');
}

function OpenNotSignedInModal(message, showFooter = true) {
    WebsiteCloseAllModalsBeforeOpeningAnotherModal();
    $('#errorMessageNotSignedInModal').html(message);
    if (showFooter) $('#ModalNotSignedInFooter').show(0);
    else $('#ModalNotSignedInFooter').hide(0);
    $('#NotSignedInModal').modal('show');
}

function OpenAdminNotAllowedModal() {
    WebsiteCloseAllModalsBeforeOpeningAnotherModal();
    $('#NotEnabledForAdminModal').modal('show');
}

function OpenNotEnabledForBettorModal() {
    WebsiteCloseAllModalsBeforeOpeningAnotherModal();
    $('#NotEnabledForBettorModal').modal('show');
}

function OpenActionNotAllowedModal(errorMessage) {
    WebsiteCloseAllModalsBeforeOpeningAnotherModal();
    $('#errorMessageNotAllowedActionModal').html(errorMessage);
    $('#NotAllowedActionModal').modal('show');
}
function OpenNotAllowedCustomActionModal(errorMessage) {
    WebsiteCloseAllModalsBeforeOpeningAnotherModal();
    $('#errorMessageNotAllowedCustomActionModal').html(errorMessage);
    $('#NotAllowedCustomActionModal').modal('show');
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
    let TheDataTable = $('#' + tableId).DataTable({
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
            lengthMenu: '<span class="text-dark text-smaller">Page Size </span> <select id="' + selectId + '" class="btn-xs-block general-form-select text-smaller">' +
                '<option value="10">10</option>' +
                '<option value="25">25</option>' +
                '<option value="50">50</option>' +
                '<option value="100">100</option>' +
                '</select>'
        },
        order: orderObj
    });
}


function InsertSiteTimezoneOffsetRequest(element) {
    window.location.href = insertParameter($(element).prop('href'), 'TimeOffset', new Date().getTimezoneOffset());
}

const insertParameter = (url, key, value) => {
    try {
        if (url.includes('?')) {
            url += `&${key}=${value}`;
        } else {
            url += `?${key}=${value}`;
        }
        return url;
    } catch (e) {
        return url;
    }
};


function SilentAJAXpost(url, dataObj, reload = false, redirectUrl = null, customCallback = null) {
    try {
        FullPageLoaderShow();

        $.ajax({
            url: url,
            type: 'POST',
            dataType: 'JSON',
            data: dataObj,
            success: function (data) {
                FullPageLoaderHide();
                if (customCallback === null) {
                    let success = data.status;
                    if (success) {
                        if (reload) {
                            document.location.reload();
                        }
                        else {
                            if (!(redirectUrl === null)) {
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
        console.log(err);
        console.log('-----------ENDOF Fatal Error------------');
    }
}






function AJAXpost(url, dataObj, reload = false, redirectUrl = null, customCallback = null) {
    try {
        FullPageLoaderShow();
        //console.log("i anm here");
        $.ajax({
            url: url,
            type: 'POST',
            dataType: 'JSON',
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


function StartTimer(element) {
    let countDownDate = new Date($(element).data('endtime'));
    // Update the count down every 1 second
    var x = setInterval(function () {

        // Get today's date and time
        var now = new Date().getTime();

        // Find the distance between now and the count down date
        var distance = countDownDate - now;

        // Time calculations for days, hours, minutes and seconds
        var days = Math.floor(distance / (1000 * 60 * 60 * 24));
        var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60)) + (days * 24);
        var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
        var seconds = Math.floor((distance % (1000 * 60)) / 1000);

        if (hours < 10) {
            hours = "0" + hours;
        }
        if (minutes < 10) {
            minutes = "0" + minutes;
        }
        if (seconds < 10) {
            seconds = "0" + seconds;
        }


        // Output the result in an element with id="demo"
        $(element).html(hours + ":" + minutes + ":" + seconds);

        // If the count down is over, write some text 
        if (distance < 0) {
            clearInterval(x);
            $(element).html("EXPIRED");
        }
    }, 1000);
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



        // If the count down is over, write some text 
        if (distance < 0) {
            clearInterval(knockoutInterval);
            LogoutAction();
        }
    }, 1000);
}



const sendFormRequest = (url, json) => {
    var form = document.createElement('form');
    form.method = 'POST';
    form.action = url;
    form.style.display = "none";
    for (var v in json) {
        var input = document.createElement('input');
        input.name = v;
        input.value = json[v];
        form.appendChild(input);
    }
    document.body.appendChild(form);
    form.submit();
};



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

function setCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
}
function getCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}
function eraseCookie(name) {
    document.cookie = name + '=; Max-Age=-99999999;';
}

function ShowToast(message, callback = null) {
    $("#ShortchaseToastBody").html(message);
    $("#ShortchaseToastWrapper").show(150, function () {
        if (callback != null) {
            window.setTimeout(callback, 1000);
        }
    });
    
}
function ShowDangerToast(message, callback = null) {
    $("#ShortchaseDangerToastBody").html(message);
    $("#ShortchaseDangerToastWrapper").show(150, function () {
        if (callback != null) {
            window.setTimeout(callback, 1000);
        }
    });
    
}


function HideToast() {
    $("#ShortchaseToastBody").html("");
    $("#ShortchaseToastWrapper").hide(150);
}

function HideDangerToast() {
    $("#ShortchaseDangerToastBody").html("");
    $("#ShortchaseDangerToastWrapper").hide(150);
}

function ResizeAllCardsAccordingly(){
    let maxHeight = 0;
    $('.cardColHeightAdjustmentForcedByEdge').each(function(index, element) {
        let currentElementHeight = parseInt($(element).height());
        if (currentElementHeight > maxHeight) {
            maxHeight = currentElementHeight;
        }
    });

    $('.cardColHeightAdjustmentForcedByEdge').height(maxHeight);
}