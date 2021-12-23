using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Services
{
    public class NguoiDungServices
    {
        private DbContextHM db = null;
        public NguoiDungServices ()
        {
            db = new DbContextHM();
        }

        public List <ThongTinCaNhan> ListAllttcn()
        {
            return db.ThongTinCaNhans.ToList();
        }
        //Danh sach DHM
        public IEnumerable<ThongTinCaNhan> ListAllPagingTTCN(string searchString1, string searchString2, int page, int pagesize)
        {
            IEnumerable<ThongTinCaNhan> model = db.ThongTinCaNhans;
            if (!string.IsNullOrEmpty(searchString1))
            {
                model = model.Where(x => x.IdTTCN.Contains(searchString1) || x.hoTen.Contains(searchString1) || x.soDT.Contains(searchString1) 
                                    || x.userName.Contains(searchString1) ||  x.CCCD.Contains(searchString1) );
            }
            //if (!string.IsNullOrEmpty(searchString2))
            //{
            //    model = model.Where(x => x.tgBatDau.ToString() == searchString2);
            //}
            return model.OrderByDescending(x => x.IdTTCN).ThenBy(x => x.CCCD).ToPagedList(page, pagesize);
        }

        public ThongTinCaNhan GetByIdTTCN (string id)
        {
            return db.ThongTinCaNhans.Where(x => x.IdTTCN.CompareTo(id) == 0).SingleOrDefault();
        }

        public bool isExistTaiKhoan(string userName)
        {
            ThongTinCaNhan kh = db.ThongTinCaNhans.Where(t => t.userName == userName).FirstOrDefault();
            if (kh != null)
            {
                return true;
            }
            return false;
        }
        public bool isExistSDT(string soDT)
        {
            ThongTinCaNhan kh = db.ThongTinCaNhans.Where(t => t.soDT == soDT).FirstOrDefault();
            if (kh != null)
            {
                return true;
            }
            return false;
        }
      
        public bool isExistCCCD(string cccd)
        {
            ThongTinCaNhan kh = db.ThongTinCaNhans.Where(t => t.CCCD == cccd).FirstOrDefault();
            if (kh != null)
            {
                return true;
            }
            return false;
        }
        public bool isExistHotTen(string hoten)
        {
            ThongTinCaNhan kh = db.ThongTinCaNhans.Where(t => t.hoTen == hoten).FirstOrDefault();
            if (kh == null)
            {
                return true;
            }
            return false;
        }


        //Them
        public void ThemTTCN(ThongTinCaNhan thongTinCaNhan)
        {
            var id = db.ThongTinCaNhans.Max(x => x.IdTTCN);
            string phanDau = id.Substring(0, 2);
            int so = Convert.ToInt32(id.Substring(2, 2)) + 1;
            var ttcn = new ThongTinCaNhan()
            {
                IdTTCN = so > 9 ? phanDau + so : phanDau + "0" + so,
                idQuyen = thongTinCaNhan.idQuyen ,
                userName = thongTinCaNhan.userName,
                password = thongTinCaNhan.password,
                hoTen   =thongTinCaNhan.hoTen,
                CCCD    = thongTinCaNhan.CCCD,
                soDT    = thongTinCaNhan.soDT,
                ngaySinh = thongTinCaNhan.ngaySinh,
                ngheNghiep = thongTinCaNhan.ngheNghiep,
                gioiTinh = thongTinCaNhan.gioiTinh,
                diaChi = thongTinCaNhan.diaChi,
                trinhDo = thongTinCaNhan.trinhDo,
                soLanHM = thongTinCaNhan.soLanHM,
                coQuanTH = thongTinCaNhan.coQuanTH,
                nhomMau = thongTinCaNhan.nhomMau,
                trangThai = thongTinCaNhan.trangThai
            };
            db.ThongTinCaNhans.Add(ttcn);
            db.SaveChanges();
        }

        public void SuaTTCN2(ThongTinCaNhan thongTinCaNhan)
        {
            ThongTinCaNhan ttcn2 = GetByIdTTCN(thongTinCaNhan.IdTTCN);
            ttcn2.hoTen = thongTinCaNhan.hoTen;
            ttcn2.CCCD = thongTinCaNhan.CCCD;
            ttcn2.soDT = thongTinCaNhan.soDT;
            ttcn2.ngaySinh = thongTinCaNhan.ngaySinh;
            ttcn2.ngheNghiep = thongTinCaNhan.ngheNghiep;
            ttcn2.gioiTinh = thongTinCaNhan.gioiTinh;
            ttcn2.diaChi = thongTinCaNhan.diaChi;
            ttcn2.trinhDo = thongTinCaNhan.trinhDo;
            ttcn2.soLanHM = thongTinCaNhan.soLanHM;
            ttcn2.nhomMau = thongTinCaNhan.nhomMau;
            ttcn2.coQuanTH = thongTinCaNhan.coQuanTH;
            ttcn2.userName = thongTinCaNhan.userName;
            ttcn2.password = thongTinCaNhan.password;
            ttcn2.idQuyen = thongTinCaNhan.idQuyen;
            ttcn2.trangThai = thongTinCaNhan.trangThai;
            try
            {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges

                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public bool XoaTTCN(string id)
        {
            var XoaTTCN = db.ThongTinCaNhans.Find(id);
            db.ThongTinCaNhans.Remove(XoaTTCN);
            return true;
        }

        public void CapNhatTTCN(ThongTinCaNhan thongTinCaNhan)
        {
           
                db.ThongTinCaNhans.AddOrUpdate(thongTinCaNhan);
                db.SaveChanges();
          
        }
        //public void CapNhatTTCN(ThongTinCaNhan thongTinCaNhan)
        //{

        //    try
        //    {
        //        db.ThongTinCaNhans.AddOrUpdate(thongTinCaNhan);
        //        db.SaveChanges();
        //    }
        //    catch (DbEntityValidationException dbEx)
        //    {
        //        foreach (var validationErrors in dbEx.EntityValidationErrors)
        //        {
        //            foreach (var validationError in validationErrors.ValidationErrors)
        //            {
        //                System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
        //            }
        //        }
        //    }
        //}


        //public void SuaTTCN2(ThongTinCaNhan thongTinCaNhan)
        //{
        //    ThongTinCaNhan ttcn2 = GetByIdTTCN(thongTinCaNhan.IdTTCN);
        //    ttcn2.userName = thongTinCaNhan.userName;          
        //    ttcn2.password = thongTinCaNhan.password;
        //    db.SaveChanges();
        //}



    }
}
