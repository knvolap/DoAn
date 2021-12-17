namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuYCNM")]
    public partial class PhieuYCNM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuYCNM()
        {
            ChiTietPhanCongs = new HashSet<ChiTietPhanCong>();
        }

        [Key]
        [StringLength(20)]
        public string IdPhieuYCNM { get; set; }

        [Required]
        [StringLength(20)]
        public string idNVYT { get; set; }

        [Required]
        [StringLength(20)]
        public string idDHM { get; set; }

        public int? soLuong { get; set; }

        [Column(TypeName = "date")]
        public DateTime? tgBatDau { get; set; }

        [Column(TypeName = "date")]
        public DateTime? tgKetThuc { get; set; }

        public DateTime? tgCapnhat { get; set; }

        [Required]
        public string lyDo { get; set; }

        [StringLength(50)]
        public string trangThai { get; set; }

        public virtual DotHienMau DotHienMau { get; set; }

        public virtual NhanVienYTe NhanVienYTe { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhanCong> ChiTietPhanCongs { get; set; }
    }
}
