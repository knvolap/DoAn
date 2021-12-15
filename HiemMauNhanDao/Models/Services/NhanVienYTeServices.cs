using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Services
{
    
    public class NhanVienYTeServices
    {
        private DbContextHM db = null;

        public NhanVienYTeServices()
        {
            db = new DbContextHM();
        }
        public List<TaiKhoan> ListTKNV()
        {
            return db.TaiKhoans.ToList();
        }

        //DanhSachTK
        public IEnumerable<TaiKhoan>ListAllPageTKNV (string searchString1, int page, int pageSize)
        {
            IEnumerable<TaiKhoan> model = db.TaiKhoans;
            if(!string.IsNullOrEmpty(searchString1))
            {
                model = model.Where(x => x.IdTK.Contains(searchString1) || x.userName.Contains(searchString1));
            }
            return model.OrderByDescending(x => x.idQuyen= "Q05").ToPagedList(page, pageSize);
        }



        public void addNVYT(TaiKhoan taiKhoan)
        {
            var id = db.TaiKhoans.Max(x => x.IdTK);
            string phanDau = id.Substring(0, 2);
            int so = Convert.ToInt32(id.Substring(2, 2)) + 1;
            var nhanvien = new TaiKhoan()
            {
                IdTK = so > 9 ? phanDau + so : phanDau + "0" + so,
                idQuyen = taiKhoan.idQuyen = "Q05",
                userName = taiKhoan.userName,
                password = taiKhoan.password,
                trangThai = taiKhoan.trangThai
            };
            db.TaiKhoans.Add(nhanvien);
            db.SaveChanges();
        }
        public TaiKhoan GetByIdNVYT(string id)
        {
            return db.TaiKhoans.Where(s => s.IdTK.CompareTo(id) == 0).SingleOrDefault();
        }

        public void editNVYT(TaiKhoan taiKhoan)
        {
            TaiKhoan nvyt = GetByIdNVYT(taiKhoan.IdTK);
            nvyt.password = taiKhoan.password;

            db.SaveChanges();
        }

        //xoa
        public bool XoaNVYT(string id)
        {
            try
            {
                var xoaNVYT = db.TaiKhoans.Find(id);
                db.TaiKhoans.Remove(xoaNVYT);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
