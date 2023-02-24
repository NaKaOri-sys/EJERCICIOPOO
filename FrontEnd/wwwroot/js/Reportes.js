$('#modalError').hide();
$('#modalError').find('#crossButton').on('click', {}, function () {
    $('#modalError').hide();
    return;
}); $('#modalError').find('#closeButton').on('click', {}, function () {
    $('#modalError').hide();
    return;
});
$('#reportesList').on('change', function () {
    let bearer = sessionStorage.getItem("bearer_token")
    if (bearer === null) {
        $('#modalError').show();
        return;
    }
    $.ajax({
        url: "https://localhost:7045/api/reportes",
        data: {
            "ID": $('#reportesList option:selected').text()
        },
        headers: { "Authorization": "Bearer " + bearer },
        type: "GET",
        dataType: "html",
        success: function (data) {
            $('#content').append(data);
        },
        error: function (xhr, status) {
            alert("Sorry, there was a problem!");
        }
    });
});