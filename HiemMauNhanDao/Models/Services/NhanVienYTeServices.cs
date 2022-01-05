using Models.EF;
using Models.ViewModel;
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
        public List<ThongTinCaNhan> ListTKNV()
        {
            return db.ThongTinCaNhans.ToList();
        }
        public List<NhanVienYTe> ListNV()
        {
            return db.NhanVienYTes.ToList();
        }
        public bool isExistIDTK(string userName)
        {
            NhanVienYTe kh = db.NhanVienYTes.Where(t => t.idTTCN == userName).FirstOrDefault();
            if (kh != null)
            {
                return true;
            }
            return false;
        }
        public bool isExistNVYT(string id)
        {
            NhanVienYTe kh = db.NhanVienYTes.Where(t => t.IdNVYT == id).FirstOrDefault();
            if (kh != null)
            {
                return true;
            }
            return false;
        }
        //DanhSachTK
        public IEnumerable<ThongTinCaNhan>ListAllPageTKNV (string searchString1, int page, int pageSize)
        {
            IEnumerable<ThongTinCaNhan> model = db.ThongTinCaNhans;
            if(!string.IsNullOrEmpty(searchString1))
            {
                model = model.Where(x => x.IdTTCN.Contains(searchString1) || x.userName.Contains(searchString1));
            }
            return model.OrderByDescending(x => x.idQuyen= "Q05").ToPagedList(page, pageSize);
        }

        public IEnumerable<NhanVienView> GetListNVYT(string keysearch, int page, int pagesize)
        {
            var query = from nv in db.NhanVienYTes                      
                        join tt in db.ThongTinCaNhans on nv.idTTCN equals tt.IdTTCN
                        join cv in db.ChucVus on nv.idChucVu equals cv.IdChucVu
                        select new { nv, tt,cv };

            //check từ khóa có tồn tại hay k
            if (!string.IsNullOrEmpty(keysearch))
            {
                query = query.Where(x => x.nv.IdNVYT.Contains(keysearch) || x.nv.idChucVu.Contains(keysearch)
                || x.tt.IdTTCN.Contains(keysearch) || x.tt.hoTen.Contains(keysearch) || x.tt.soDT.Contains(keysearch));
            }
            //tạo biến result -> hiển thị sp ->           
            var result = query.Select(x => new NhanVienView()
            {
                IdTTCN = x.tt.IdTTCN,
                hoTen = x.tt.hoTen,
                gioiTinh = x.tt.gioiTinh,
                CCCD = x.tt.CCCD,
                IdNVYT = x.nv.IdNVYT,
                idBenhVien =x.nv.idBenhVien,
                idChucVu=x.cv.IdChucVu,
                tenChucVu=x.cv.TenChucVu

                
            }).OrderByDescending(x => x.IdNVYT).ThenBy(q => q.IdTTCN).ToPagedList(page, pagesize);
            return result;
        }



        public void addNVYT2(NhanVienView nhanVienView)
        {           
            var id1= db.ThongTinCaNhans.Max(x => x.IdTTCN);
            string phanDau1 = id1.Substring(0, 2);
            int so1 = Convert.ToInt32(id1.Substring(2, 2)) + 1;       
            var nhanvien2 = new NhanVienView()
            {
                IdTTCN = so1 > 9 ? phanDau1 + so1 : phanDau1 + "0" + so1,               
                idQuyen = nhanVienView.idQuyen = "Q05",
                userName = nhanVienView.userName,
                password = nhanVienView.password,
                trangThai = nhanVienView.trangThai               
            };
            var id2 = db.NhanVienYTes.Max(x => x.IdNVYT);
            string phanDau2 = id2.Substring(0, 2);
            int so2 = Convert.ToInt32(id2.Substring(2, 2)) + 1;
            var nhanvien3 = new NhanVienView()
            {
                IdNVYT = so1 > 9 ? phanDau2 + so2 : phanDau2 + "0" + so2,
                IdTTCN = nhanVienView.IdTTCN,
                idBenhVien = nhanVienView.idBenhVien ,
                idChucVu = nhanVienView.idChucVu,
                khoa = nhanVienView.khoa,
                trinhDo = nhanVienView.trinhDo
            };
            //db.ThongTinCaNhans.Add(nhanvien2);
            //db.NhanVienYTes.Add(nhanvien3);
            db.SaveChanges();
        }


        public void addNVYT(ThongTinCaNhan taiKhoan)
        {
            var id = db.ThongTinCaNhans.Max(x => x.IdTTCN);
            string phanDau = id.Substring(0, 2);
            int so = Convert.ToInt32(id.Substring(2, 2)) + 1;
            var nhanvien = new ThongTinCaNhan()
            {
                IdTTCN = so > 9 ? phanDau + so : phanDau + "0" + so,
                idQuyen = taiKhoan.idQuyen = "Q05",
                userName = taiKhoan.userName,
                password = taiKhoan.password,
                trangThai = taiKhoan.trangThai
            };
            db.ThongTinCaNhans.Add(nhanvien);
            db.SaveChanges();
        }
        public ThongTinCaNhan GetByIdNVYT(string id)
        {
            return db.ThongTinCaNhans.Where(s => s.IdTTCN.CompareTo(id) == 0).SingleOrDefault();
        }

        public void editNVYT(ThongTinCaNhan taiKhoan)
        {
            ThongTinCaNhan nvyt = GetByIdNVYT(taiKhoan.IdTTCN);
            nvyt.password = taiKhoan.password;

            db.SaveChanges();
        }

        //xoa
        public bool XoaNVYT(string id)
        {
            try
            {
                var xoaNVYT = db.ThongTinCaNhans.Find(id);
                db.ThongTinCaNhans.Remove(xoaNVYT);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
