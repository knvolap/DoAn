var ct = {
    init: function () {
        ct.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/KetQuaHienMau/ChangeStatus4",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.log(response);
                    if (response.tt == true) {
                        btn.text('Đã duyệt');
                    }
                    else {
                        btn.text('Chưa duyệt');
                    }
                  
                }
            });
        });
    }
}
ct.init();