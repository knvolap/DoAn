using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Models.EF
{
    public partial class DbContextHM : DbContext
    {
        public DbContextHM() : base("name=DbContextHM")
        {
        }

        public virtual DbSet<BenhVien> BenhViens { get; set; }
        public virtual DbSet<ChucVu> ChucVus { get; set; }
        public virtual DbSet<DonViLienKet> DonViLienKets { get; set; }
        public virtual DbSet<DotHienMau> DotHienMaus { get; set; }
        public virtual DbSet<DotToChucHM> DotToChucHMs { get; set; }
        public virtual DbSet<KetQuaHienMau> KetQuaHienMaus { get; set; }
        public virtual DbSet<NhanVienYTe> NhanVienYTes { get; set; }
        public virtual DbSet<PhieuDKHM> PhieuDKHMs { get; set; }
        public virtual DbSet<PhieuYCNM> PhieuYCNMs { get; set; }
        public virtual DbSet<Quyen> Quyens { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<ThongTinCaNhan> ThongTinCaNhans { get; set; }
        public virtual DbSet<chiTietDHM> chiTietDHMs { get; set; }
        public virtual DbSet<ChiTietPhanCong> ChiTietPhanCongs { get; set; }
        public virtual DbSet<DSNVTH> DSNVTHs { get; set; }
        public virtual DbSet<LichSuHienMau> LichSuHienMaus { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BenhVien>()
                .Property(e => e.IdBenhVien)
                .IsUnicode(false);

            modelBuilder.Entity<BenhVien>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<BenhVien>()
                .Property(e => e.soDTBV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ChucVu>()
                .Property(e => e.IdChucVu)
                .IsUnicode(false);

       

            modelBuilder.Entity<DonViLienKet>()
                .Property(e => e.IdDVLK)
                .IsUnicode(false);

            modelBuilder.Entity<DonViLienKet>()
                .Property(e => e.idTK)
                .IsUnicode(false);

            modelBuilder.Entity<DonViLienKet>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<DonViLienKet>()
                .Property(e => e.soDT)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DonViLienKet>()
                .HasMany(e => e.chiTietDHMs)
                .WithRequired(e => e.DonViLienKet)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DotHienMau>()
                .Property(e => e.IdDHM)
                .IsUnicode(false);

            modelBuilder.Entity<DotHienMau>()
                .HasMany(e => e.chiTietDHMs)
                .WithRequired(e => e.DotHienMau)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DotHienMau>()
                .HasMany(e => e.PhieuYCNMs)
                .WithRequired(e => e.DotHienMau)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DotHienMau>()
                .HasMany(e => e.DotToChucHMs)
                .WithRequired(e => e.DotHienMau)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<DotToChucHM>()
                .Property(e => e.IdDTCHM)
                .IsUnicode(false);

            modelBuilder.Entity<DotToChucHM>()
                .Property(e => e.idDHM)
                .IsUnicode(false);

            modelBuilder.Entity<DotToChucHM>()
              .HasMany(e => e.PhieuDKHMs)
              .WithRequired(e => e.DotToChucHM)
              .WillCascadeOnDelete(false);

            modelBuilder.Entity<KetQuaHienMau>()
                .Property(e => e.IdKQHM)
                .IsUnicode(false);

            modelBuilder.Entity<KetQuaHienMau>()
                .Property(e => e.idDTCHM)
                .IsUnicode(false);

            modelBuilder.Entity<KetQuaHienMau>()
                .Property(e => e.nhomMau)
                .IsUnicode(false);

            modelBuilder.Entity<KetQuaHienMau>()
                .Property(e => e.machMau)
                .IsUnicode(false);

            modelBuilder.Entity<KetQuaHienMau>()
                .Property(e => e.huyetAp)
                .IsUnicode(false);

            modelBuilder.Entity<KetQuaHienMau>()
                .HasMany(e => e.LichSuHienMaus)
                .WithRequired(e => e.KetQuaHienMau)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVienYTe>()
                .Property(e => e.IdNVYT)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVienYTe>()
                .Property(e => e.idTK)
                .IsUnicode(false);
            modelBuilder.Entity<NhanVienYTe>()
               .Property(e => e.idBenhVien)
               .IsUnicode(false);

            modelBuilder.Entity<NhanVienYTe>()
                .Property(e => e.idChucVu)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVienYTe>()
                .HasMany(e => e.chiTietDHMs)
                .WithRequired(e => e.NhanVienYTe)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhieuDKHM>()
                .Property(e => e.idDTCHM)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuDKHM>()
                .Property(e => e.idTTCN)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuDKHM>()
                .HasMany(e => e.KetQuaHienMaus)
                .WithRequired(e => e.PhieuDKHM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhieuYCNM>()
                .Property(e => e.IdPhieuYCNM)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuYCNM>()
                .Property(e => e.idNVYT)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuYCNM>()
                .Property(e => e.idDHM)
                .IsUnicode(false);

            modelBuilder.Entity<Quyen>()
                .Property(e => e.IdQuyen)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.IdTK)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.idQuyen)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.userName)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.DonViLienKets)
                .WithRequired(e => e.TaiKhoan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.ThongTinCaNhans)
                .WithRequired(e => e.TaiKhoan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ThongTinCaNhan>()
                .Property(e => e.idTTCN)
                .IsUnicode(false);

            modelBuilder.Entity<ThongTinCaNhan>()
              .HasMany(e => e.PhieuDKHMs)
              .WithRequired(e => e.ThongTinCaNhan)
              .WillCascadeOnDelete(false);

            modelBuilder.Entity<ThongTinCaNhan>()
                .Property(e => e.idTK)
                .IsUnicode(false);

            modelBuilder.Entity<ThongTinCaNhan>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<ThongTinCaNhan>()
                .Property(e => e.CCCD)
                .IsUnicode(false);

            modelBuilder.Entity<ThongTinCaNhan>()
                .Property(e => e.soDT)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ThongTinCaNhan>()
                .Property(e => e.gioiTinh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ThongTinCaNhan>()
                .Property(e => e.nhomMau)
                .IsUnicode(false);

            modelBuilder.Entity<chiTietDHM>()
                .Property(e => e.idDHM)
                .IsUnicode(false);

            modelBuilder.Entity<chiTietDHM>()
                .Property(e => e.idDVLK)
                .IsUnicode(false);

            modelBuilder.Entity<chiTietDHM>()
                .Property(e => e.idNVYT)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietPhanCong>()
                .Property(e => e.idDVLK)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietPhanCong>()
                .Property(e => e.idPhieuYCNM)
                .IsUnicode(false);

            modelBuilder.Entity<DSNVTH>()
                .Property(e => e.idDTCHM)
                .IsUnicode(false);

            modelBuilder.Entity<DSNVTH>()
                .Property(e => e.idNVYT)
                .IsUnicode(false);

            modelBuilder.Entity<LichSuHienMau>()
                .Property(e => e.idTK)
                .IsUnicode(false);

            modelBuilder.Entity<LichSuHienMau>()
                .Property(e => e.idKQHM)
                .IsUnicode(false);
        }


        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{

        //    // Configure relationships  
        //    //liên kết 1 vs N
        //    //modelBuilder.Entity<TaiKhoan>().HasOne(b => b.Quyens).WithMany(b => b.TaiKhoans).HasForeignKey(p => p.idQuyen).OnDelete(DeleteBehavior.NoAction);
        //    //modelBuilder.Entity<Room>().HasOne(b => b.Floors).WithMany(b => b.Rooms).HasForeignKey(p => p.floorId).OnDelete(DeleteBehavior.NoAction);
        //    //modelBuilder.Entity<BookRoom>().HasOne(b => b.Users).WithMany(b => b.BookRooms).HasForeignKey(p => p.personBookingId).OnDelete(DeleteBehavior.NoAction);
        //    //modelBuilder.Entity<User>().HasOne(b => b.Roles).WithMany(b => b.Users).HasForeignKey(p => p.roleId).OnDelete(DeleteBehavior.NoAction);

        //    //liên kết  N vs 1
        //    //modelBuilder.Entity<Room>().HasMany<BookRoom>(s => s.BookRooms)
        //    //        .WithOne(ad => ad.Rooms).HasForeignKey(ad => ad.roomId);
        //    //Data seeding -- nhập dữ liệu có sẵn ở file "ModelBuilderExtensions"
        //    //modelBuilder.Seed();
        //}
    }
}
