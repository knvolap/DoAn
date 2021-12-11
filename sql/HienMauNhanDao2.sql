USE master
GO
IF EXISTS(SELECT name FROM sysdatabases WHERE name ='HiemMauNhanDao')
DROP DATABASE HiemMauNhanDao
go

----Bắt đầu ----

CREATE DATABASE HiemMauNhanDao
GO
USE HiemMauNhanDao
GO

SET DATEFORMAT dmy

--Table 1 Role
CREATE TABLE Quyen(
	IdQuyen		VARCHAR(20) PRIMARY KEY,
	tenQuyen	NVARCHAR (50) not null,
)
GO


--Table 2 TaiKhoan
CREATE TABLE TaiKhoan(
	IdTK		VARCHAR(20) PRIMARY KEY,
	idQuyen		VARCHAR(20)  not null,
	userName	VARCHAR (50)UNIQUE not null,
	password	VARCHAR(50) not null,
	trangThai	bit DEFAULT '1' CHECK ( trangThai IN ( '0', '1' ) ), --0: chưa kích hoạt, 1: đã kích hoạt
)
GO


--Table 3 DotHienMau
CREATE TABLE DotHienMau(
	IdDHM		VARCHAR(20) PRIMARY KEY,
	TenDHM		NVARCHAR (50) UNIQUE not null,
	noiDung		NVARCHAR (150) null,
	tgBatDau	DATE  null,	
	tgKetThuc	DATE  null,
	trangThai	bit DEFAULT '1' CHECK ( trangThai IN ( '0', '1' ) ), --0: Hết hạn, 1: Đang diễn ra
)
GO


--Table 4 ThongTinCaNhan
CREATE TABLE ThongTinCaNhan(
	idTTCN		 VARCHAR(20) PRIMARY KEY,
	idTK		 VARCHAR(20) not null,	
	hoTen		 NVARCHAR(50)  NULL,
	Email		 VARCHAR(50) UNIQUE  NULL,	
	CCCD		 VARCHAR(12) UNIQUE  NULL,
	soDT		 CHAR(11)  UNIQUE  NULL,
	ngaySinh	 DATE  NULL,
	gioiTinh	 bit DEFAULT '1' CHECK ( gioiTinh IN ( '0', '1' ) ), 
	diaChi		 NVARCHAR(100)  NULL,
	ngheNghiep	 NVARCHAR(100) NULL,
	trinhDo		 NVARCHAR(50) NULL,
	soLanHM		 int  null,
	nhomMau		 VARCHAR(2) null,
	trangThai	 NVARCHAR(50) null
)
GO

--Table 5 BenhVien
CREATE TABLE BenhVien(
	IdBenhVien	 VARCHAR(20) PRIMARY KEY,
	TenBenhVien	 NVARCHAR (50) not null,
	diaChi		 NVARCHAR(100) NOT NULL,
	Email		 VARCHAR(50) UNIQUE NOT NULL,	
	soDTBV		 CHAR(11) UNIQUE NOT NULL,	
	minhChung	 VARCHAR(50)  NULL,	
	trangThai	bit DEFAULT '1' CHECK ( trangThai IN ( '0', '1' ) ), --0: chưa kích hoạt, 1: đã kích hoạt
)
GO
 


--Table 6 ChucVu
CREATE TABLE ChucVu(
	IdChucVu	 VARCHAR(20) PRIMARY KEY,
	TenChucVu	 NVARCHAR (50) not null,	
)
GO

--Table 7 NhanVienYTe
CREATE TABLE NhanVienYTe(
	IdNVYT		VARCHAR(20) PRIMARY KEY,
	idTK		VARCHAR(20) FOREIGN KEY REFERENCES dbo.TaiKhoan(IdTK)  ON DELETE CASCADE ON UPDATE CASCADE ,
	idBenhVien	 VARCHAR(20)FOREIGN KEY REFERENCES dbo.BenhVien(IdBenhVien)  ON DELETE CASCADE ON UPDATE CASCADE ,
	idChucVu	VARCHAR(20)   null,
	khoa		NVARCHAR(20)   null, --  Khoa Huyết học - Khoa Lọc máu - Khoa Truyền máu 
	nhiemVu		NVARCHAR (50)  null,	
	trinhDo		NVARCHAR (50)  null,	
	trangThai	bit DEFAULT '1' CHECK ( trangThai IN ( '0', '1' ) ), --0: chưa kích hoạt, 1: đã kích hoạt
)
GO



