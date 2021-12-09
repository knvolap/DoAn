namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChucVu")]
    public partial class ChucVu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ChucVu()
        {
            NhanVienYTes = new HashSet<NhanVienYTe>();
        }
        [Key]
        [StringLength(20)]
        public string IdChucVu { get; set; }

        [Required]
        [StringLength(20)]
        public string idBenhVien { get; set; }

        [Required]
        [StringLength(50)]
        public string TenChucVu { get; set; }


        public virtual BenhVien BenhVien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NhanVienYTe> NhanVienYTes { get; set; }
    }
}
