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

        public string IdNVYT { get; set; }
        public string idBenhVien { get; set; }
        public string idChucVu { get; set; }
        public string khoa { get; set; }
        public string trinhDo { get; set; }
        public bool? trangThai { get; set; }

    }
}