--Table 8 DonViLienKet
CREATE TABLE DonViLienKet(
	IdDVLK			VARCHAR(20) PRIMARY KEY,
	idTK			VARCHAR(20) FOREIGN KEY REFERENCES dbo.TaiKhoan(IdTK) ON DELETE CASCADE ON UPDATE CASCADE  not null,
	TenDonVi		NVARCHAR (50) not null,
	diaChi			NVARCHAR(100)  NULL,
	Email			VARCHAR(50) UNIQUE  NULL,	
	soDT			CHAR(11) UNIQUE NULL,
	minhChung		VARCHAR(50)  NULL,	
	trangThai		bit DEFAULT '1' CHECK ( trangThai IN ( '0', '1' ) ), --0: chưa kích hoạt, 1: đã kích hoạt
)
GO


--Table 9 PhieuYCNM
CREATE TABLE PhieuYCNM(
	IdPhieuYCNM		VARCHAR(20) PRIMARY KEY,
	idNVYT			VARCHAR(20) not null,
	idDHM			VARCHAR(20) FOREIGN KEY REFERENCES dbo.DotHienMau(IdDHM) not null,
	soLuong			int   null,
	tgBatDau		DATE  null,	
	tgKetThuc		DATE  null,
	tgCapnhat		DATETime null,
	lyDo			NVARCHAR(MAX) NOT NULL,
	trangThai		NVARCHAR(50) 
)
GO


--Table 10 ChiTietPhanCong
CREATE TABLE ChiTietPhanCong(
	idDVLK			VARCHAR(20) not null,	
	idPhieuYCNM		VARCHAR(20) not null,	
	tgXacNhan		DATE DEFAULT GETDATE()null,
	tgThucHien		DATETIME not null,
	phanHoi			NVARCHAR(MAX) ,
	trangThai		NVARCHAR(50)
)
GO

--Table 11 DotToChucHM
CREATE TABLE DotToChucHM(
	IdDTCHM			VARCHAR(20) PRIMARY KEY,
	idDHM			VARCHAR(20)  not null,
	tenDotHienMau	NVARCHAR (100) not null,
	noiDung			NVARCHAR(300) NOT NULL,
	doiTuongThamGia	NVARCHAR (100) not null,
	diaChiToChuc	NVARCHAR (150) not null,
	soLuong			int  not null,
	ngayBatDauDK	DATE not null,	
	ngayKetThucDK	DATE not null,
	ngayToChuc		DATETIME not null,
	tgCapNhat		DATETIME DEFAULT GETDATE()null,
	trangThai		NVARCHAR(50) 
)
GO


--Table 12 PhieuDKHM
CREATE TABLE PhieuDKHM(
	idPDKHM			VARCHAR(20) PRIMARY KEY,
	idDTCHM			VARCHAR(20) NOT NULL,
	idTTCN			VARCHAR(20) NOT NULL,
	benhKhac		NVARCHAR(100) null,
	trangThai		bit DEFAULT '1' CHECK ( trangThai IN ( '0', '1' ) ), --0: thất bại, 1: Thành công
	ghiChu			NVARCHAR(100) null,
	tgDuKien		DATETIME null,
	sutCan			BIT NOT NULL,
	noiHach			BIT NOT NULL,
	chamCu			BIT NOT NULL,
	xamMinh			BIT NOT NULL,
	duocTruyenMau	BIT NOT NULL,
	suDungMatuy		BIT NOT NULL,
	NguyCoHIV		BIT NOT NULL,
	QHTD			BIT NOT NULL,
	tiemVacXin		BIT NOT NULL,
	dungTKS			BIT NOT NULL,
	biSot			BIT NOT NULL,
	dTTT			BIT NOT NULL,
	dangMangThai	BIT NOT NULL
)
GO





--Table 13 
CREATE TABLE chiTietDHM(
	idDHM		VARCHAR(20) FOREIGN KEY REFERENCES dbo.DotHienMau(IdDHM) not null,
	idDVLK		VARCHAR(20) FOREIGN KEY REFERENCES dbo.DonViLienKet(IdDVLK) not null,
	idNVYT		VARCHAR(20)  FOREIGN KEY REFERENCES dbo.NhanVienYTe(IdNVYT) not null,
	ngayDK		DATE  DEFAULT GETDATE()null,
	trangThai	NVARCHAR(50)  null
)
GO


