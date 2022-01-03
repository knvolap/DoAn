using Models.EF;
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
    }
}
