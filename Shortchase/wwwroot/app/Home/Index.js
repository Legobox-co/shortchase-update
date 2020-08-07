let trendingIterationInterval = null;
let potdSlideShowInterval = null;

$(document).ready(function () {
    GetTrendingData();
    GetPOTDData();

    $(".imageTextandBackgroundColor").mouseenter(function () {
        $(this).css({ "background-color": "#FCD015A6", "text-align": "left", "opacity": "1" });
   
    });

    $(".imageTextandBackgroundColor").mouseleave(function () {
        $(this).css({ "background-color": "transparent", "text-align": "center", "width": "243px" });
    });
    $(".circle").mouseenter(function () {
        $(this).css({ "background-color": "rgba(255,187,0,0.5)" });
    });
    $(".circle").mouseleave(function () {
        $(this).css({ "background-color": "transparent"});
    });

    $(".search-nav").change(function () {
        $(this).css({ "color": "#555555" });
    });

    $(".marketplace-hr-hover").on('mouseenter', function () {
        //alert('i am here');
        var line = $(this).find(".marketplace-hr-hover-change-color");
        line.css({"width":"80px"});
        line.css({ "border-color": "#FFBE0C" })
    });
    $(".marketplace-hr-hover").on('mouseleave', function () {
        //alert('i am here');
        var line = $(this).find(".marketplace-hr-hover-change-color");
        line.css({ "width": "38px" });
        line.css({ "border-color": "#31303852" })
    })

    $(document).on('click', '.marketplace-item-link', function (e) {
        e.preventDefault();
        let filters = {
            Category: $(this).data('id'),
            TimeOffset: new Date().getTimezoneOffset()
        };
        const url = $('#SearchResultsUrl').val();

        loadFilteredPage(url, filters);
    });
    $("#forCappersAndBettorsSwitch").css({ "height": "94px", "width": "500px" });
    
   
   let cappersAndBettorsMinHeight = $('#forCappersAndBettorsDynamicContent').height();
   // let cappersAndBettorsSectionHeight = $('#forCappersAndBettorsSectionWrapper').height();

    $(document).on('click', '#forCappersSwitchActivator', function (e) {
        e.preventDefault();
        $('#forCappersAndBettorsSwitch').css('background', 'purple');
        $('#ForCarpers').css('display', 'block');
        $('#ForBettors').css('display', 'none');
        $('#CarperAndBettorImage').attr("src", "img/potd/cappers.jpg");
       
    });


    $(document).on('click', '#forBettorsSwitchActivator', function (e) {
        e.preventDefault();
        
        $('#forCappersAndBettorsSwitch').css('background', 'rgb(152, 0, 0)');
       
        $('#ForCarpers').css('display', 'none');
        $('#ForBettors').css({ 'display': 'block', 'color': '#323139' });
        $('#CarperAndBettorImage').attr("src", "img/potd/bettors.jpg");
        
    });


    let testimonialHeight = $('#TestimonialsDynamicContent').height() + 15;

    $(document).on('click', '#testimonialSelector_1', function (e) {
        e.preventDefault();
        $('#testimonialSelector_2').removeClass('testimonialPictureActive');
        $(this).addClass('testimonialPictureActive');
        $('#TestimonialsDynamicContent').css('min-height', testimonialHeight + 'px');
        $('#testimonialContent_2').hide(0, function () {
            $('#testimonialContent_1').show(0);
        });
    });


    $(document).on('click', '#testimonialSelector_2', function (e) {
        e.preventDefault();
        $('#testimonialSelector_1').removeClass('testimonialPictureActive');
        $(this).addClass('testimonialPictureActive');
        $('#TestimonialsDynamicContent').css('min-height', testimonialHeight + 'px');
        $('#testimonialContent_1').hide(0, function () {
            $('#testimonialContent_2').show(0);
        });
    });




    $(document).on('click', '.previous-potd-item', function (e) {
        e.preventDefault();
        let ShouldIterateOverPOTD = parseInt($('#ShouldIterateOverPOTD').val()) === 1 && parseInt($('#TotalPOTDItems').val()) > 1;
        if (ShouldIterateOverPOTD) MoveToPOTDItem('down');
        
    });



    $(document).on('click', '.next-potd-item', function (e) {
        e.preventDefault();
        let ShouldIterateOverPOTD = parseInt($('#ShouldIterateOverPOTD').val()) === 1 && parseInt($('#TotalPOTDItems').val()) > 1;
        if (ShouldIterateOverPOTD) MoveToPOTDItem('up');
    });


});


