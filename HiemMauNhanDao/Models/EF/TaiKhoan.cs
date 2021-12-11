namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiKhoan")]
    public partial class TaiKhoan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        public TaiKhoan()
        {
            DonViLienKets = new HashSet<DonViLienKet>();
            NhanVienYTes = new HashSet<NhanVienYTe>();
            ThongTinCaNhans = new HashSet<ThongTinCaNhan>();
        }

        [Key]
        [StringLength(20)]
        public string IdTK { get; set; }

        [Required]
        [StringLength(20)]
        public string idQuyen { get; set; }

        [Required]
        [StringLength(50)]
        public string userName { get; set; }

        [Required]
        [StringLength(50)]
        public string password { get; set; }

        public bool? trangThai { get; set; }

        public virtual Quyen Quyen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonViLienKet> DonViLienKets { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThongTinCaNhan> ThongTinCaNhans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NhanVienYTe> NhanVienYTes { get; set; }


    }
}
