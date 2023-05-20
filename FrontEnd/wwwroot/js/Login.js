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
        url: '/Login'
    })

});