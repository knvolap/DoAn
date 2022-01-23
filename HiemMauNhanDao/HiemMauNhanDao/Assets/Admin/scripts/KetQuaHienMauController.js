$(document).ready(function () {
    $('#xuatExcel').on('click', function () {
        $.ajax({
            dataType: 'json',
            type:'GET',
            url:'/KetQuaHienMau//',
        })
    })

});