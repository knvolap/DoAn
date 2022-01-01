namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhanVienYTe")]
    public partial class NhanVienYTe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhanVienYTe()
        {
            chiTietDHMs = new HashSet<chiTietDHM>();
            PhieuYCNMs = new HashSet<PhieuYCNM>();
        }

        [Key]
        [StringLength(20)]
        public string IdNVYT { get; set; }

        [StringLength(20)]
        public string idTTCN { get; set; }

        [StringLength(20)]
        public string idBenhVien { get; set; }

        [StringLength(20)]
        public string idChucVu { get; set; }

        [StringLength(20)]
        public string khoa { get; set; }

        [StringLength(50)]
        public string trinhDo { get; set; }

        public bool? trangThai { get; set; }

        public virtual BenhVien BenhVien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<chiTietDHM> chiTietDHMs { get; set; }

        public virtual ChucVu ChucVu { get; set; }

        public virtual DSNVTH DSNVTH { get; set; }

        public virtual ThongTinCaNhan ThongTinCaNhan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuYCNM> PhieuYCNMs { get; set; }
    }
}
