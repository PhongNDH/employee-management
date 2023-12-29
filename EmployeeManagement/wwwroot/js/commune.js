$(document).ready(function () {
    $('#province').change(function () {
        var selectedProvinceId = $(this).val();
        $.ajax({
            url: '/UI/UpdateDistrictListByProvinceId',
            type: 'GET',
            data: { provinceId: selectedProvinceId },
            success: function (data) {
                $('#district').empty();
                data.forEach(function (district) {
                    $('#district').append($('<option>', {
                        value: district.id,
                        text: district.name,
                        style: 'color : black'
                    }));
                });
            },
            error: function (error) {
                console.error(error);
            }
        });
    });
});