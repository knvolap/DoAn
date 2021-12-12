using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Services
{
    public class QuanLyTKServices
    {
        private DbContextHM db = null;

        public QuanLyTKServices()
        {
            db = new DbContextHM();
        }



        public TaiKhoan GetByIdTK(string id)
        {
            return db.TaiKhoans.Where(b => b.IdTK.CompareTo(id) == 0).FirstOrDefault();
        }
        public List<TaiKhoan> ListAllTK()
        {
            return db.TaiKhoans.ToList();
        }
        public List<Quyen> ListAllQ()
        {
            return db.Quyens.ToList();
        }
        public IEnumerable<TaiKhoan> ListAllTaiKhoan(string keysearch, int page, int pageSize)
        {
            IEnumerable<TaiKhoan> model = db.TaiKhoans;
            if (!string.IsNullOrEmpty(keysearch))
            {
                model = model.Where(x => x.IdTK.Contains(keysearch) || x.userName.Contains(keysearch) || x.idQuyen.Contains(keysearch)  );
            }
            return model.OrderByDescending(x => x.IdTK).ThenBy(x => x.userName).ToPagedList(page, pageSize);
        }

        public void themTK(TaiKhoan taiKhoan)
        {
            var id = db.TaiKhoans.Max(x => x.IdTK);
            string phanDau = id.Substring(0, 2);
            int so = Convert.ToInt32(id.Substring(2, 2)) + 1;
            var tk1 = new TaiKhoan()
            {
                IdTK = so > 9 ? phanDau + so : phanDau + "0" + so,
                idQuyen =taiKhoan.idQuyen,
                userName = taiKhoan.userName,
                password = taiKhoan.password,
                trangThai = taiKhoan.trangThai
            };
            db.TaiKhoans.Add(tk1);
            db.SaveChanges();
        }


        //Sửa
        public void SuaTk(TaiKhoan taiKhoan)
        {
            TaiKhoan tk = GetByIdTK(taiKhoan.IdTK);
            tk.idQuyen = taiKhoan.idQuyen;
            tk.userName = taiKhoan.userName;
            tk.password = taiKhoan.password;
            tk.trangThai = taiKhoan.trangThai;       
            db.SaveChanges();
        }

        //xóa sản phẩm
        public void xoaTK(string id)
        {
            TaiKhoan nd = GetByIdTK(id);
            db.TaiKhoans.Remove(nd);
            db.SaveChanges();
        }



    }



}
