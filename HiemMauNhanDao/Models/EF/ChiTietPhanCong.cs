namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietPhanCong")]
    public partial class ChiTietPhanCong
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string idDVLK { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string idPhieuYCNM { get; set; }

        [Column(TypeName = "date")]
        public DateTime? tgXacNhan { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime tgThucHien { get; set; }

        public string phanHoi { get; set; }

        [StringLength(50)]
        public string trangThai { get; set; }
    }
}
