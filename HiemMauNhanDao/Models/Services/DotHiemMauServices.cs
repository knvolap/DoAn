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
    public class DotHiemMauServices
    {
        private DbContextHM db = null;

        public DotHiemMauServices()
        {
            db = new DbContextHM();
        }

        public List <DotHienMau> ListAll()
        {
            return db.DotHienMaus.ToList();
        }

        //Danh sach DHM
        public IEnumerable<DotHienMau> ListAllPagingDHM(string searchString1, string searchString2, int page, int pagesize)
        {
            IEnumerable<DotHienMau> model = db.DotHienMaus;
            if (!string.IsNullOrEmpty(searchString1))
            {
                model = model.Where(x => x.IdDHM.Contains(searchString1) || x.TenDHM.Contains(searchString1) || x.noiDung.Contains(searchString1) );
            }
            if (!string.IsNullOrEmpty(searchString2))
            {
                model = model.Where(x => x.tgBatDau.ToString()==searchString2  );
            }
            return model.OrderByDescending(x=>x.IdDHM).ThenBy(x=>x.tgKetThuc).ToPagedList(page, pagesize);
        }
      
        public DotHienMau GetByIdDHM(string id)
        {
            return db.DotHienMaus.Where(s => s.IdDHM.CompareTo(id) == 0).SingleOrDefault();
        }
        public List<chiTietDHM> ListAllChiTietDHM()
        {
            return db.chiTietDHMs.ToList();
        }
  

        //Them
        public void ThemDHM (DotHienMau dotHienMau)
        {
            var id = db.DotHienMaus.Max(x => x.IdDHM);
            string phanDau = id.Substring(0, 3);
            int so = Convert.ToInt32(id.Substring(3, 2)) + 1;
            var baiDang = new DotHienMau()
            {
                IdDHM = so > 9 ? phanDau + so : phanDau + "0" + so,
                TenDHM = dotHienMau.TenDHM,
                noiDung = dotHienMau.noiDung,
                tgBatDau = dotHienMau.tgBatDau,
                tgKetThuc = dotHienMau.tgKetThuc ,
                trangThai = dotHienMau.trangThai
            };
            db.DotHienMaus.Add(baiDang);
            db.SaveChanges();
        }

        //sua  DHM
        public void SuaDHM(DotHienMau dotHienMau)
        {
            DotHienMau dhm = GetByIdDHM(dotHienMau.IdDHM);
            dhm.TenDHM = dotHienMau.TenDHM;
            dhm.noiDung = dotHienMau.noiDung;
            dhm.tgBatDau = dotHienMau.tgBatDau;
            dhm.tgKetThuc = dotHienMau.tgKetThuc;
            dhm.trangThai = dotHienMau.trangThai;
            db.SaveChanges();
        }

        //xoa
        public bool XoaDHM(string id)
        {
            try
            {
                var xoaDHM = db.DotHienMaus.Find(id);
                db.DotHienMaus.Remove(xoaDHM);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }







        //public List<> GetById(string tenDHM)
        //{
        //    return db.DotHienMaus.SingleOrDefault(x => x.TenDHM == tenDHM);
        //}


    }
}