--Table 14 KetQuaHienMau
CREATE TABLE KetQuaHienMau(
	IdKQHM			VARCHAR(20) PRIMARY KEY,
	idDTCHM			VARCHAR(20)  NOT NULL,
	idPDKHM			VARCHAR(20)  FOREIGN KEY REFERENCES dbo.PhieuDKHM(IdPDKHM) not null,
	nhomMau			VARCHAR(10)  NULL,
	nguoiKham		NVARCHAR (50)  null,
	nguoiXN			NVARCHAR (50)  null,
	nguoiLayMau		NVARCHAR (50)  null,
	canNang			float  null,
	machMau			VARCHAR(10)  null,
	tinhTrangLS		NVARCHAR (50) null,
	huyetAp			VARCHAR(10)   null,
	luongMauHien	int  null,
	hienMau			BIT  null,
	noiDung			NVARCHAR(150)  NULL,
	HST				int,
	HBV				int,
	MSD				int,
	phanUng			NVARCHAR (50) ,	
	thoiGianLayMau	DATETIME  DEFAULT GETDATE()not null,
	ghiChu			NVARCHAR (50) ,
	trangThai		NVARCHAR(50) 
)
GO

--Table 15 DanhSachNhanVienThucHien
CREATE TABLE DSNVTH(
	idDTCHM		VARCHAR(20) ,
	idNVYT		VARCHAR(20)  FOREIGN KEY REFERENCES dbo.NhanVienYTe(IdNVYT) not null
)
GO

--Table 16 LichSuHienMau
CREATE TABLE LichSuHienMau(
	idTK			VARCHAR(20) FOREIGN KEY REFERENCES dbo.TaiKhoan(IdTK)  ON DELETE CASCADE ON UPDATE CASCADE not null,
	idKQHM			VARCHAR(20) FOREIGN KEY REFERENCES dbo.KetQuaHienMau(IdKQHM) not null,
	quaTang			NVARCHAR(50),
	anhChungNhan	IMAGE 
)
GO

--RÀNG BUỘC CÁC BẢNG--

--BẢNG 2---TaiKhoan
ALTER TABLE TaiKhoan
	ADD CONSTRAINT FK_TK_idRole FOREIGN KEY (idQuyen) REFERENCES Quyen(IdQuyen)ON DELETE CASCADE ON UPDATE CASCADE,
		CONSTRAINT CK_TK_userNameL CHECK ( userName LIKE '[A-Za-z0-9]%@gmail.com'
										OR userName LIKE '[A-Za-z0-9]%@yahoo.com.vn'
										OR userName LIKE '[A-Za-z0-9]%@[A-Za-z]%.[A-Za-z]%.[A-Za-z]%.vn'
										OR userName LIKE '[A-Za-z0-9]%@[A-Za-z]%.[A-Za-z]%.com' 
									  OR userName LIKE '[A-Za-z0-9]%@[A-Za-z0-9]%.[A-Za-z]%.com.vn'
										OR userName LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
	
GO



---- BẢNG 3--DotHienMau
--ALTER TABLE DotHienMau
--	ADD  CONSTRAINT CK_DHM_TrangThai check(trangThai in ( N'Hết hạn' , N'Đang diễn ra',N'Đủ số lượng'))
--GO

-- BẢNG 4--ThongTinCaNhan
ALTER TABLE ThongTinCaNhan
	ADD CONSTRAINT FK_TTCN_IdTK			FOREIGN KEY (idTK) REFERENCES TaiKhoan(idTK)ON DELETE CASCADE ON UPDATE CASCADE,	
		CONSTRAINT CHECK_TTCN_NGAYSINH	CHECK (dateDiff(year, ngaySinh, getdate())>=18  ),
		CONSTRAINT CHECK_PDKHM_soLanHM	CHECK (soLanHM >=0  or soLanHM <30  ),
		CONSTRAINT CHECK_TTCN_EMAIL		CHECK ( Email LIKE '[A-Za-z0-9]%@gmail.com' OR Email LIKE '[A-Za-z0-9]%@sv.ute.udn.vn'
											 OR Email LIKE '[A-Za-z0-9]%@[A-Za-z].com.vn' OR Email LIKE '[A-Za-z0-9]%@[A-Za-z]%.[A-Za-z]%.[A-Za-z]%.vn'),
		CONSTRAINT CHECK_TTCN1_soDT		CHECK ( soDT LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]' 
											or soDT LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'  ),
		CONSTRAINT CHECK_TTCN_CCCD		CHECK ( CCCD LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]' or CCCD LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
		CONSTRAINT CHECK_TTCN_nhomMau	CHECK (nhomMau in ( 'A+','A-','B+','B-','AB+','AB-','O+','O-'))	
GO

-- BẢNG 5--BenhVien
ALTER TABLE BenhVien
	ADD CONSTRAINT CHECK_BV_EMAILBV CHECK ( Email LIKE '[A-Za-z0-9]%@gmail.com' OR Email LIKE '[A-Za-z0-9]%@yahoo.com.vn'
											 OR Email LIKE '[A-Za-z0-9]%@[A-Za-z]%.[A-Za-z]%.[A-Za-z]%.vn'
											 OR Email LIKE '[A-Za-z0-9]%@[A-Za-z]%.[A-Za-z]%.com' 
											 OR Email LIKE '[A-Za-z0-9]%@[A-Za-z0-9]%.[A-Za-z]%.com.vn'),
		CONSTRAINT CHECK_BV_sdt2 CHECK ( soDTBV LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]' 
									or  soDTBV LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'
									or  soDTBV LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]' 
									or  soDTBV LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')	
