$('#reportesList').on('change', function () {
    let bearer = sessionStorage.getItem("bearer_token")
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
        },
        complete: function (xhr, status) {
            //$('#showresults').slideDown('slow')
        }
    });
});