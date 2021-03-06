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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public chiTietDHM()
        {
            DotToChucHMs = new HashSet<DotToChucHM>();
        }

        [Key]
        [StringLength(20)]
        public string IdChiTietDHM { get; set; }

        [Required]
        [StringLength(20)]
        public string idDHM { get; set; }

        [Required]
        [StringLength(20)]
        public string idDVLK { get; set; }

        [Required]
        [StringLength(20)]
        public string idBenhVien { get; set; }

        [StringLength(50)]
        public string tenNguoiDK { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ngayDK { get; set; }

        public bool trangThai { get; set; }

        public virtual BenhVien BenhVien { get; set; }

        public virtual DotHienMau DotHienMau { get; set; }

        public virtual DonViLienKet DonViLienKet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DotToChucHM> DotToChucHMs { get; set; }
    }
}
