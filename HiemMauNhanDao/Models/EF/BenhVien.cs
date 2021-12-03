namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BenhVien")]
    public partial class BenhVien
    {
        [Key]
        [StringLength(20)]
        public string IdBenhVien { get; set; }

        [Required]
        [StringLength(50)]
        public string TenBenhVien { get; set; }

        [Required]
        [StringLength(100)]
        public string diaChi { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(10)]
        public string soDTBV { get; set; }

        [Column(TypeName = "image")]
        [Required]
        public byte[] minhChung { get; set; }

        [Required]
        [StringLength(50)]
        public string trangThai { get; set; }
    }
}
