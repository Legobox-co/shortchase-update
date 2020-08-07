$(document).ready(function () {

    $('.FinishAndSaveCropBtn').click(function (e) {
        e.preventDefault();

        let obj = {
            Id: $(this).data('id'),
            CroppedMedia: cropper.getCroppedCanvas().toDataURL(),
            CroppedMediaHeight: parseFloat($('#CropHeight').html().trim()),
            CroppedMediaWidth: parseFloat($('#CropWidth').html().trim())
        };
        let url = $('#CropMediaURL').val();
        AJAXpost(url, obj, true);
    });

    const image = document.getElementById('imageCropper');
    const cropper = new Cropper(image, {
        aspectRatio: 16 / 9,
        crop(event) {
            //console.log(event.detail.x);
            //console.log(event.detail.y);
            $('#CropWidth').html(event.detail.width.toFixed(2));
            $('#CropHeight').html(event.detail.height.toFixed(2));
            //console.log(event.detail.rotate);
            //console.log(event.detail.scaleX);
            //console.log(event.detail.scaleY);
        },
    });


});