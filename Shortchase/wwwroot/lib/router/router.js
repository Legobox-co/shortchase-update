$(document).ready(function () {
    $(document).on('click', '.backend-nav-item', function(e){
        e.preventDefault();
        let id = $(this).attr('id');
        InsertTimezoneOffsetRequest(id);
    });

});

function InsertTimezoneOffsetRequest(id) {
    let elementId = '#' + id;
    window.location.href = insertParameter($(elementId).prop('href'), 'TimeOffset', new Date().getTimezoneOffset());
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