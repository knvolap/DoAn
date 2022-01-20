using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class TTCNvsPhieuDKHMView
    {
        public string IdTTCN { get; set; }
        public string hoTen { get; set; }
        public string CCCD { get; set; }
        public string soDT { get; set; }
        [Column(TypeName = "date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ngaySinh { get; set; }
        public bool? gioiTinh { get; set; }
        public string diaChi { get; set; }
        public string ngheNghiep { get; set; }
        public string trinhDo { get; set; }
        public int? soLanHM { get; set; }
        public string coQuanTH { get; set; }
        public string nhomMau { get; set; }
        public string idPDKHM { get; set; }
        public string idDTCHM { get; set; }
        public string tenDTCHM { get; set; }
        public string benhKhac { get; set; }
        public bool trangThai { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm    dd/MM/yyyy}")]
        public DateTime? tgDuKien { get; set; }
        public bool sutCan { get; set; }
        public bool hienMau { get; set; }
        public bool ungThu { get; set; }
        public bool noiHach { get; set; }
        public bool chamCu { get; set; }
        public bool xamMinh { get; set; }
        public bool duocTruyenMau { get; set; }
        public bool suDungMatuy { get; set; }
        public bool NguyCoHIV { get; set; }
        public bool QHTD { get; set; }
        public bool tiemVacXin { get; set; }
        public bool dungTKS { get; set; }
        public bool biSot { get; set; }
        public bool dTTT { get; set; }
        public bool dangMangThai { get; set; }
        public bool xacNhan { get; set; }
    }
}
