namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonViLienKet")]
    public partial class DonViLienKet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonViLienKet()
        {
            chiTietDHMs = new HashSet<chiTietDHM>();
            ChiTietPhanCongs = new HashSet<ChiTietPhanCong>();
        }

        [Key]
        [StringLength(20)]
        public string IdDVLK { get; set; }

        [Required]
        [StringLength(20)]
        public string idTTCN { get; set; }

      
        [StringLength(50)]
        [Required(ErrorMessage = "Không được bỏ trống tên đơn vị")]
        public string TenDonVi { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Không được bỏ trống địa chỉ")]
        public string diaChi { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Không được bỏ trống Email")]
        public string Email { get; set; }

        [StringLength(12)]
        [Required(ErrorMessage = "Không được bỏ trống số điện thoại")]
        public string soDT { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Không được bỏ trống minh chứng")]
        public string minhChung { get; set; }

        public bool? trangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<chiTietDHM> chiTietDHMs { get; set; }

        public virtual ThongTinCaNhan ThongTinCaNhan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhanCong> ChiTietPhanCongs { get; set; }
    }
}
