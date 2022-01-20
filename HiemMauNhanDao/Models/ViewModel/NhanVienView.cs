using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class NhanVienView
    {

        public string IdTTCN { get; set; }
        public string idQuyen { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string hoTen { get; set; }     
        public string CCCD { get; set; }      
        public string soDT { get; set; }
        public DateTime? ngaySinh { get; set; }
        public bool? gioiTinh { get; set; }
        public string diaChi { get; set; }
        public string ngheNghiep { get; set; }       
        public string trinhDo { get; set; }
        public int? soLanHM { get; set; }    
        public string coQuanTH { get; set; }     
        public string nhomMau { get; set; }

        public string IdNVYT { get; set; }
        public string idBenhVien { get; set; }
        public string tenBenhVien { get; set; }
        public string tenChucVu { get; set; }
        public string khoa { get; set; }
        public string trinhDoCM { get; set; }
        public bool trangThai { get; set; }


        public string idDTCHM { get; set; }
        public string tenDTCHM { get; set; }
        public string NhiemVu { get; set; }

    }
}
