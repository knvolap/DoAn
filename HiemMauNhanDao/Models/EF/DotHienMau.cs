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
            DotToChucHMs = new HashSet<DotToChucHM>();
        }

        [Key]
        [StringLength(20)]
        public string IdDHM { get; set; }

        [Required]
        [StringLength(50)]
        public string TenDHM { get; set; }

        [StringLength(150)]
        public string noiDung { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(ApplyFormatInEditMode =true,DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime? tgBatDau { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? tgKetThuc { get; set; }

        public bool? trangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<chiTietDHM> chiTietDHMs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuYCNM> PhieuYCNMs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DotToChucHM> DotToChucHMs { get; set; }
    }
}
