$('#formLogin').find('#loginSpinner').hide();
$('#formLogin').on('click', '#loginButton', {}, function () {
    if (!inputCompleto($('#inputUsername'))) {
        return;
    }
    if (!inputCompleto($('#inputPassword'))) {
        return;
    }
    $('#formLogin').find('#loginSpinner').show();
    $.ajax({
        url: '/Login',
        success: function (response, textStatus, xhr) {
            $('#formLogin').find('#loginSpinner').hide();
            if (xhr.getResponseHeader('X-IsAuthenticated') === 'true') {
                window.location.href = "/Reportes";
            } else {
                window.location.href = "/Error";
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            $('#formLogin').find('#loginSpinner').hide();
            window.location.href = "/Error";
        }
    })

});