function MoveToPOTDItem(direction) {
    let totalItems = parseInt($('#TotalPOTDItems').val());
    let currentItem = parseInt($('#CurrentPOTDItem').val());
    let itemToHide = currentItem;
    if (direction === 'down') {
        if (currentItem <= 1) {
            currentItem = totalItems;
        }
        else {
            currentItem -= 1;
        }
    }
    else {
        if (currentItem >= totalItems) {
            currentItem = 1;
        }
        else {
            currentItem += 1;
        }
    }
    $('#CurrentPOTDItem').val(currentItem);
    let newBackground = $('#potd-item-' + currentItem).data('bg');
    if (typeof newBackground === "undefined" || newBackground.trim() === "") {
      //  $('#potd-bg-changer').css('background', "url('../img/potd/default-bg.jpg')");
    }
    else {
       // $('#potd-bg-changer').css('background', "url('" + newBackground+"')");
    }
    $('#potd-item-' + itemToHide).hide(150, function () {
        $('#potd-item-' + currentItem).show(150);
    });
}


function GetTrendingData() {
    let url = $('#IndexPartTrendingUrl').val();
    let obj = {
        TimeOffset: new Date().getTimezoneOffset()
    };
    FullPageLoaderShow();
    $.ajax({
        url: url,
        type: 'POST',
        data: obj,
        success: function(data) {
            FullPageLoaderHide();
            $('#TrendingSection').html(data);
            TrendingIteration();
        },
        error: function(err) {
            FullPageLoaderHide();

            Swal.fire({ title: 'Error on Asynchronous call', text: 'Check console for more information', type: 'error' });
            console.log('-----------Error on Asynchronous call------------');
            console.log(err);
            console.log('-----------ENDOF Error on Asynchronous call------------');
        }

    });
}

function TrendingIteration() {
    let ShouldIterateOverTrendingItems = parseInt($('#ShouldIterateOverTrendingItems').val()) === 1 && parseInt($('#TrendingItemIteratorTotal').val()) > 1;
    if (ShouldIterateOverTrendingItems) {
        trendingIterationInterval = window.setInterval(function() {
            let currentItem = parseInt($('#TrendingItemIterator').val());
            let currentItemId = "#trendingItem_" + currentItem;
            let totalItems = parseInt($('#TrendingItemIteratorTotal').val());
            let nextItem = 0;
            if (currentItem === totalItems - 1) {
                nextItem = 0;
            }
            else {
                nextItem = currentItem + 1;
            }
            let nextItemId = "#trendingItem_" + nextItem;

            $(currentItemId).hide(150, function() {
                $('#TrendingItemIterator').val(nextItem);
                $(nextItemId).show(150);
            });
        }, 5000);
    }
    else return false;
    
}


function GetPOTDData() {
    let url = $('#IndexPartPOTDUrl').val();
    let obj = {
        TimeOffset: new Date().getTimezoneOffset()
    };
    FullPageLoaderShow();
    $.ajax({
        url: url,
        type: 'POST',
        data: obj,
        success: function(data) {
            $('#POTDPartWrapper').html(data);

            let currentItem = parseInt($('#CurrentPOTDItem').val());
            let newBackground = $('#potd-item-' + currentItem).data('bg');
            if (typeof newBackground === "undefined" || newBackground.trim() === "") {
               // $('#potd-bg-changer').css('background', "url('../img/potd/default-bg.jpg').css('background-repeat','no-repeat')");
            }
            else {
              //  $('#potd-bg-changer').css('background', "url('" + newBackground + "')");
            }

            let ShouldIterateOverPOTD = parseInt($('#ShouldIterateOverPOTD').val()) === 1 && parseInt($('#TotalPOTDItems').val()) > 1;
            if (ShouldIterateOverPOTD) {
                potdSlideShowInterval = window.setInterval(function() { MoveToPOTDItem("up"); }, 5000);
            }

            FullPageLoaderHide();
        },
        error: function(err) {
            FullPageLoaderHide();

            Swal.fire({ title: 'Error on Asynchronous call', text: 'Check console for more information', type: 'error' });
            console.log('-----------Error on Asynchronous call------------');
            console.log(err);
            console.log('-----------ENDOF Error on Asynchronous call------------');
        }

    });
}
