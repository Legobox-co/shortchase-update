$(document).ready(function () {

    $(document).on('click', '.toggleHiddenAreaOnClick', function (e) {
        e.preventDefault();
        let toggleId = $(this).data('area');
        let iconClass = $(this).data('icon');
        $('.' + iconClass).each(function (index, element) {
            if ($(element).hasClass("fa-chevron-down")) {
                $(element).removeClass('fa-chevron-down');
                $(element).addClass('fa-chevron-up');
            }
            else {
                $(element).removeClass('fa-chevron-up');
                $(element).addClass('fa-chevron-down');
            }
        });
        $('#' + toggleId).toggle(150);
    });

    $('.FollowUserBtn').click(function (e) {
        e.preventDefault();

        let obj = {
            UserToFollowId: $(this).data('to')
        }
        let url = $('#FollowUserURL').val();
        AJAXpost(url, obj, true);
    });

    $('.UnfollowUserBtn').click(function (e) {
        e.preventDefault();

        let obj = {
            UserToUnFollowId: $(this).data('to')
        }
        let url = $('#UnfollowUserURL').val();
        AJAXpost(url, obj, true);
    });

    $('.StarIconHover').hover(
        function () {
            let number = parseInt($(this).data('number'));
            $('.StarIconHover').each(function (i, el) {
                if (parseInt($(el).data('number')) <= number) {
                    $(this).removeClass('text-grey-light-2');
                    $(this).addClass('text-yellow-light');
                }
            });
        },
        function () {
            let number = parseInt($(this).data('number'));
            $('.StarIconHover').each(function (i, el) {
                if (parseInt($(el).data('number')) <= number) {
                    $(this).removeClass('text-yellow-light');
                    $(this).addClass('text-grey-light-2');
                }
            });
        }
    );



    $('.EvaluateUser').click(function (e) {
        e.preventDefault();

        let obj = {
            UserToEvaluateId: $(this).data('user'),
            Rating: $(this).data('number')
        }
        let url = $('#EvaluateUserURL').val();
        AJAXpost(url, obj, true);
    });

    $('.ReEvaluateUser').click(function (e) {
        e.preventDefault();

        let obj = {
            RatingId: $(this).data('rating'),
            RatingNewValue: $(this).data('number')
        }
        let url = $('#ReEvaluateUserURL').val();
        AJAXpost(url, obj, true);
    });
});