$(document).ready(function () {

    $('#EditPostContent').summernote();

    $('#EditPostTitle').on('keyup paste input change', function () {
        let value = $(this).val();
        if (!value || value === "" || value.trim() === "") $('#EditPostSlug').val("");
        else $('#EditPostSlug').val(slugify(value));
    });



    $('#EditPostFeaturedImage').change(function () {
        var filename = $('#EditPostFeaturedImage').val().split('\\').pop();
        if (filename.trim() === "") {
            filename = 'Select featured image';
            $('#initialFeaturedImagePreview').show(150);
        }
        else $('#initialFeaturedImagePreview').hide(150);
        $('#pictureLabel').html(filename);
    });

    
    $('#EditPostForm').submit(function (e) {
        e.preventDefault();


        var obj = {};
        obj.Id = $('#EditPostId').val();
        obj.Title = $('#EditPostTitle').val();
        obj.Slug = $('#EditPostSlug').val();
        obj.DatePublished = $('#EditPostDatePublished').val();
        obj.Content = $('#EditPostContent').val();
        obj.Excerpt = $('#EditPostExcerpt').val();
        obj.File = $('#EditPostFeaturedImage').val();
        
        
        if (obj.DatePublished === "") {
            return Swal.fire({
                title: 'Form Incomplete!',
                text: 'You need to provide a date for the post to be published.',
                type: 'error',
                showCancelButton: false
            });
        }
        if (!obj.Content || obj.Content === "" || obj.Content.trim() === ""  ) {
            return Swal.fire({
                title: 'Form Incomplete!',
                text: 'You need to provide content for the post to be published.',
                type: 'error',
                showCancelButton: false
            });
        }

        let isTimeToSubmit = false;

        if (IsEmptyString(obj.File) && parseInt($('#PostHasPictureAlready').val()) === 0) {
            Swal.fire({
                title: 'Are you sure you want to continue without a featured image?',
                text: '',
                type: 'warning',
                showCancelButton: true,
                confirmButtonText: "Yes",
                cancelButtonText: "No"
            }).then((result) => {
                if (result.value) {
                    SubmitNewPostData(obj);
                }
            });
        }
        else {
            isTimeToSubmit = true;
        }

        
        if (isTimeToSubmit) {
            SubmitNewPostData(obj);
        }
        
    });



});

function SubmitNewPostData(obj) {

    var formData = new FormData();

    formData.append('Post.Id', obj.Id);
    formData.append('Post.Title', obj.Title);
    formData.append('Post.Slug', obj.Slug);
    formData.append('Post.DatePublished', obj.DatePublished);
    formData.append('Post.Content', obj.Content);
    formData.append('Post.Excerpt', obj.Excerpt);
    formData.append('Post.IsPublished', obj.IsPublished);
    formData.append('Post.File', obj.File);
    formData.append('Post.TimezoneOffset', new Date().getTimezoneOffset());

    let url = $('#UpdatePromotionPostURL').val();
    let redirectUrl = $('#PromotionIndexURL').val();

    
    AJAXFilePost(url, formData, false, redirectUrl);
}