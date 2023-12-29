$(document).ready(function () {
    $('#province').change(function () {
        const selectedProvinceId = $(this).val();
        console.log(selectedProvinceId);
        $.ajax({
            url: '/Ui/UpdateDistrictListByProvinceId',
            type: 'GET',
            data: {provinceId: selectedProvinceId},
            success: function (data) {
                console.log("Data : " + data);
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

        $.ajax({
            url: '/Ui/UpdateCommuneListByProvinceId',
            type: 'GET',
            data: {provinceId: selectedProvinceId},
            success: function (data) {
                $('#commune').empty();
                data.forEach(function (commune) {
                    $('#commune').append($('<option>', {
                        value: commune.id,
                        text: commune.name,
                        style: 'color : black'
                    }));
                });
            },
            error: function (error) {
                console.error(error);
            }
        });
    });

    $('#district').change(function () {
        const selectedDistrictId = $(this).val();
        console.log(selectedDistrictId); 
        $.ajax({
            url: '/Ui/UpdateCommuneListByDistrictId',
            type: 'GET',
            data: {districtId: selectedDistrictId},
            success: function (data) {
                $('#commune').empty();
                data.forEach(function (commune) {
                    $('#commune').append($('<option>', {
                        value: commune.id,
                        text: commune.name,
                        style: 'color : black'
                    }));
                });
            },
            error: function (error) {
                console.error(error);
            }
        })
    })

    $('#chk_phonenumber').change(function () {
        const isChecked = $(this).is(':checked');
        const phoneNumberInput = $('#phonenumber');
        phoneNumberInput.prop('disabled', isChecked);
        phoneNumberInput.val("");
    });

    $('#chk_idCard').change(function () {
        const isChecked = $(this).is(':checked');
        const idCardInput = $('#idCard');
        idCardInput.prop('disabled', isChecked);
        idCardInput.val("");
    });


});