GO

-- BẢNG 6--ChucVu


-- BẢNG 7--
ALTER TABLE NhanVienYTe
	ADD CONSTRAINT FK_NVYT_IdKChucVu FOREIGN KEY (idChucVu) REFERENCES ChucVu(IdChucVu)ON DELETE CASCADE ON UPDATE CASCADE,		
		CONSTRAINT CK_NVYT_nhiemVu check(nhiemVu in ( N'Lấy máu' , N'Khám sức khỏe',N'Xét nghiệm máu','Thêm nhân viên'))
		--CONSTRAINT CK_NVYT_TrangThai check(trangThai in ( N'Hoạt động' , N'Nghỉ',N'Đã phân công'))
GO

-- BẢNG 8--DonViLienKet
ALTER TABLE DonViLienKet
	ADD CONSTRAINT CHECK_DV_EMAILBV CHECK ( Email LIKE '[A-Za-z0-9]%@gmail.com' OR Email LIKE '[A-Za-z0-9]%@yahoo.com.vn'
											 OR Email LIKE '[A-Za-z0-9]%@[A-Za-z]%.[A-Za-z]%.[A-Za-z]%.vn'
											 OR Email LIKE '[A-Za-z0-9]%@[A-Za-z]%.[A-Za-z]%.vn'
											 OR Email LIKE '[A-Za-z0-9]%@[A-Za-z]%.[A-Za-z]%.com' 
											 OR Email LIKE '[A-Za-z0-9]%@[A-Za-z0-9]%.[A-Za-z]%.com.vn'),
		CONSTRAINT  CHECK_DV_SODIENTHOAIBV CHECK (	soDT LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]' 
												or  soDT LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]' 
												or  soDT LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]' 
												or  soDT LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')	
GO

-- BẢNG 9 PhieuYCNM
ALTER TABLE PhieuYCNM
	ADD CONSTRAINT FK_D_IdKNhanVienYTe FOREIGN KEY (idNVYT) REFERENCES NhanVienYTe(IdNVYT)ON DELETE CASCADE ON UPDATE CASCADE,	
		CONSTRAINT CHECK_DV_soLuong CHECK (soLuong >=1 )
		--CONSTRAINT CK_PhieuYCNM_TrangThai check(trangThai in ( N'Chờ Duyệt' , N'Đã duyệt',N'Đã tiếp nhận', N'Hủy' ))	
GO

-- BẢNG 10 ChiTietPhanCong
ALTER TABLE ChiTietPhanCong
	ADD	CONSTRAINT FK_CTPC_idPhieuYCNM	FOREIGN KEY (idPhieuYCNM) REFERENCES PhieuYCNM(IdPhieuYCNM) ,
		CONSTRAINT FK_CTPC_DVLK1		FOREIGN KEY (idDVLK) REFERENCES DonViLienKet(IdDVLK)ON DELETE CASCADE ON UPDATE CASCADE
	--	CONSTRAINT CK_CTPC_TrangThai	check(trangThai in ( N'Chờ duyệt' , N'Đã duyệt',N'Đã tiếp nhận', N'Hủy' ))	
GO

