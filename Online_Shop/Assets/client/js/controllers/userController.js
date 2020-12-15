var user = {
    init: function () {
        user.loadProvince();
        user.registerEvent();
    },

    registerEvent: function () {
        $('#ddlProvince').off('change').on('change', function () {
            var id = $(this).val();
            if (id != '') {
                user.loadDistrict(parseInt(id));
            }
            else {
                $('#ddlDistrict').html('');
            }
        });

        $('#ddlDistrict').off('change').on('change', function () {
            var id = $(this).val();
            if (id != '') {
                user.loadPrecinct(parseInt(id));
            }
            else {
                $('#ddlPrecinct').html('');
            }
        });
    },

    loadProvince: function () {
        $.ajax({
            url: '/User/LoadProvince',
            type: "POST",
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var html = '<option>--Chọn tỉnh thành--</option>';
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value = "' + item.ID + '">' + item.Name + '</option>'
                    });
                    $('#ddlProvince').html(html);
                }
            }
        })
    },

    loadDistrict: function (id) {
        $.ajax({
            url: '/User/LoadDistrict',
            type: "POST",
            data: { provinceID: id },
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var html = '<option>--Chọn quận huyện--</option>';
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value = "' + item.ID + '">' + item.Name + '</option>'
                    });
                    $('#ddlDistrict').html(html);
                }
            }
        })
    },

    loadPrecinct: function (id) {
        $.ajax({
            url: '/User/LoadPrecinct',
            type: "POST",
            data: { districtID: id },
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var html = '<option>--Chọn xã phường--</option>';
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value = "' + item.Name + '">' + item.Name + '</option>'
                    });
                    $('#ddlPrecinct').html(html);
                }
            }
        })
    }
}
user.init();