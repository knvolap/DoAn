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
    public class DKDHMServices
    {
        private DbContextHM db = null;
    
        public DKDHMServices()
        {
            db = new DbContextHM();
        }
        //danh sach DK DHM
        public IEnumerable<DotHiemMauView> ListAllDKDHM(string searchString1 , int page, int pageSize)
        {
            var query = from c in db.chiTietDHMs
                        join dhm in db.DotHienMaus on c.idDHM equals dhm.IdDHM
                        select new { c, dhm };

            //check từ khóa có tồn tại hay k
            if (!string.IsNullOrEmpty(searchString1))
            {
                query = query.Where(x => x.c.idDHM.Contains(searchString1) || x.c.idNVYT.Contains(searchString1)
                                     || x.c.idDVLK.Contains(searchString1) || x.dhm.TenDHM.Contains(searchString1));
            }
            var result = query.Select(x => new DotHiemMauView()
            {
                IdDHM = x.dhm.IdDHM,
                TenDHM = x.dhm.TenDHM,
                tgBatDau = x.dhm.tgBatDau,
                tgKetThuc = x.dhm.tgKetThuc,
                trangThai = x.dhm.trangThai,
                idDVLK = x.c.idDVLK,
                idNVYT = x.c.idNVYT,
                ngayDK = x.c.ngayDK,
            }).OrderByDescending(x => x.IdDHM).ThenBy(q => q.ngayDK).ToPagedList(page, pageSize);
            return result;
        }
        public IEnumerable<DotHienMau> ListAllDKDHM2(string keysearch, int page, int pageSize)
        {
            IEnumerable<DotHienMau> model = db.DotHienMaus;
            if (!string.IsNullOrEmpty(keysearch))
            {
                model = model.Where(x => x.IdDHM.Contains(keysearch) || x.TenDHM.Contains(keysearch)  || x.noiDung.Contains(keysearch));
            }
            return model.OrderByDescending(x => x.IdDHM).ThenBy(x => x.TenDHM).ToPagedList(page, pageSize);
        }

        public List<chiTietDHM> ListAllChiTietDHM()
        {
            return db.chiTietDHMs.ToList();
        }
        public List<DotHienMau> GetDotHienMaus()
        {
            return db.DotHienMaus.ToList();
        }
        public List<DonViLienKet> GetDonViLienKets()
        {
            return db.DonViLienKets.ToList();
        }


        public chiTietDHM GetChiTietDHMID(string id)
        {
            return db.chiTietDHMs.Where(s => s.idDHM.CompareTo(id) == 0).SingleOrDefault();
        }


        public DotHienMau GetDKDHM(string id)
        {
            return db.DotHienMaus.Where(s => s.IdDHM.CompareTo(id) == 0).SingleOrDefault();
        }

        //sua chi tiet DHM
        public void EditCTDHM(chiTietDHM ChiTietDHM)
        {
            chiTietDHM ct = GetChiTietDHMID(ChiTietDHM.idDHM);
            ct.idDVLK = ChiTietDHM.idDHM;
            ct.idNVYT = ChiTietDHM.idNVYT;
            ct.trangThai = ChiTietDHM.trangThai;
            db.SaveChanges();
        }

        //sua DK DHM
        public void EditDKDHM(chiTietDHM ChiTietDHM)
        {
            chiTietDHM ct = GetChiTietDHMID(ChiTietDHM.idDHM);
            ct.idDVLK = ChiTietDHM.idDHM;
            ct.idNVYT = ChiTietDHM.idNVYT;
            ct.idDHM = ChiTietDHM.idDHM;
            ct.ngayDK = ChiTietDHM.ngayDK;
            ct.trangThai = ChiTietDHM.trangThai;
            db.SaveChanges();
        }
        //xoa

        public void DeleteDKDHM(string id)
        {
            chiTietDHM nd = GetChiTietDHMID(id);
            db.chiTietDHMs.Remove(nd);
            db.SaveChanges();
        }

    }
}
