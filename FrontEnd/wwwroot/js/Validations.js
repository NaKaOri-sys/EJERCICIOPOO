function inputCompleto(input) {

    var value = input.val();

    if (value === typeof ('undefined') || value == "") {
        input.focus().select();
        return false;
    }

    return true;
}

function checkPasswordAreEquals(password, confirmPassword) {

    var pass = password.val();
    var confirmPass = confirmPassword.val();
    if (pass != confirmPass) {
        alert("Las contraseñas no coinciden.");
        return false;
    }
    return true;
}