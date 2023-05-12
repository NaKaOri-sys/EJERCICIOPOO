$('#modalError').find('#crossButton').on('click', {}, function () {
    $('#modalError').hide();
    return;
}); $('#modalError').find('#closeButton').on('click', {}, function () {
    $('#modalError').hide();
    return;
});
$('#btnShow').on('click', function () {
    if ($('#reportesList option:selected').val() === 'default') {
        alert('Se debe seleccionar un reporte para continuar.');
        return;
    }
    else {
        $('#hiddenSelect').val($('#reportesList option:selected').text());
        $.ajax({
            url: '/Reportes',
            success: function (data) {
                $('#content').html(data);
            },
            error: function (xhr, status) {
                if (status === 401) {
                    $('#modalError').show();
                    return;
                }
                alert("Sorry, there was a problem!");
            }
        });
    }
});