namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThongTinCaNhan")]
    public partial class ThongTinCaNhan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        public ThongTinCaNhan()
        {
            DonViLienKets = new HashSet<DonViLienKet>();
            NhanVienYTes = new HashSet<NhanVienYTe>();
            PhieuDKHMs = new HashSet<PhieuDKHM>();
        }
        [Key]
        [StringLength(20)]
        public string IdTTCN { get; set; }
       
        [Required]
        [StringLength(20)]
        public string idQuyen { get; set; }

        [Required]
        [StringLength(50)]
        public string userName { get; set; }

        [Required]
        [StringLength(50)]
        public string password { get; set; }
  
        [Required]
        [StringLength(100)]
        public string hoTen { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(12)]
        public string CCCD { get; set; }

        [Required]
        [StringLength(10)]
        public string soDT { get; set; }

        [Column(TypeName = "date")]
        public DateTime ngaySinh { get; set; }


        public bool? gioiTinh { get; set; }

        [Required]
        [StringLength(100)]
        public string diaChi { get; set; }

        [StringLength(100)]
        public string ngheNghiep { get; set; }

        [StringLength(50)]
        public string trinhDo { get; set; }

        [StringLength(50)]
        public string coQuanTH { get; set; }

        public int? soLanHM { get; set; }

        [StringLength(2)]
        public string nhomMau { get; set; }

        public bool? trangThai { get; set; }

        public virtual Quyen Quyen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuDKHM> PhieuDKHMs { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonViLienKet> DonViLienKets { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichSuHienMau> LichSuHienMaus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NhanVienYTe> NhanVienYTes { get; set; }

    }
}
