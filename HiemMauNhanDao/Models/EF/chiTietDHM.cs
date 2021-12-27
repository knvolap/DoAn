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
        [StringLength(20)]
        public string IdChiTietDHM { get; set; }
     
        [StringLength(20)]
        public string idDHM { get; set; }

 
        [StringLength(20)]
        public string idDVLK { get; set; }

        [StringLength(20)]
        public string idNVYT { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ngayDK { get; set; }

        
        public bool? trangThai { get; set; }

        public virtual DonViLienKet DonViLienKet { get; set; }

        public virtual DotHienMau DotHienMau { get; set; }

        public virtual NhanVienYTe NhanVienYTe { get; set; }
    }
}
