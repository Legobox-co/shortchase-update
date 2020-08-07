$(document).ready(function () {

    $(document).on('click', '.ActionNotAllowedNotSignedIn,.unauth-user-click', function (e) {
        e.preventDefault();
        OpenNotSignedInModal("You can buy a pick only after signing up");
    });
    $(document).on('click', '.ReportThisListing', function (e) {
        e.preventDefault();
        let id = $(this).data('id');
        $('#ReportListingIdValue').val(id);
        //$('#ReportContent').summernote();
        $('#ReportThisListingModal').modal('show');
    });

    $('#ReportThisListingModal').on('hidden.bs.modal', function (e) {
        //$('#ReportContent').summernote('destroy');
        $('#ReportSubjectValue').val("");
        $('.ReportSubjectPicker').each(function (i, element) {
            $(element).removeClass('btn-dark');
            $(element).addClass('btn-default');
        });
        $('#ReportSubjectWrapper').html('<span class="text - muted">Select one option above</span>');
        $('#ReportContentWrapper').hide(0);
    });

    $(document).on('click', '.ReportSubjectPicker', function (e) {
        e.preventDefault();
        let subject = $(this).data('subject');
        let thisElement = this;
        $('.ReportSubjectPicker').each(function (i, element) {
            $(element).removeClass('btn-dark');
            $(element).addClass('btn-default');
        });
        $(thisElement).removeClass('btn-default');
        $(thisElement).addClass('btn-dark');
        $('#ReportSubjectWrapper').html(subject);
        $('#ReportSubjectValue').val(subject);
        if (subject === "Other") $('#ReportContentWrapper').show(150);
        else $('#ReportContentWrapper').hide(150);
    });


    $(document).on('click', '.unauth-actiontoself-link', function (e) {
        e.preventDefault();
        OpenActionNotAllowedModal("You cannot buy your own listings!");
    });

    $(document).on('click', '.unauth-reportself-link', function (e) {
        e.preventDefault();
        OpenActionNotAllowedModal("You cannot report your own listings!");
    });

    $(document).on('click', '.AddItemToCartBtn', function (e) {
        e.preventDefault();
        let obj = {
            Id: $(this).data('listing'),
        };
        let url = $('#AddItemCartUrl').val();

        AJAXpost(url, obj, true);
    });

    $('.pick-link').click(function () {
        let filters = {
            Listing: $(this).data('listing'),
            TimeOffset: new Date().getTimezoneOffset()
        };

        const url = $('#ViewListingUrl').val();

        loadFilteredPage(url, filters);
    });


    $('#ListingReportModalForm').submit(function (e) {
        e.preventDefault();

        let obj = {
            ReportTopic: $('#ReportSubjectValue').val(),
            ReportedListingId: $('#ReportListingIdValue').val(),
            ReportContent: $('#ReportContent').val(),
        };
        if (IsEmptyString(obj.ReportTopic) || IsEmptyString(obj.ReportedListingId)) {
            return Swal.fire({
                title: 'Form Incomplete!',
                text: 'You need to provide a valid subject for the report!',
                type: 'error',
                showCancelButton: false
            });
        }
        else {

            if (obj.ReportTopic === "Other") {
                if (IsEmptyString(obj.ReportContent)) {
                    return Swal.fire({
                        title: 'Form Incomplete!',
                        text: 'For subject "Others" you need to provide content for the report!',
                        type: 'error',
                        showCancelButton: false
                    });
                }
            }
            let url = $('#AddBetListingReportUrl').val();
            AJAXpost(url, obj, true);
        }
    });


    $('.countDownWrapper').each(function (i, element) {
        StartTimer(element);
    });

    ResizeAllCardsAccordingly();
});

