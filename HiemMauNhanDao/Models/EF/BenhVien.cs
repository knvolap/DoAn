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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BenhVien()
        {
            NhanVienYTes = new HashSet<NhanVienYTe>();
        }

        [Key]
        [StringLength(20)]
        public string IdBenhVien { get; set; }

        
        [StringLength(50)]
        //[Required(ErrorMessage = "Tên bệnh viện không được để trống.")]
        public string TenBenhVien { get; set; }

       
        [StringLength(100)]
        //[Required(ErrorMessage = "Địa chỉ không được để trống.")]
        public string diaChi { get; set; }

   
        [StringLength(50)]
        //[Required(ErrorMessage = "Email không được để trống.")]
        public string Email { get; set; }

        
        [StringLength(11)]
        //[Required(ErrorMessage = "SDT không được để trống.")]
        public string soDTBV { get; set; }

       
        [StringLength(50)]
        //[Required(ErrorMessage = "Minh chứng không được để trống.")]
        public string minhChung { get; set; }

        public bool? trangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NhanVienYTe> NhanVienYTes { get; set; }
    }
}
