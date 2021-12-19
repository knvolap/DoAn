using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class PhieuDKHMView
    {
        public string idPDKHM { get; set; }    
        public string idDTCHM { get; set; }
        public DateTime? tgDuKien { get; set; }
        public bool? trangThai { get; set; }
      
        public string IdTTCN { get; set; }
        public bool? gioiTinh { get; set; }
        public string hoTen { get; set; }
    }
}
