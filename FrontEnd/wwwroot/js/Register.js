$('#formRegister').find('#registerSpinner').hide();
$('#formRegister').on('click', '#registerButton', {}, function () {
    if (!inputCompleto($('#inputUsername'))) {
        return;
    }
    if (!inputCompleto($('#inputPassword'))) {
        return;
    }
    if (!inputCompleto($('#confirmPassword'))) {
        return;
    }
    if (!checkPasswordAreEquals($('#inputPassword'), $('#confirmPassword'))) {
        return;
    }

    let uriLogin = 'https://localhost:7045/api/Usuario';
    let data = JSON.stringify({
        "user": $('#formRegister').find('#inputUsername').val(),
        "password": $('#formRegister').find('#inputPassword').val(),
        "confirmarPassword": $('#formRegister').find('#confirmPassword').val()
    });
    $('#formRegister').find('#registerSpinner').show();
    $.ajax({
        url: uriLogin,
        type: "POST",
        data: data,
        contentType: "application/json; charset=utf-8",
    })
        .done(function (response) {
            $('#formRegister').find('#registerSpinner').hide();
            window.location.href = "/Login"
        })
        .fail(function (response) {
            $('#formRegister').find('#registerSpinner').hide();
            window.location.href = "/Error"
        })

});