-- BẢNG 11 DotToChucHM
ALTER TABLE DotToChucHM
	ADD 
		CONSTRAINT FK_DTCHM_IdDHM		FOREIGN KEY (idDHM) REFERENCES DotHienMau(IdDHM)ON DELETE CASCADE ON UPDATE CASCADE,
		CONSTRAINT CHECK_DTCHM_soLuong	CHECK (soLuong >=1 or soLuong <=1000)
	--	CONSTRAINT CK_DTCHM_TrangThai	check(trangThai in ( N'Chờ duyệt' , N'Đã duyệt',N'Đã tiếp nhận', N'Hủy' ))	
GO

-- BẢNG 12 PhieuDKHM
ALTER TABLE PhieuDKHM
	ADD CONSTRAINT FK_PDKHM_TTCNid		FOREIGN KEY (idTTCN) REFERENCES ThongTinCaNhan(IdTTCN)ON DELETE CASCADE ON UPDATE CASCADE,
		CONSTRAINT FK_PDKHM_DTCHMid		FOREIGN KEY (idDTCHM) REFERENCES DotToChucHM(IdDTCHM)ON DELETE CASCADE ON UPDATE CASCADE,
		CONSTRAINT CHECK_PDKHM_sutCan	default 0 for sutCan,			
		CONSTRAINT CHECK_PDKHM_noiHach	default 0 for noiHach	,		
		CONSTRAINT CHECK_PDKHM_chamCu	default 0 for chamCu	,		
		CONSTRAINT CHECK_PDKHM_xamMinh	default 0 for xamMinh	,		
		CONSTRAINT CHECK_PDKHM_dtm		default 0 for duocTruyenMau	,	
		CONSTRAINT CHECK_PDKHM_sdMT		default 0 for suDungMatuy,		
		CONSTRAINT CHECK_PDKHM_ncHIV	default 0 for NguyCoHIV	,	
		CONSTRAINT CHECK_PDKHM_QHTD		default 0 for QHTD,	
		CONSTRAINT CHECK_PDKHM_tiemVC	default 0 for tiemVacXin,		
		CONSTRAINT CHECK_PDKHM_dungTKS	default 0 for dungTKS,			
		CONSTRAINT CHECK_PDKHM_biSot	default 0 for biSot	,		
		CONSTRAINT CHECK_PDKHM_dTTT		default 0 for dTTT	,		
		CONSTRAINT CHECK_PDKHM_DMT		default 0 for dangMangThai	
	--	CONSTRAINT CK_PDKHM_TrangThai	check(trangThai in ( N'Chờ Duyệt' ,N'Đăng ký thành công', N'Hủy' ))	
GO

-- BẢNG 14--
ALTER TABLE KetQuaHienMau
	ADD CONSTRAINT FK_KQHM_idDotTC	FOREIGN KEY (idDTCHM)	REFERENCES DotToChucHM(IdDTCHM)ON DELETE CASCADE ON UPDATE CASCADE,		
		CONSTRAINT CHECK_KQHM_nhomMau	CHECK (nhomMau in ( 'A+','A-','B+','B-','AB+','AB-','O+','O-')),
		CONSTRAINT CHECK_KQHM_HST		CHECK (HST >=1 ),
		CONSTRAINT CHECK_KQHM_HBV		CHECK (HBV >=1 ),
		CONSTRAINT CHECK_KQHM_MSD		CHECK (MSD >=1 ),
		CONSTRAINT CHECK_KQHM_luongMauHien	CHECK (luongMauHien in ( '250','350','450')),
		CONSTRAINT CHECK_KQHM_hienMau		default 0 for hienMau	,
		CONSTRAINT CHECK_KQHM_canNang		CHECK (canNang	 >=1 )
	--	CONSTRAINT CK_KQHM_TrangThai	check(trangThai in ( N'Đang cập nhật' ,N'Đã cập nhật', N'Chưa cập nhật' ))		
GO
-- BẢNG 15--
ALTER TABLE DSNVTH
	ADD CONSTRAINT FK_DSNVTH_idDotHienMau	FOREIGN KEY (idDTCHM)	REFERENCES DotToChucHM(IdDTCHM)ON DELETE CASCADE ON UPDATE CASCADE		
GO


---Nhập dữ liệu--
--B1
INSERT INTO dbo.Quyen
        ( IdQuyen, tenQuyen )
