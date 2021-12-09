namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("chiTietDHM")]
    public partial class chiTietDHM
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string idDHM { get; set; }
        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string idDVLK { get; set; }
        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string idNVYT { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ngayDK { get; set; }

        [StringLength(50)]
        public string trangThai { get; set; }

        public virtual DonViLienKet DonViLienKet { get; set; }

        public virtual DotHienMau DotHienMau { get; set; }

        public virtual NhanVienYTe NhanVienYTe { get; set; }
    }
}
