using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Services
{
   public class BenhVienServices
    {
        private DbContextHM db = null;
        public BenhVienServices()
        {
            db = new DbContextHM();
        }
        public BenhVien GetByIdBenhVien(string id)
        {
            return db.BenhViens.Where(b => b.IdBenhVien.CompareTo(id) == 0).FirstOrDefault();
        }

        public List<BenhVien> ListBV()
        {
            return db.BenhViens.ToList();
        }
        public IEnumerable<BenhVien> ListAllBenhVien(string keysearch, int page, int pageSize)
        {
            IEnumerable<BenhVien> model = db.BenhViens;
            if(!string.IsNullOrEmpty(keysearch))
            {
                model = model.Where(x => x.IdBenhVien.Contains(keysearch) || x.TenBenhVien.Contains(keysearch) || x.soDTBV.Contains(keysearch) || x.Email.Contains(keysearch));
            }
            return model.OrderByDescending(x => x.IdBenhVien).ThenBy(x => x.TenBenhVien).ToPagedList(page, pageSize);
        }

        public void AddBV(BenhVien benhVien, string fileName)
        {
            var id = db.BenhViens.Max(x => x.IdBenhVien);
            string phanDau = id.Substring(0, 2);
            int so = Convert.ToInt32(id.Substring(2, 2)) + 1;
            var benhVien1 = new BenhVien()
            {
                IdBenhVien = so > 9 ? phanDau + so : phanDau + "0" + so,
                TenBenhVien = benhVien.TenBenhVien,
                diaChi = benhVien.diaChi,
                Email = benhVien.Email,
                minhChung = benhVien.minhChung,
                soDTBV = benhVien.soDTBV,
                trangThai = benhVien.trangThai
            };
            db.BenhViens.Add(benhVien1);
            db.SaveChanges();
        }

        public void EditBV(BenhVien benhVien, string fileName)
        {
            try
            {
                BenhVien bv = GetByIdBenhVien(benhVien.IdBenhVien);
                bv.TenBenhVien = benhVien.TenBenhVien;
                bv.Email = benhVien.Email;
                bv.diaChi = benhVien.diaChi;
                bv.minhChung = fileName;
                bv.soDTBV = benhVien.soDTBV;
                bv.trangThai = benhVien.trangThai;
                db.SaveChanges();
            }
            catch
            {
                Console.WriteLine("Cập nhật thất bại");
            }
        }
       
        public bool Delete(string id)
         {
            try
            {
                var ls = db.BenhViens.Find(id);

                db.BenhViens.Remove(ls);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public object ListAllBenhVien(string searchString, int page)
        {
            throw new System.NotImplementedException();
        }

    }
}