VALUES  ( 'Q01', N'Admin'),
		( 'Q02', N'Ban chỉ đạo'),
		( 'Q03', N'Đơn vị liên kết'),	
		( 'Q04', N'Bệnh viện'),
		( 'Q05', N'Nhân viên y tế'),
		( 'Q06', N'Người dùng')	
GO
--B2
INSERT INTO dbo.TaiKhoan
        (IdTK,idQuyen, userName ,password)
VALUES  ('TK01', 'Q01', 'admin@gmail.com','123123'),
		('TK02', 'Q02', 'banchidao@gmail.com','123123'),
		('TK03', 'Q03', 'donviLK@gmail.com','123123'),
		('TK04', 'Q04', 'bvDaKhoa@gmail.com','123123'),
		('TK05', 'Q05', 'nhanvien1@gmail.com','123123'),
		('TK06', 'Q06', 'nguoidung1@gmail.com','123123'),
		('TK07', 'Q06', 'nguoidung2@gmail.com','123123'),
		('TK08', 'Q06', 'nguoidung3@gmail.com','123123'),
		('TK09', 'Q06', 'nguoidung4@gmail.com','123123'),
		('TK10', 'Q03', 'demodvlk@gmail.com','123123'),
		('TK11', 'Q04', 'demobv@gmail.com','123123')
GO



--B3
INSERT INTO dbo.DotHienMau
		(IdDHM, TenDHM,noiDung,tgBatDau,tgKetThuc,trangThai)
VALUES	('DHM01',N'Hiến máu nhân đạo quý 1 năm 2021',N'Bổ sung nguồn máu cho thành phố Đà Nẵng','07/01/2021','07/03/2021','0'),
		('DHM02',N'Hiến máu nhân đạo quý 2 năm 2021',N'Bổ sung nguồn máu cho thành phố Đà Nẵng','07/04/2021','07/06/2021','0'),
		('DHM03',N'Hiến máu nhân đạo quý 3 năm 2021',N'Bổ sung nguồn máu cho thành phố Đà Nẵng','07/07/2021','07/09/2021','0'),
		('DHM04',N'Hiến máu nhân đạo quý 4 năm 2021',N'Bổ sung nguồn máu cho thành phố Đà Nẵng','07/10/2021','07/12/2021','1')
GO
--B4
INSERT INTO dbo.ThongTinCaNhan
		(idTTCN,idTK,hoTen,Email,CCCD,soDT,ngaySinh,gioiTinh,diaChi,ngheNghiep,trinhDo,nhomMau,soLanHM,trangThai )
VALUES	('TT01','TK06',N'Trần Võ Lập','knvolap@sv.ute.udn.vn','215496444222','0375163016','30/04/2000','',N'10 Thanh Sơn- Hải Châu - Đà Nẵng',N'Sinh Viên',N'Kỹ sư','A-','1',''),
		('TT02','TK07',N'Nguyễn Ngọc Huy','ngochuy@gmail.com','215496444111','0375163333','16/06/2000','',N'40 Hải Hồ - Hải Châu - Đà Nẵng',N'Sinh Viên',N'Kỹ sư','A-','1',''),
		('TT03','TK08',N'Minh Hiếu','minhhieu@sv.ute.udn.vn','215496444333','0375165555','20/04/1999','',N'10 Thanh Long- Hải Châu - Đà Nẵng',N'Sinh Viên',N'Kỹ sư','A-','1','')
GO

--B5
INSERT INTO dbo.BenhVien
		(IdBenhVien, TenBenhVien,diaChi,Email,soDTBV,minhChung,trangThai)
VALUES	('BV01',N'Đa Khoa Đà Nẵng',N'50 Cao Thắng - Hải Châu - Đà Nẵng','bvdakhoa@gmail.com','0900944488','https://images.app.goo.gl/C8uuYjnrWkNDF1sJA',''),
		('BV02',N'Ung Bướu Đà Nẵng',N'28 Đường Hoàng Thị Loan- Liên Chiểu - Hòa Khánh - Đà Nẵng','benhvienungbuou@gmail.com','0888004468','https://images.app.goo.gl/C8uuYjnrWkNDF1sJA',''),
		('BV03',N'Chưa có',N'Đà Nẵng','demobv1@gmail.com','0888004499','https://images.app.goo.gl/C8uuYjnrWkNDF1sJA','')
GO

--B6
INSERT INTO dbo.ChucVu
		(IdChucVu,TenChucVu )
