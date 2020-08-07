$(document).ready(function () {

    /*
    $('#AppLogo').change(function () {
        var filename = $('#AppLogo').val().split('\\').pop();
        if (filename.trim() === "") filename = 'Select Backend App Logo';
        $('#AppLogoLabel').html(filename);
    });
    */

    $('.UpdateAppConfigBtn').click(function (e) {
        e.preventDefault();


        var obj = {};
        obj.AppName = $('#AppName').val();
        obj.AppTagline = $('#AppTagline').val();
        obj.AppLogo = $('#AppLogo').val();

        /*var fileInput = document.getElementById('AppLogo');
        if (fileInput.files.length > 0) {
            obj.AppLogo = {
                File: fileInput.files[0],
                Name: fileInput.files[0].name
            };
        }*/

        if (IsEmptyString(obj.AppName)) {
            return Swal.fire({
                title: 'Form Incomplete!',
                text: 'App Name is mandatory.',
                type: 'error',
                showCancelButton: false
            });
        }

        if (IsEmptyString(obj.AppTagline)) {
            return Swal.fire({
                title: 'Form Incomplete!',
                text: 'App Tagline is mandatory.',
                type: 'error',
                showCancelButton: false
            });
        }

        let LogoHasValueAlready = $('#LogoHasValueAlready').val().toLowerCase() === "true";


        if (IsEmptyString(obj.AppLogo) && !LogoHasValueAlready) {
            return Swal.fire({
                title: 'Form Incomplete!',
                text: 'App Logo is mandatory.',
                type: 'error',
                showCancelButton: false
            });
        }

        //console.log(obj);
        SubmitAppConfigData(obj);


    });


});

function SubmitAppConfigData(obj) {

    var formData = new FormData();

    formData.append('configs.AppName', obj.AppName);
    formData.append('configs.AppTagline', obj.AppTagline);
    formData.append('configs.AppLogo', obj.AppLogo);
    /*if (obj.AppLogo === null) formData.append('configs.AppLogo', null);
    else formData.append('configs.AppLogo', obj.AppLogo.File, obj.AppLogo.Name);*/

    let url = $('#UpdateAppConfigURL').val();
    AJAXFilePost(url, formData, true);
}