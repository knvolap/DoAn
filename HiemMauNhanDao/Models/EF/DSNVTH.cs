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
        [Key]
        [StringLength(20)]
        public string idDTCHM { get; set; }

        
        [StringLength(20)]
        public string idNVYT { get; set; }

        public virtual NhanVienYTe NhanVienYTe { get; set; }
        public virtual DotToChucHM DotToChucHM { get; set; }
    }
}
