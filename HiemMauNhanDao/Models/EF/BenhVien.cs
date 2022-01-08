namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BenhVien")]
    public partial class BenhVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BenhVien()
        {
            chiTietDHMs = new HashSet<chiTietDHM>();
            NhanVienYTes = new HashSet<NhanVienYTe>();
        }

        [Key]
        [StringLength(20)]
        public string IdBenhVien { get; set; }

        [Required]
        [StringLength(50)]
        public string TenBenhVien { get; set; }

        [Required]
        [StringLength(100)]
        public string diaChi { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(11)]
        public string soDTBV { get; set; }

        [StringLength(50)]
        public string minhChung { get; set; }

        public bool? trangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<chiTietDHM> chiTietDHMs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NhanVienYTe> NhanVienYTes { get; set; }
    }
}
