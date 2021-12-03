namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LichSuHienMau")]
    public partial class LichSuHienMau
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string idTK { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string idKQHM { get; set; }

        [StringLength(50)]
        public string quaTang { get; set; }

        [Column(TypeName = "image")]
        public byte[] anhChungNhan { get; set; }

        public virtual KetQuaHienMau KetQuaHienMau { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
