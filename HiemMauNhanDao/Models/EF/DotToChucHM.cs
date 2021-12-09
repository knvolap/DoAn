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
            PhieuDKHMs = new HashSet<PhieuDKHM>();
        }
        [Key]
        [StringLength(20)]
        public string IdDTCHM { get; set; }

        [Required]
        [StringLength(20)]
        public string idDHM { get; set; }

        [Required]
        [StringLength(100)]
        public string tenDotHienMau { get; set; }

        [Required]
        [StringLength(300)]
        public string noiDung { get; set; }

        [Required]
        [StringLength(100)]
        public string doiTuongThamGia { get; set; }

        [Required]
        [StringLength(150)]
        public string diaChiToChuc { get; set; }

        public int soLuong { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ngayBatDauDK { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ngayKetThucDK { get; set; }

        public DateTime ngayToChuc { get; set; }

        public DateTime? tgCapNhat { get; set; }

        [StringLength(50)]
        public string trangThai { get; set; }

        public virtual DotHienMau DotHienMau { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public virtual ICollection<PhieuDKHM> PhieuDKHMs { get; set; }
    }
}
