using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Services
{
    public class TCDHMServices
    {
        private DbContextHM db = null;
    
        public TCDHMServices()
        {
            db = new DbContextHM();
        }
        public IEnumerable<DotToChucHM> ListAlldTCHM(string keysearch, int page, int pageSize)
        {
            IEnumerable<DotToChucHM> model = db.DotToChucHMs;
            if (!string.IsNullOrEmpty(keysearch))
            {
                model = model.Where(x => x.IdDTCHM.Contains(keysearch) || x.tenDotHienMau.Contains(keysearch) || x.noiDung.Contains(keysearch));
            }
            return model.OrderByDescending(x => x.IdDTCHM).ThenBy(x => x.tenDotHienMau).ToPagedList(page, pageSize);
        }

        public List<DotToChucHM> GetListDTCHM()
        {
            return db.DotToChucHMs.ToList();
        }

        public DotToChucHM GetByIdTCHM(string id)
        {
            return db.DotToChucHMs.Where(b => b.IdDTCHM.CompareTo(id) == 0).FirstOrDefault();
        }

    }
}