VALUES	('CV01',N'Giám Đốc'),
		('CV02',N'Trưởng Khoa'),
		('CV03',N'Y tá'),		
		('CV07',N'Chưa có')
GO

--B7
INSERT INTO dbo.NhanVienYTe
		(IdNVYT,idTK,idBenhVien,idChucVu,khoa,nhiemVu,trinhDo,trangThai )
VALUES	('NV01','TK04','BV01','CV01',N'Khoa Truyền máu',N'Khám sức khỏe',N'Giáo sư ','1'),
		('NV02','TK05','BV01','CV02',N'Khoa Truyền máu',N'Lấy máu',N'Đại học','1'),
		('NV03','TK11','BV02','CV03',N'Khoa Truyền máu',N'Lấy máu',N'Đại học','1')
		
GO

--B8
INSERT INTO dbo.DonViLienKet
		(IdDVLK, idTK,TenDonVi,diaChi,Email,soDT,minhChung,trangThai)
VALUES	('DV01','TK03',N'Đại học Sư Phạm Kỹ Thuật',N'48 Cao Thắng-Hải Châu-Đà Nẵng','SPKT@ute.udn.com','0900944499','https://images.app.goo.gl/C8uuYjnrWkNDF1sJA','1')	,	
		('DV02','TK10',N'Chưa có',N'Đà Nẵng','demodv1@gmail.com','0900944411','https://images.app.goo.gl/C8uuYjnrWkNDF1sJA','1')
GO

--B9
INSERT INTO dbo.PhieuYCNM
		(IdPhieuYCNM, idNVYT,idDHM,soLuong,tgBatDau,tgKetThuc,tgCapnhat,lyDo,trangThai)
VALUES	('PYC01','NV01','DHM04','10','15/11/2021','18/11/2021','07:30 19/11/2021',N'Cần nguồn máu hiếm. Nhóm máu AB+',N'Chờ Duyệt'),
		('PYC02','NV01','DHM04','10','15/12/2021','18/12/2021','07:30 19/12/2021',N'Cần nguồn máu hiếm. Nhóm máu AB+',N'Chờ Duyệt')
GO

--B11
INSERT INTO dbo.DotToChucHM
		(IdDTCHM, idDHM,tenDotHienMau,noiDung,doiTuongThamGia,diaChiToChuc,soLuong,ngayBatDauDK,ngayKetThucDK,ngayToChuc,tgCapNhat,trangThai)
VALUES	('TCHM01','DHM04',N'HMNĐ quý 4 năm 2021 lần 1',N'Bổ sung nguồn máu cho thành phố Đà Nẵng',N'Sinh viên',N'48 cao Thắng - HC- ĐN','300','15/11/2021','18/11/2021','07:30 19/11/2021','15/11/2021',N'Chờ Duyệt'),
		('TCHM02','DHM04',N'HMNĐ quý 4 năm 2021 lần 2',N'Bổ sung nguồn máu cho thành phố Đà Nẵng',N'Sinh viên',N'48 cao Thắng - HC- ĐN','300','15/12/2021','18/12/2021','07:30 19/12/2021','15/12/2021',N'Chờ Duyệt')	
GO

--B12
INSERT INTO dbo.PhieuDKHM
		( idPDKHM,idTTCN,idDTCHM,benhKhac,trangThai,ghiChu,tgDuKien)
VALUES	('PDK01','TT02','TCHM01',N'không có','',N'k có','20/11/2021'),
		('PDK02','TT03','TCHM01',N'không có','',N'k có','20/11/2021')
GO

--B13
INSERT INTO dbo.chiTietDHM
		( idDHM,idDVLK,idNVYT,ngayDK,trangThai)
VALUES	('DHM03','DV01','NV01' ,'20/7/2021',N'Đăng ký thành công' ),
		('DHM04','DV01','NV01','20/11/2021',N'Đăng ký thành công' )
GO




--ALTER TABLE DonViLienKet
--DROP CONSTRAINT CHECK_DV_SODIENTHOAIBV;

--ALTER TABLE DonViLienKet
--DROP COLUMN soDT;

--ALTER TABLE DonViLienKet
--  ADD trangThai		bit DEFAULT '1' CHECK ( trangThai IN ( '0', '1' ) ), --0: chưa kích hoạt, 1: đã kích hoạt

--Mật khẩu mã hóa -> copy bỏ vào mật khẩu của sql sau khi run file
---  4297f44b13955235245b2497399d7a93
