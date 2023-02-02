$('#formLogin').find('#loginSpinner').hide();
$('#formLogin').on('click', '#loginButton', {}, function () {
    let uriLogin = 'https://localhost:7045/api/Login';
    let data = JSON.stringify({
        "user": $('#formLogin').find('#inputUsername').val(),
        "password": $('#formLogin').find('#inputPassword').val()
    });
    $('#formLogin').find('#loginSpinner').show();
    $.ajax({
        url: uriLogin,
        type: "POST",
        data: data,
        contentType: "application/json; charset=utf-8",
    })
        .done(function (response) {
            window.sessionStorage.setItem("bearer_token", response);
            $('#formLogin').find('#loginSpinner').hide();
            window.location.href = "/Reportes"
        })
        .fail(function (response) {
            $('#formLogin').find('#loginSpinner').hide();
            window.location.href = "/Error"
        })
        
});
