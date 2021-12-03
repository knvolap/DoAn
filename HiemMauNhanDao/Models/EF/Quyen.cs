namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Quyen")]
    public partial class Quyen
    {
        [Key]
        [StringLength(20)]
        public string IdQuyen { get; set; }

        [Required]
        [StringLength(50)]
        public string tenQuyen { get; set; }
    }
}
