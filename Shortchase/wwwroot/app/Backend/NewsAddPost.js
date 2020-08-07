$(document).ready(function () {

    $('#AddPostContent').summernote();

    $('#AddPostTitle').on('keyup paste input change', function () {
        let value = $(this).val();
        if (!value || value === "" || value.trim() === "") $('#AddPostSlug').val("");
        else $('#AddPostSlug').val(slugify(value));
    });

    $('#AddPostIsPublished').change(function () {
        let isChecked = $(this).prop('checked');
        if (isChecked) {
            $('#PostDatePublishedWrapper').hide(150);
            $('#AddPostDatePublished').val("");
        }
        else {
            $('#PostDatePublishedWrapper').show(150);
        }
    });



    
    $('#AddPostForm').submit(function (e) {
        e.preventDefault();


        var obj = {};
        obj.Title = $('#AddPostTitle').val();
        obj.Slug = $('#AddPostSlug').val();
        obj.DatePublished = $('#AddPostDatePublished').val();
        obj.Content = $('#AddPostContent').val();
        obj.IsPublished = $('#AddPostIsPublished').prop('checked');
        obj.Excerpt = $('#AddPostExcerpt').val();
        obj.File = $('#AddPostFeaturedImage').val();
        
        

        if (!obj.IsPublished && obj.DatePublished === "") {
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

        if (IsEmptyString(obj.File)) {
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

    formData.append('Post.Title', obj.Title);
    formData.append('Post.Slug', obj.Slug);
    formData.append('Post.DatePublished', obj.DatePublished);
    formData.append('Post.Content', obj.Content);
    formData.append('Post.Excerpt', obj.Excerpt);
    formData.append('Post.IsPublished', obj.IsPublished);
    formData.append('Post.File', obj.File);
    formData.append('Post.TimezoneOffset', new Date().getTimezoneOffset());
    
    let url = $('#CreateNewNewsPostURL').val();
    let redirectUrl = $('#NewsIndexURL').val();
    AJAXFilePost(url, formData, false, redirectUrl);
}