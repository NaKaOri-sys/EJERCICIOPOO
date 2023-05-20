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

    $('#formRegister').find('#registerSpinner').show();
    $.ajax({
        url: "/Register"
    })

});