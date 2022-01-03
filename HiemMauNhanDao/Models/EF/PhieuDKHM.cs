namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuDKHM")]
    public partial class PhieuDKHM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuDKHM()
        {
            KetQuaHienMaus = new HashSet<KetQuaHienMau>();
        }

        [Key]
        [StringLength(20)]
        public string idPDKHM { get; set; }

        [Required]
        [StringLength(20)]
        public string idDTCHM { get; set; }

        [Required]
        [StringLength(20)]
        public string idTTCN { get; set; }

        [StringLength(100)]
        public string benhKhac { get; set; }

        public bool? trangThai { get; set; }

        public DateTime? tgDuKien { get; set; }

        public bool sutCan { get; set; }

        public bool hienMau { get; set; }

        public bool noiHach { get; set; }

        public bool chamCu { get; set; }

        public bool xamMinh { get; set; }

        public bool duocTruyenMau { get; set; }

        public bool suDungMatuy { get; set; }

        public bool NguyCoHIV { get; set; }

        public bool QHTD { get; set; }

        public bool tiemVacXin { get; set; }

        public bool dungTKS { get; set; }

        public bool biSot { get; set; }

        public bool dTTT { get; set; }

        public bool ungThu { get; set; }
        public bool dangMangThai { get; set; }

        public bool xacNhan { get; set; }

        public virtual DotToChucHM DotToChucHM { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KetQuaHienMau> KetQuaHienMaus { get; set; }

        public virtual ThongTinCaNhan ThongTinCaNhan { get; set; }
    }
}
