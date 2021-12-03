namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonViLienKet")]
    public partial class DonViLienKet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonViLienKet()
        {
            chiTietDHMs = new HashSet<chiTietDHM>();
        }

        [Key]
        [StringLength(20)]
        public string IdDVLK { get; set; }

        [Required]
        [StringLength(20)]
        public string idTK { get; set; }

        [Required]
        [StringLength(50)]
        public string TenDonVi { get; set; }

        [Required]
        [StringLength(100)]
        public string diaChi { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(10)]
        public string soDT { get; set; }

        [Column(TypeName = "image")]
        [Required]
        public byte[] minhChung { get; set; }

        [StringLength(50)]
        public string trangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<chiTietDHM> chiTietDHMs { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
