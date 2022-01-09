using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Services
{
   public class ChiTietDHMServices
    {
        private DbContextHM db = null;
        public ChiTietDHMServices()
        {
            db = new DbContextHM();
        }
        public chiTietDHM GetByIdChiTietDHM(string id)
        {
            return db.chiTietDHMs.Where(b => b.IdChiTietDHM.CompareTo(id) == 0).FirstOrDefault();
        }
 
        public List<chiTietDHM> ListCTDHM()
        {
            return db.chiTietDHMs.ToList();
        }
        public IEnumerable<chiTietDHM> ListAllCTDHM(string keysearch, int page, int pageSize)
        {
            IEnumerable<chiTietDHM> model = db.chiTietDHMs;
            if (!string.IsNullOrEmpty(keysearch))
            {
                model = model.Where(x => x.IdChiTietDHM.Contains(keysearch) || x.BenhVien.TenBenhVien.Contains(keysearch) || x.DonViLienKet.TenDonVi.Contains(keysearch) ) ;
            }
            return model.OrderByDescending(x => x.IdChiTietDHM).ThenBy(x => x.idBenhVien).ToPagedList(page, pageSize);
        }

        public bool Delete(string id)
        {
            try
            {
                var ls = db.chiTietDHMs.Find(id);
                db.chiTietDHMs.Remove(ls);
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
