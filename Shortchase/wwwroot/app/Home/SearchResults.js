$(document).ready(function () {
    $('#MoneyRangeFilter').on('input', function () {
        let currentValue = parseFloat($(this).val());
        let minValue = parseFloat($(this).attr('min'));
        let maxValue = parseFloat($(this).attr('max'));
        let average = (maxValue + minValue) / 2;
        if (currentValue <= average) {
            $('#MoneyMinFilter').val(currentValue.toFixed(2));
        }
        else {
            $('#MoneyMaxFilter').val(currentValue.toFixed(2));
        }
    });


    $('.money-discrete-validator').change(function () {
        let currentValue = parseFloat($(this).val());
        let minValue = parseFloat($(this).attr('min'));
        let maxValue = parseFloat($(this).attr('max'));
        if (currentValue >= maxValue) {
            $(this).val(maxValue.toFixed(2));
        }
        else if (currentValue <= minValue) {
            $(this).val(minValue.toFixed(2));
        }
        else {
            $(this).val(currentValue.toFixed(2));
        }
    });




    $('.ToggleFiltersMobile').click(function (e) {
        e.preventDefault();
        if ($('#sidebar').hasClass("d-none d-sm-none")) {
            $('#sidebar').removeClass("d-none d-sm-none");
        }
        else {
            $('#sidebar').addClass("d-none d-sm-none");
        }
    });


    $('.ToggleSortersMobile').click(function (e) {
        e.preventDefault();
        if ($('#sorters').hasClass("d-none d-sm-none")) {
            $('#sorters').removeClass("d-none d-sm-none");
        }
        else {
            $('#sorters').addClass("d-none d-sm-none");
        }
    });


    $('.ClearAllFilters').click(function (e) {
        e.preventDefault();

        let filters = {
            Category: null,
            SubCategory: null,
            Keyword: null,
            Odds: null,
            PriceMin: null,
            PriceMax: null,
            SortBy: null,
            SortByPickType: null,
            SortByPrice: null,
            SortByOdds: null,
            SortByCurrency: null,
            TimeOffset: new Date().getTimezoneOffset()
        };
        const url = $('#SearchResultsUrl').val();

        loadFilteredPage(url, filters);
    });


    $('.filters-change-listener').change(function () {
        let filters = GetFilterValues();

        const url = $('#SearchResultsUrl').val();

        loadFilteredPage(url, filters);
    });

    $('.pick-link').click(function () {
        let filters = {
            Listing: $(this).data('listing'),
            TimeOffset: new Date().getTimezoneOffset()
        };

        const url = $('#ViewListingUrl').val();

        loadFilteredPage(url, filters);
    });


    $(document).on('click', '.buy-now-link', function (e) {
        e.preventDefault();

        let obj = {
            Id: $(this).data('listing')
        };

        let url = $('#AddItemCartUrl').val();

        AJAXpost(url, obj, false, null, function (data) {
            if (data.status) {
                $('#ContinueShoppingModal').modal('show');
            }
            else {
                ShowDangerToast("<i class='fas fa-exclamation-circle'></i> You already added this Pick to your cart", function () {
                    HideDangerToast();
                });
            }
        });
    });


    $(document).on('click', '.unauth-user-click', function (e) {
        e.preventDefault();
        OpenNotSignedInModal("You can buy listings only after signing up/sign-in.");
    });


    $(document).on('click', '.unauth-actiontoself-link', function (e) {
        e.preventDefault();
        OpenActionNotAllowedModal("You cannot buy your own listings!");
    });


    $('.countDownWrapper').each(function (i, element) {
        StartTimer(element);
    });

    ResizeAllCardsAccordingly();
});


function GetFilterValues() {
    let filters = {};
    $('.filters-marketplace').each(function (index, element) {
        if (!$(element).val()) filters[$(element).data('filter')] = null;
        else filters[$(element).data('filter')] = $(element).val();

    });
    filters.TimeOffset = new Date().getTimezoneOffset();
    return filters;
}