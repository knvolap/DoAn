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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DotToChucHM()
        {
            DSNVTHs = new HashSet<DSNVTH>();
            PhieuDKHMs = new HashSet<PhieuDKHM>();
        }

        [Key]
        [StringLength(20)]
        public string IdDTCHM { get; set; }

        [Required]
        [StringLength(20)]
        public string idChiTietDHM { get; set; }

        [Required(ErrorMessage = "Tên bài đăng không được để trống.")]
        [StringLength(100)]
        public string tenDotHienMau { get; set; }

        [Required(ErrorMessage = "Nội dung không được để trống.")]
        public string noiDung { get; set; }

        [Required(ErrorMessage = "Đối tượng tham gia không được để trống.")]
        [StringLength(100)]
        public string doiTuongThamGia { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được để trống.")]
        [StringLength(150)]
        public string diaChiToChuc { get; set; }

        [Required(ErrorMessage = "số lượng không được để trống.")]
        public int soLuong { get; set; }

        [Required(ErrorMessage = "Ngày bắt đầu không được để trống.")]
        [Column(TypeName = "date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ngayBatDauDK { get; set; }

        [Required(ErrorMessage = "Ngày kết thúc không được để trống.")]
        [Column(TypeName = "date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ngayKetThucDK { get; set; }

        [Required(ErrorMessage = "Ngày tổ chức không được để trống.")]
        [Column(TypeName = "date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm    dd/MM/yyyy}")]
        public DateTime ngayToChuc { get; set; }

        [StringLength(50)]
        public string tenNguoiDangBai { get; set; }

        public bool trangThai { get; set; }

        public virtual chiTietDHM chiTietDHM { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DSNVTH> DSNVTHs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuDKHM> PhieuDKHMs { get; set; }
    }
}
