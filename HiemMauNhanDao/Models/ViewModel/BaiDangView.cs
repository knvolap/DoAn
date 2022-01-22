using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class BaiDangView
    {
        public string IdDTCHM { get; set; }
        public string idChiTietDHM { get; set; }
        public string tenDotHienMau { get; set; }
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
        public string tenNguoiDangBai { get; set; }
        public bool trangThai { get; set; }
      
        public string idCTDK { get; set; }
        public string idDVLK { get; set; }
        public string tenDVLK { get; set; }
        public string sdtDVLK { get; set; }
        public string idBenhVien { get; set; }
        public string tenBV { get; set; }
        public string sdtBV { get; set; }


    }
}
