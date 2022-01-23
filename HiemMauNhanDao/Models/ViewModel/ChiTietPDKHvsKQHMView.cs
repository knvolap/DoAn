using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class ChiTietPDKHvsKQHMView
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

        public string IdKQHM { get; set; }   
        public string nhomMau { get; set; }
        public string nguoiKham { get; set; }
        public string nguoiXN { get; set; }
        public string nguoiLayMau { get; set; }
        public string idnguoiKham { get; set; }
        public string idnguoiXN { get; set; }
        public string idnguoiLayMau { get; set; }

        public double? canNang { get; set; }
        public string machMau { get; set; }
        public string tinhTrangLS { get; set; }
        public string huyetAp { get; set; }
        public int? luongMauHien { get; set; }
        public bool? hienMau2 { get; set; }
        public string noiDung { get; set; }
        public string HST { get; set; }
        public string HBV { get; set; }
        public string MSD { get; set; }
        public string phanUng { get; set; }
        [Column(TypeName = "date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm    dd/MM/yyyy}")]
        public DateTime? thoiGianLayMau { get; set; }
        [Column(TypeName = "date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm    dd/MM/yyyy}")]
        public DateTime? thoiGianKham { get; set; }
        [Column(TypeName = "date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm    dd/MM/yyyy}")]
        public DateTime? thoiGianXN{ get; set; }
        [Column(TypeName = "date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm    dd/MM/yyyy}")]
        public DateTime? thoiGianCapNhat { get; set; }
        public string ghiChu { get; set; }
        public string trangThai2 { get; set; }

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
        public string nhiemVu { get; set; }

    }
}
