$('#modalError').find('#crossButton').on('click', {}, function () {
    $('#modalError').hide();
    return;
}); $('#modalError').find('#closeButton').on('click', {}, function () {
    $('#modalError').hide();
    return;
});
let cookie = getCookieValue("Token");
$('#btnShow').on('click', function () {
    if (cookie === null) {
        $('#modalError').show();
        return;
    }
    if ($('#reportesList option:selected').val() === 'default') {
        alert('Se debe seleccionar un reporte para continuar.');
        return;
    }
    else {
        $('#HiddenInput').val($('#reportesList option:selected').text());
        $.ajax({
            url: '/Reportes?handler=Reporte&ID=' + $('#HiddenInput').val(),
            success: function (data) {
                $('#resultado').html(data);
            },
            error: function (xhr, status, errorThrown) {
                alert("Sorry, there was a problem!" + errorThrown);
            }
        });
    }
});