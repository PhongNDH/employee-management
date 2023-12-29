$(document).ready(function () {
    $('#download-btn').click(function () {
        $.ajax({
            url: '/Employee/ExportEmployeeToExcel',
            type: 'GET',
            data: {},
            success: function () {
                location.reload();
                // alert(data);
                // console.log("Successfully!");
            },
            error: function (error) {
                console.log("Error");
                console.error(error);
            }
        });
    });
});

