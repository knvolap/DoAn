using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
     public class DotHiemMauView
    {
        public string IdDHM { get; set; }


        public string TenDHM { get; set; }


        public string noiDung { get; set; }


        public DateTime tgBatDau { get; set; }


        public DateTime tgKetThuc { get; set; }


        public bool? trangThai { get; set; }

        public string idDVLK { get; set; }

        public string idNVYT { get; set; }

        public DateTime? ngayDK { get; set; }

    }
}
