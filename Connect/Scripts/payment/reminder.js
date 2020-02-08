$(document).ready(function () {
    $(document).on('click', ".button-remind-all-incomplete", function (e) {
        $(".ajax_loader").show();
        e.preventDefault();

        $.ajax({
            url: 'remind-all-incomplete',
            method: "post",
            dataType: "html",
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $("#divUnverifiedPayments").html(data);
            },
            error: function () {
                alert("There was a problem. Please retry or contact your IT team");
            }
        });
    });

    $(document).on('click', ".button-remind-single", function (e) {
        $(".ajax_loader").show();
        e.preventDefault();
        var id = $(this).attr("identifier");

        var data = JSON.stringify({
            'id': id
        });

        $.ajax({
            url: 'remind-single-ajax',
            method: "post",
            dataType: "html",
            contentType: 'application/json; charset=utf-8',
            data: data,
            success: function (data) {
                $("#divUnverifiedPayments").html(data);
            },
            error: function () {
                alert("There was a problem. Please retry or contact your IT team");
            }
        });
    });

});

