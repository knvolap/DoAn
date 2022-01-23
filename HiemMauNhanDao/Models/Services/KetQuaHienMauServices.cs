using Models.EF;
using Models.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Services
{
    public class KetQuaHienMauServices
    {
        private DbContextHM db = null;

        public KetQuaHienMauServices()
        {
            db = new DbContextHM();
        }

        public List<ThongTinCaNhan> ListAllTTCN2()
        {
            return db.ThongTinCaNhans.ToList();
        }
        public List<PhieuDKHM> ListAllPDHHM2()
        {
            return db.PhieuDKHMs.ToList();
        }
        public List<KetQuaHienMau> ListAllKQHM()
        {
            return db.KetQuaHienMaus.ToList();
        }
        public IEnumerable<ChiTietPDKHvsKQHMView> GetListKQHM(string keysearch ,string idNTG, string idnvyt, int page, int pagesize)
        {
            var query = from pdk in db.PhieuDKHMs
                        join tt in db.ThongTinCaNhans on pdk.idTTCN equals tt.IdTTCN
                        join dtchm in db.DotToChucHMs on pdk.idDTCHM equals dtchm.IdDTCHM
                        join ct in db.chiTietDHMs on dtchm.idChiTietDHM equals ct.IdChiTietDHM
                        join bv in db.BenhViens on ct.idBenhVien equals bv.IdBenhVien

                        join dhm in db.DotHienMaus on ct.idDHM equals dhm.IdDHM
                        join dv in db.DonViLienKets on ct.idDVLK equals dv.IdDVLK

                        join nvyt in db.NhanVienYTes on bv.IdBenhVien equals nvyt.idBenhVien
                        join dsnv in db.DSNVTHs on nvyt.IdNVYT equals dsnv.idNVYT

                        where ct.idBenhVien == idNTG && ct.IdChiTietDHM == dtchm.idChiTietDHM && dsnv.idNVYT == idnvyt
                        select new { pdk, tt , dtchm,dhm,bv,ct , nvyt , dsnv };
            //check từ khóa có tồn tại hay k
            if (!string.IsNullOrEmpty(keysearch))
            {
                query = query.Where(x => x.pdk.idDTCHM.Contains(keysearch) || x.pdk.idPDKHM.Contains(keysearch)
                || x.tt.IdTTCN.Contains(keysearch) || x.tt.hoTen.Contains(keysearch) || x.tt.soDT.Contains(keysearch));
            }
            //tạo biến result -> hiển thị sp ->           
            var result = query.Select(x => new ChiTietPDKHvsKQHMView()
            {
                IdTTCN = x.tt.IdTTCN,
                hoTen = x.tt.hoTen,
                gioiTinh = x.tt.gioiTinh,
                soDT = x.tt.soDT,
                idPDKHM = x.pdk.idPDKHM,
                idDTCHM = x.pdk.idDTCHM,
                trangThai = x.pdk.trangThai,
                tenDTCHM =x.dtchm.tenDotHienMau,
                nhiemVu = x.dsnv.nhiemVu,

            }).OrderByDescending(x => x.idDTCHM).ThenBy(q => q.idPDKHM).ToPagedList(page, pagesize);
            return result;
        }
        public IEnumerable<ChiTietPDKHvsKQHMView> GetListKQHM2(string keysearch, string idNTG, string idnvyt, int page, int pagesize)
        {
            var query = from kqhm in db.KetQuaHienMaus
                        join pdk in db.PhieuDKHMs on kqhm.idPDKHM equals pdk.idPDKHM
                        join tt in db.ThongTinCaNhans on pdk.idTTCN equals tt.IdTTCN
                        join dtchm in db.DotToChucHMs on pdk.idDTCHM equals dtchm.IdDTCHM
                        join ct in db.chiTietDHMs on dtchm.idChiTietDHM equals ct.IdChiTietDHM
                        join bv in db.BenhViens on ct.idBenhVien equals bv.IdBenhVien
                        join dhm in db.DotHienMaus on ct.idDHM equals dhm.IdDHM
                        join dv in db.DonViLienKets on ct.idDVLK equals dv.IdDVLK

                        join nvyt in db.NhanVienYTes on bv.IdBenhVien equals nvyt.idBenhVien
                        join dsnv in db.DSNVTHs on nvyt.IdNVYT equals dsnv.idNVYT
                       
                        where ct.idBenhVien == idNTG && ct.IdChiTietDHM == dtchm.idChiTietDHM && dsnv.idNVYT==idnvyt
                        
                        select new { pdk, tt, kqhm, dtchm , ct, bv, dhm , dv , dsnv, nvyt };
            //check từ khóa có tồn tại hay k
            if (!string.IsNullOrEmpty(keysearch))
            {
                query = query.Where(x => x.pdk.idDTCHM.Contains(keysearch) || x.pdk.idPDKHM.Contains(keysearch) || x.dtchm.tenDotHienMau.Contains(keysearch)
                || x.tt.IdTTCN.Contains(keysearch) || x.tt.hoTen.Contains(keysearch) || x.tt.soDT.Contains(keysearch));
            }
            //tạo biến result -> hiển thị sp ->           
            var result = query.Select(x => new ChiTietPDKHvsKQHMView()
            {
                IdTTCN = x.tt.IdTTCN,
                hoTen = x.tt.hoTen,
                gioiTinh = x.tt.gioiTinh,
                soDT = x.tt.soDT,

                idPDKHM = x.pdk.idPDKHM,
                idDTCHM = x.pdk.idDTCHM,

                tenDTCHM = x.dtchm.tenDotHienMau,

                nhomMau = x.kqhm.nhomMau,
                trangThai2 = x.kqhm.trangThai,
                IdKQHM = x.kqhm.IdKQHM,
                nhiemVu=x.dsnv.nhiemVu,
            }).OrderByDescending(x => x.idDTCHM).ThenBy(q => q.idPDKHM).ToPagedList(page, pagesize);
            return result;
        }
        public TTCNvsPhieuDKHMView GetByIdTTDK(string id)
        { 
            var query = from pdk in db.PhieuDKHMs                       
                        join tt in db.ThongTinCaNhans on pdk.idTTCN equals tt.IdTTCN
                        join dtchm in db.DotToChucHMs on pdk.idDTCHM equals dtchm.IdDTCHM
                        where pdk.idPDKHM == id
                        select new { pdk, tt , dtchm };


            //tạo biến result -> hiển thị sp ->           
            var result = query.Select(x => new TTCNvsPhieuDKHMView()
            {
                IdTTCN = x.tt.IdTTCN,
                hoTen = x.tt.hoTen,
                gioiTinh = x.tt.gioiTinh,
                soDT = x.tt.soDT,
                coQuanTH=x.tt.coQuanTH,
                soLanHM = x.tt.soLanHM,
                trinhDo = x.tt.trinhDo,
                ngheNghiep = x.tt.ngheNghiep,
                diaChi  =x.tt.diaChi,
                ngaySinh =x.tt.ngaySinh,
                CCCD =x.tt.CCCD,
                nhomMau = x.tt.nhomMau,
                idPDKHM = x.pdk.idPDKHM,
                idDTCHM = x.pdk.idDTCHM,
                benhKhac      =x.pdk.benhKhac ,     
                tgDuKien      =x.pdk.tgDuKien ,
                sutCan        =x.pdk.sutCan ,
                hienMau       =x.pdk.hienMau ,
                noiHach       =x.pdk.noiHach,
                chamCu        =x.pdk.chamCu ,
                xamMinh       =x.pdk.xamMinh ,
                duocTruyenMau =x.pdk.duocTruyenMau ,
                suDungMatuy   =x.pdk.suDungMatuy ,
                NguyCoHIV     =x.pdk.NguyCoHIV ,
                QHTD          =x.pdk.QHTD ,
                tiemVacXin    =x.pdk.tiemVacXin ,
                dungTKS       =x.pdk.dungTKS ,
                biSot         =x.pdk.biSot ,
                dTTT          =x.pdk.dTTT,
                ungThu        = x.pdk.ungThu,
                dangMangThai  =x.pdk.dangMangThai,
                xacNhan       =x.pdk.xacNhan,

            }).SingleOrDefault();
            return result;
        }
        public KetQuaHienMau GetByIdKQHM(string id)
        {
            return db.KetQuaHienMaus.Where(x => x.IdKQHM.CompareTo(id) == 0).SingleOrDefault();
        }
        public ChiTietPDKHvsKQHMView GetByIdTTDK2(string id)
        {
            var query = from kqhm in db.KetQuaHienMaus
                        join pdk in db.PhieuDKHMs on kqhm.idPDKHM equals pdk.idPDKHM
                        join tt in db.ThongTinCaNhans on pdk.idTTCN equals tt.IdTTCN
                        join dtchm in db.DotToChucHMs on pdk.idDTCHM equals dtchm.IdDTCHM
                        where tt.IdTTCN == id
                        select new { pdk, tt, kqhm , dtchm };

            //tạo biến result -> hiển thị sp ->           
            var result = query.Select(x => new ChiTietPDKHvsKQHMView()
            {
                IdTTCN = x.tt.IdTTCN,
                hoTen = x.tt.hoTen,
                gioiTinh = x.tt.gioiTinh,
                soDT = x.tt.soDT,
                coQuanTH = x.tt.coQuanTH,
                soLanHM = x.tt.soLanHM,
                trinhDo = x.tt.trinhDo,
                ngheNghiep = x.tt.ngheNghiep,
                diaChi = x.tt.diaChi,
                ngaySinh = x.tt.ngaySinh,
                CCCD = x.tt.CCCD,
                idPDKHM = x.pdk.idPDKHM,
                idDTCHM = x.pdk.idDTCHM,
                benhKhac = x.pdk.benhKhac,
                tgDuKien = x.pdk.tgDuKien,
                sutCan = x.pdk.sutCan,
                hienMau = x.pdk.hienMau,
                noiHach = x.pdk.noiHach,
                chamCu = x.pdk.chamCu,
                xamMinh = x.pdk.xamMinh,
                duocTruyenMau = x.pdk.duocTruyenMau,
                suDungMatuy = x.pdk.suDungMatuy,
                NguyCoHIV = x.pdk.NguyCoHIV,
                QHTD = x.pdk.QHTD,
                tiemVacXin = x.pdk.tiemVacXin,
                dungTKS = x.pdk.dungTKS,
                biSot = x.pdk.biSot,
                dTTT = x.pdk.dTTT,
                ungThu = x.pdk.ungThu,
                dangMangThai = x.pdk.dangMangThai,
                xacNhan = x.pdk.xacNhan,


                IdKQHM = x.kqhm.IdKQHM,
                nhomMau = x.kqhm.nhomMau,
                trangThai2 = x.kqhm.trangThai,

                idnguoiKham = x.kqhm.idnguoiKham,
                idnguoiXN = x.kqhm.idnguoiXN,
                idnguoiLayMau = x.kqhm.idnguoiLayMau,

                nguoiKham = x.kqhm.nguoiKham,
                nguoiXN = x.kqhm.nguoiXN,
                nguoiLayMau = x.kqhm.nguoiLayMau,

                canNang = x.kqhm.canNang,
                machMau = x.kqhm.machMau,
                tinhTrangLS = x.kqhm.tinhTrangLS,
                huyetAp = x.kqhm.huyetAp,
                luongMauHien = x.kqhm.luongMauHien,
                hienMau2 = x.kqhm.hienMau,
                noiDung = x.kqhm.noiDung,
                HST = x.kqhm.HST,
                HBV = x.kqhm.HBV,
                MSD = x.kqhm.MSD,
                phanUng = x.kqhm.phanUng,
                thoiGianLayMau = x.kqhm.tgLayMau,
                thoiGianKham = x.kqhm.tgKham,
                thoiGianXN = x.kqhm.tgXetNghiem,
                thoiGianCapNhat= x.kqhm.tgCapNhat,
                ghiChu = x.kqhm.ghiChu,

            }).SingleOrDefault();
            return result;
        }

        //duuyet
        public bool ChangeStatus4(string id)
        {
            var chiTiet = db.PhieuDKHMs.Find(id);
            chiTiet.trangThai = !chiTiet.trangThai;
            db.SaveChanges();
            return chiTiet.trangThai;
        }

        public bool Delete(string id)
        {
            try
            {
                var ls = db.KetQuaHienMaus.Find(id);
                db.KetQuaHienMaus.Remove(ls);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
