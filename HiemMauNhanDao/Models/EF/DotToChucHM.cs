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
        public DateTime ngayBatDauDK { get; set; }

        [Column(TypeName = "date")]
        public DateTime ngayKetThucDK { get; set; }

        public DateTime ngayToChuc { get; set; }

        public DateTime? tgCapNhat { get; set; }

        [StringLength(50)]
        public string trangThai { get; set; }
    }
}
