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
            KetQuaHienMaus = new HashSet<KetQuaHienMau>();
            KetQuaHienMaus1 = new HashSet<KetQuaHienMau>();
            KetQuaHienMaus2 = new HashSet<KetQuaHienMau>();
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
        public string tenChucVu { get; set; }

        [StringLength(20)]
        public string khoa { get; set; }

        [StringLength(50)]
        public string trinhDoCM { get; set; }

        public bool trangThai { get; set; }

        public virtual BenhVien BenhVien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KetQuaHienMau> KetQuaHienMaus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KetQuaHienMau> KetQuaHienMaus1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KetQuaHienMau> KetQuaHienMaus2 { get; set; }

        public virtual DSNVTH DSNVTH { get; set; }

        public virtual ThongTinCaNhan ThongTinCaNhan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuYCNM> PhieuYCNMs { get; set; }
    }
}
