namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DotHienMau")]
    public partial class DotHienMau
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DotHienMau()
        {
            chiTietDHMs = new HashSet<chiTietDHM>();
            PhieuYCNMs = new HashSet<PhieuYCNM>();
        }

        [Key]
        [StringLength(20)]
        public string IdDHM { get; set; }

        [Required]
        [StringLength(50)]
        public string TenDHM { get; set; }

        [Required]
        [StringLength(150)]
        public string noiDung { get; set; }

        [Column(TypeName = "date")]
        public DateTime tgBatDau { get; set; }

        [Column(TypeName = "date")]
        public DateTime tgKetThuc { get; set; }

        [StringLength(50)]
        public string trangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<chiTietDHM> chiTietDHMs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuYCNM> PhieuYCNMs { get; set; }
    }
}
