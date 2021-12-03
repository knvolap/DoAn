namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DSNVTH")]
    public partial class DSNVTH
    {
        [StringLength(20)]
        public string idDTCHM { get; set; }

        [Key]
        [StringLength(20)]
        public string idNVYT { get; set; }

        public virtual NhanVienYTe NhanVienYTe { get; set; }
    }
}
