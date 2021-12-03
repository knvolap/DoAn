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
        [Key]
        [StringLength(20)]
        public string IdChucVu { get; set; }

        [Required]
        [StringLength(20)]
        public string idBenhVien { get; set; }

        [Required]
        [StringLength(50)]
        public string TenChucVu { get; set; }
    }
}
