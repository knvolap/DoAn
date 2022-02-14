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
    public class DotToChucHMServices
    {
        private DbContextHM db = null;

        public DotToChucHMServices()
        {
            db = new DbContextHM();
        }
        public DotToChucHM GetByIdDTCHM(string id)
        {
            return db.DotToChucHMs.Where(b => b.IdDTCHM.CompareTo(id) == 0).FirstOrDefault();
        }

        public List<DotToChucHM> ListBV()
        {
            return db.DotToChucHMs.ToList();
        }
        public IEnumerable<DotToChucHM> ListAllDTCHM(string keysearch, int page, int pageSize)
        {
            IEnumerable<DotToChucHM> model = db.DotToChucHMs;
            if (!string.IsNullOrEmpty(keysearch))
            {
                model = model.Where(x => x.IdDTCHM.Contains(keysearch) || x.tenDotHienMau.Contains(keysearch) || x.idChiTietDHM.Contains(keysearch)  );
            }
            return model.OrderByDescending(x => x.IdDTCHM).ThenBy(x => x.tenDotHienMau).ToPagedList(page, pageSize);
        }

        public IEnumerable<ChiTietvsToChucHMView> ListAllBaiDang2(string searchString1, string idbv ,int page, int pageSize)
        {
            var query = from dtc in db.DotToChucHMs
                        join ct in db.chiTietDHMs on dtc.idChiTietDHM equals ct.IdChiTietDHM                        
                        join bv in db.BenhViens on ct.idBenhVien equals bv.IdBenhVien

                        join dhm in db.DotHienMaus on ct.idDHM equals dhm.IdDHM
                        join dv in db.DonViLienKets on ct.idDVLK equals dv.IdDVLK

                        where ct.idBenhVien == idbv && ct.IdChiTietDHM==dtc.idChiTietDHM

                        select new { dtc, ct, dv, bv, dhm };

            //nv.idTTCN ->nv.IdNVYT ->bv.IdBenhVien ->ct.IdChiTietDHM ->dtc.IdDTCHM
            //check từ khóa có tồn tại hay k
            if (!string.IsNullOrEmpty(searchString1))
            {
                query = query.Where(x => x.dhm.TenDHM.Contains(searchString1) || x.bv.TenBenhVien.Contains(searchString1) || x.dv.TenDonVi.Contains(searchString1)
                                     || x.dtc.tenDotHienMau.Contains(searchString1) || x.dtc.noiDung.Contains(searchString1));
            }
            var result = query.Select(x => new ChiTietvsToChucHMView()
            {
                IdDTCHM = x.dtc.IdDTCHM,
                tenDTCHM = x.dtc.tenDotHienMau,
                noiDung = x.dtc.noiDung,

                ngayBatDauDK = x.dtc.ngayBatDauDK,
                ngayKetThucDK = x.dtc.ngayKetThucDK,
                ngayToChuc = x.dtc.ngayToChuc,
                trangThai = x.dtc.trangThai,
                doiTuongThamGia = x.dtc.diaChiToChuc,
                diaChiToChuc = x.dtc.diaChiToChuc,
                soLuong = x.dtc.soLuong,

                IdChiTietDHM = x.ct.IdChiTietDHM,
                idDHM = x.ct.idDHM,
                idBenhVien = x.ct.idBenhVien,
                idDVLK = x.ct.idDVLK,

                tenDHM = x.dhm.TenDHM,
                tenBenhVien = x.bv.TenBenhVien,
                tenDVLK = x.dv.TenDonVi,
            }).OrderByDescending(x => x.IdDTCHM).ThenBy(q => q.tenDTCHM).ToPagedList(page, pageSize);
            return result;
        }

        public IEnumerable<ChiTietvsToChucHMView> ListAllBaiDang3(string searchString1, string idbv, int page, int pageSize)
        {
            var query = from dtc in db.DotToChucHMs
                        join ct in db.chiTietDHMs on dtc.idChiTietDHM equals ct.IdChiTietDHM
                        join bv in db.BenhViens on ct.idBenhVien equals bv.IdBenhVien

                        join dhm in db.DotHienMaus on ct.idDHM equals dhm.IdDHM
                        join dv in db.DonViLienKets on ct.idDVLK equals dv.IdDVLK

                        where ct.idDVLK == idbv && ct.IdChiTietDHM == dtc.idChiTietDHM

                        select new { dtc, ct, dv, bv, dhm };

            //nv.idTTCN ->nv.IdNVYT ->bv.IdBenhVien ->ct.IdChiTietDHM ->dtc.IdDTCHM
            //check từ khóa có tồn tại hay k
            if (!string.IsNullOrEmpty(searchString1))
            {
                query = query.Where(x => x.dhm.TenDHM.Contains(searchString1) || x.bv.TenBenhVien.Contains(searchString1) || x.dv.TenDonVi.Contains(searchString1)
                                     || x.dtc.tenDotHienMau.Contains(searchString1) || x.dtc.noiDung.Contains(searchString1));
            }
            var result = query.Select(x => new ChiTietvsToChucHMView()
            {
                IdDTCHM = x.dtc.IdDTCHM,
                tenDTCHM = x.dtc.tenDotHienMau,
                noiDung = x.dtc.noiDung,

                ngayBatDauDK = x.dtc.ngayBatDauDK,
                ngayKetThucDK = x.dtc.ngayKetThucDK,
                ngayToChuc = x.dtc.ngayToChuc,
                trangThai = x.dtc.trangThai,
                doiTuongThamGia = x.dtc.diaChiToChuc,
                diaChiToChuc = x.dtc.diaChiToChuc,
                soLuong = x.dtc.soLuong,

                IdChiTietDHM = x.ct.IdChiTietDHM,
                idDHM = x.ct.idDHM,
                idBenhVien = x.ct.idBenhVien,
                idDVLK = x.ct.idDVLK,

                tenDHM = x.dhm.TenDHM,
                tenBenhVien = x.bv.TenBenhVien,
                tenDVLK = x.dv.TenDonVi,

            }).OrderByDescending(x => x.IdDTCHM).ThenBy(q => q.tenDTCHM).ToPagedList(page, pageSize);
            return result;
        }


        public IEnumerable<ChiTietvsToChucHMView> ListAllBaiDang(string searchString1, int page, int pageSize)
        {
            var query = from dtc in db.DotToChucHMs
                        join ct in db.chiTietDHMs on dtc.idChiTietDHM equals ct.IdChiTietDHM
                        join dv in db.DonViLienKets on ct.idDVLK equals dv.IdDVLK
                        join bv in db.BenhViens on ct.idBenhVien equals bv.IdBenhVien
                        join dhm in db.DotHienMaus on ct.idDHM equals dhm.IdDHM
                        select new { dtc, ct ,dv,bv,dhm };

            //check từ khóa có tồn tại hay k
            if (!string.IsNullOrEmpty(searchString1))
            {
                query = query.Where(x => x.dhm.TenDHM.Contains(searchString1) || x.bv.TenBenhVien.Contains(searchString1) || x.dv.TenDonVi.Contains(searchString1)
                                     || x.dtc.tenDotHienMau.Contains(searchString1) || x.dtc.noiDung.Contains(searchString1));
            }
            var result = query.Select(x => new ChiTietvsToChucHMView()
            {
                IdDTCHM =x.dtc.IdDTCHM,
                tenDTCHM=x.dtc.tenDotHienMau,
                noiDung = x.dtc.noiDung,
               
                ngayBatDauDK=x.dtc.ngayBatDauDK,
                ngayKetThucDK = x.dtc.ngayKetThucDK,
                ngayToChuc = x.dtc.ngayToChuc,
                trangThai= x.dtc.trangThai,
                doiTuongThamGia =x.dtc.diaChiToChuc,
                diaChiToChuc=x.dtc.diaChiToChuc,

                IdChiTietDHM = x.ct.IdChiTietDHM,
                idDHM = x.ct.idDHM,
                idBenhVien = x.ct.idBenhVien,
                idDVLK = x.ct.idDVLK,

                tenDHM = x.dhm.TenDHM,
                tenBenhVien =x.bv.TenBenhVien,
                tenDVLK = x.dv.TenDonVi,

            }).OrderByDescending(x => x.IdDTCHM).ThenBy(q => q.tenDTCHM).ToPagedList(page, pageSize);
            return result;
        }


        public ChiTietvsToChucHMView GetByIdDTCHM2(string id)
        {
            var query = from dtc in db.DotToChucHMs
                        join ct in db.chiTietDHMs on dtc.idChiTietDHM equals ct.IdChiTietDHM
                        join dv in db.DonViLienKets on ct.idDVLK equals dv.IdDVLK
                        join bv in db.BenhViens on ct.idBenhVien equals bv.IdBenhVien
                        join dhm in db.DotHienMaus on ct.idDHM equals dhm.IdDHM
                        where dtc.IdDTCHM == id
                        select new { dtc, ct, dv, bv, dhm };                               
            var result = query.Select(x => new ChiTietvsToChucHMView()
            {
                IdDTCHM = x.dtc.IdDTCHM,
                tenDTCHM = x.dtc.tenDotHienMau,
                noiDung = x.dtc.noiDung,

                ngayBatDauDK = x.dtc.ngayBatDauDK,
                ngayKetThucDK = x.dtc.ngayKetThucDK,
                ngayToChuc = x.dtc.ngayToChuc,
                trangThai = x.dtc.trangThai,
                doiTuongThamGia = x.dtc.diaChiToChuc,
                diaChiToChuc = x.dtc.diaChiToChuc,

                IdChiTietDHM = x.ct.IdChiTietDHM,
                idDHM = x.ct.idDHM,
                idBenhVien = x.ct.idBenhVien,
                idDVLK = x.ct.idDVLK,

                tenDHM = x.dhm.TenDHM,
                tenBenhVien = x.bv.TenBenhVien,
                tenDVLK = x.dv.TenDonVi,

            }).SingleOrDefault();
            return result;
        }
        public bool CheckTimeDuplicate(DateTime t1, DateTime t2, DateTime t3, DateTime s1, DateTime s2)
        {
            if (t1 >= s1 && t2 <= s2 && t3 <= s2)
            {
                return true;
            }
            else
            { return false; }
        }
        //  s1 = DotHienMau.ngayBD
        //  s2 = DotHienMau.ngayKT
        //  t1 = BaiDang.ngayBatDauDK
        //  t2 = BaiDang.ngayKetThucDK
        //  t3 = BaiDang.ngayToChuc

        public bool Delete(string id)
        {
            try
            {
                var ls = db.DotToChucHMs.Find(id);
                db.DotToChucHMs.Remove(ls);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool checkDangKyHienMau(string idTTCN,string idBaiDang)
        {
            var result = db.PhieuDKHMs.Where(x => x.idTTCN == idTTCN).ToList();
            foreach (var item in result)
            {
                if (item.idDTCHM == idBaiDang)
                {
                    if (result.Count() >= 1)
                    {
                        return false;
                    }
                    break;

                }
            }
            
            return true;
        }

        public bool checkPhanChiaNV(string idTTCN)
        {
            var result = db.DSNVTHs.Where(x => x.idNVYT == idTTCN).ToList();
            if (result.Count() >= 1)
            {
                return false;
            }
            return true;
        }
        public bool isExistBaiDang(string tenDot)
        {
            DotToChucHM kh = db.DotToChucHMs.Where(t => t.tenDotHienMau == tenDot).FirstOrDefault();
            if (kh != null)
            {
                return true;
            }
            return false;
        }
    }
}
