namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KetQuaHienMau")]
    public partial class KetQuaHienMau
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KetQuaHienMau()
        {
            LichSuHienMaus = new HashSet<LichSuHienMau>();
        }

        [Key]
        [StringLength(20)]
        public string IdKQHM { get; set; }

        [Required]
        [StringLength(20)]
        public string idPDKHM { get; set; }

        [StringLength(10)]
        public string nhomMau { get; set; }

        [StringLength(50)]
        public string nguoiKham { get; set; }

        [StringLength(50)]
        public string nguoiXN { get; set; }

        [StringLength(50)]
        public string nguoiLayMau { get; set; }

        public double? canNang { get; set; }

        [StringLength(10)]
        public string machMau { get; set; }

        [StringLength(50)]
        public string tinhTrangLS { get; set; }

        [StringLength(10)]
        public string huyetAp { get; set; }

        public int? luongMauHien { get; set; }

        public bool? hienMau { get; set; }

        [StringLength(150)]
        public string noiDung { get; set; }

        [StringLength(20)]
        public string HST { get; set; }

        [StringLength(20)]
        public string HBV { get; set; }

        [StringLength(20)]
        public string MSD { get; set; }

        [StringLength(50)]
        public string phanUng { get; set; }

        public DateTime? thoiGianLayMau { get; set; }

        [StringLength(50)]
        public string ghiChu { get; set; }

        [StringLength(50)]
        public string trangThai { get; set; }

        public virtual PhieuDKHM PhieuDKHM { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichSuHienMau> LichSuHienMaus { get; set; }
    }
}
