using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class ChiTietvsToChucHMView
    {

        public string IdChiTietDHM { get; set; }
        public string idDHM { get; set; }
        public string idDVLK { get; set; }
        public string tenDVLK { get; set; }
        public string tenDHM { get; set; }
        public string idBenhVien { get; set; }
        public string tenBenhVien { get; set; }
        public string idTTCN { get; set; }
        public string tenNguoiDung { get; set; }
        public string idNVYT { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ngayDK { get; set; }
        public bool trangThai1 { get; set; }


        public string IdDTCHM { get; set; }
        public string tenDTCHM { get; set; }
        public string noiDung { get; set; }
        public string doiTuongThamGia { get; set; }
        public string diaChiToChuc { get; set; }
        public int soLuong { get; set; }
        [Column(TypeName = "date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ngayBatDauDK { get; set; }
        [Column(TypeName = "date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ngayKetThucDK { get; set; }
        [Column(TypeName = "date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm    dd/MM/yyyy}")]
        public DateTime ngayToChuc { get; set; }
        public bool trangThai { get; set; }

        public bool trangThai2 { get; set; }
    }
}
