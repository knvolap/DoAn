using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class NhanVienvaDVLKView
    {
        public string IdTTCN { get; set; }
        public string idQuyen { get; set; }
        public string hoTen { get; set; }
        public string CCCD { get; set; }

        public string IdDVLK { get; set; }
        public string TenDonVi { get; set; }
        public string diaChi { get; set; }
        public string Email { get; set; }
        public string soDT { get; set; }
        public string minhChung { get; set; }
        public bool? trangThai { get; set; }

    }
}
