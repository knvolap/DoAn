namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DotToChucHM")]
    public partial class DotToChucHM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DotToChucHM()
        {
            DSNVTHs = new HashSet<DSNVTH>();
            PhieuDKHMs = new HashSet<PhieuDKHM>();
        }

        [Key]
        [StringLength(20)]
        public string IdDTCHM { get; set; }

        [Required]
        [StringLength(20)]
        public string idChiTietDHM { get; set; }

        [Required]
        [StringLength(100)]
        public string tenDotHienMau { get; set; }

        [Required]
        public string noiDung { get; set; }

        [Required]
        [StringLength(100)]
        public string doiTuongThamGia { get; set; }

        [Required]
        [StringLength(150)]
        public string diaChiToChuc { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage = "vui lòng nhập số từ 1")]
        public int soLuong { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ngayBatDauDK { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ngayKetThucDK { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm    dd/MM/yyyy}")]
        public DateTime ngayToChuc { get; set; }


        [StringLength(50)]
        public string trangThai { get; set; }

        public virtual chiTietDHM chiTietDHM { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DSNVTH> DSNVTHs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuDKHM> PhieuDKHMs { get; set; }
    }
}
