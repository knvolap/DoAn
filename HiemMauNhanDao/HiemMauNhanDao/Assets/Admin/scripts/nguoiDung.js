    $("#btnSubmit").on('click', function (e) {
        $("#NgaySinhError").empty();
        var ngaySinh = $("#ngaySinh").val();
        const now = new Date()
        const ngaysinh2 = new Date(ngaySinh);
        let namHT = now.getFullYear();
        let namSinh = ngaysinh2.getFullYear();
        if (namHT - namSinh >= 18) {
            return;
        }
        else {
        e.preventDefault();
            document.getElementById("NgaySinhError").innerHTML = "Bạn không đủ 18 tuổi!!! Không thể tham gia hiến máu";
        }
    })
