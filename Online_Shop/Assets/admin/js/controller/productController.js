var product = {
    init: function () {
        product.registerEvents();
    },
    registerEvents: function () {
        $('.btn-images').off('click').on('click', function (e) {
            e.preventDefault();
            $('#imagesManage').modal('show');
            $('#hidProductID').val($(this).data('id'));
            product.loadImages();
        });

        $('#btnChooseImages').off('click').on('click', function (e) {
            e.preventDefault();
            var finder = new CKFinder();
            finder.selectActionFunction = function (url) {
                var html = '<div style="float:left"><img src="' + url + '" width="100" /><a href="#" class="btnDelImages"><i class="fa fa-times"></i></a>' +
                    '<input type="hidden" class="hidImage" value=' + url + '>' + '</div>';
                $('#imageList').append(html);

                $('.btnDelImages').off('click').on('click', function (e) {
                    e.preventDefault();
                    $(this).parent().remove();
                });
            };
            finder.popup();
        });

        $('#btnSaveImages').off('click').on('click', function () {
            var images = [];
            var id = $('#hidProductID').val();
            $.each($('#imageList .hidImage'), function (i, item) {
                images.push($(item).val());
            })
            //$.each($('#imageList img'), function (i, item) {
            //    images.push($(item).prop('src'));
            //})
            $.ajax({
                url: '/Admin/Product/SaveImages',
                type: 'POST',
                data: {
                    id: id,
                    images: JSON.stringify(images)
                },
                dataType: 'json',
                success: function (res) {
                    if (res.status) {
                        $('#imagesManage').modal('hide');
                        $('#imageList').html('');
                        alert('Lưu thành công');
                    }
                }
            });
        })
    },
    loadImages: function () {
        $.ajax({
            url: '/Admin/Product/LoadImages',
            type: 'GET',
            data: {
                id: $('#hidProductID').val()
            },
            dataType: 'json',
            success: function (res) {
                var data = res.data;
                var html = '';
                $.each(data, function (i, item) {
                    html += '<div style="float:left"><img src="' + item + '" width="100" /><a href="#" class="btnDelImages"><i class="fa fa-times"></i></a></div>'
                });
                $('#imageList').html(html);

                $('.btnDelImages').off('click').on('click', function (e) {
                    e.preventDefault();
                    $(this).parent().remove();
                });
            }
        });
    }
};
product.init();