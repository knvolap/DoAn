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



        public ThongTinCaNhan GetByIdTK(string id)
        {
            return db.ThongTinCaNhans.Where(b => b.IdTTCN.CompareTo(id) == 0).FirstOrDefault();
        }
        public List<ThongTinCaNhan> ListAllTK()
        {
            return db.ThongTinCaNhans.ToList();
        }
        public List<Quyen> ListAllQ()
        {
            return db.Quyens.ToList();
        }
        public IEnumerable<ThongTinCaNhan> ListAllTaiKhoan(string keysearch, int page, int pageSize)
        {
            IEnumerable<ThongTinCaNhan> model = db.ThongTinCaNhans;
            if (!string.IsNullOrEmpty(keysearch))
            {
                model = model.Where(x => x.IdTTCN.Contains(keysearch) || x.userName.Contains(keysearch) || x.idQuyen.Contains(keysearch)  );
            }
            return model.OrderByDescending(x => x.IdTTCN).ThenBy(x => x.userName).ToPagedList(page, pageSize);
        }

        public void themTK(ThongTinCaNhan thongTinCaNhan)
        {
            var id = db.ThongTinCaNhans.Max(x => x.IdTTCN);
            string phanDau = id.Substring(0, 2);
            int so = Convert.ToInt32(id.Substring(2, 2)) + 1;
            var tk1 = new ThongTinCaNhan()
            {
                IdTTCN = so > 9 ? phanDau + so : phanDau + "0" + so,
                idQuyen = thongTinCaNhan.idQuyen,
                userName = thongTinCaNhan.userName,
                password = thongTinCaNhan.password,
                hoTen = thongTinCaNhan.hoTen,
                Email = thongTinCaNhan.Email,
                soDT = thongTinCaNhan.soDT,
                CCCD = thongTinCaNhan.CCCD,
                trangThai = thongTinCaNhan.trangThai
            };
            db.ThongTinCaNhans.Add(tk1);
            db.SaveChanges();
        }


        //Sửa
        public void SuaTk(ThongTinCaNhan thongTinCaNhan)
        {        
            ThongTinCaNhan tk = GetByIdTK(thongTinCaNhan.IdTTCN);
            tk.idQuyen = thongTinCaNhan.idQuyen;
            tk.userName = thongTinCaNhan.userName;
            tk.password = thongTinCaNhan.password;
            tk.hoTen = thongTinCaNhan.hoTen;
            tk.Email = thongTinCaNhan.Email;
            tk.soDT = thongTinCaNhan.soDT;
            tk.CCCD = thongTinCaNhan.CCCD;
            tk.trangThai = thongTinCaNhan.trangThai;
            db.SaveChanges();
        }

        //xóa sản phẩm
        public void xoaTK(string id)
        {
            ThongTinCaNhan nd = GetByIdTK(id);
            db.ThongTinCaNhans.Remove(nd);
            db.SaveChanges();
        }

    }



}
