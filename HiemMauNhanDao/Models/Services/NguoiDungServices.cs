using Models.EF;
using Models.ViewModel;
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
         db.SaveChanges();
           
        }

        public bool XoaTTCN(string id)
        {
            var XoaTTCN = db.ThongTinCaNhans.Find(id);
            db.ThongTinCaNhans.Remove(XoaTTCN);
            return true;
        }
        public IEnumerable<ChiTietPDKHvsKQHMView> GetByIdLSDK(string keysearch, string id,int page, int pagesize)
        {
            var query = from pdk in db.PhieuDKHMs
                        join tt in db.ThongTinCaNhans on pdk.idTTCN equals tt.IdTTCN
                        join dtchm in db.DotToChucHMs on pdk.idDTCHM equals dtchm.IdDTCHM
                        where tt.IdTTCN == id
                        select new { pdk, tt, dtchm };
            //check từ khóa có tồn tại hay k
            if (!string.IsNullOrEmpty(keysearch))
            {
                query = query.Where(x => x.pdk.idDTCHM.Contains(keysearch) || x.pdk.idPDKHM.Contains(keysearch)
                || x.tt.IdTTCN.Contains(keysearch) || x.tt.hoTen.Contains(keysearch) || x.tt.soDT.Contains(keysearch));
            }
            //tạo biến result -> hiển thị sp ->           
            var result = query.Select(x => new ChiTietPDKHvsKQHMView()
            {
                IdTTCN = x.tt.IdTTCN,
                hoTen = x.tt.hoTen,
                gioiTinh = x.tt.gioiTinh,
                soDT = x.tt.soDT,
                tgDuKien=x.pdk.tgDuKien,
                idPDKHM = x.pdk.idPDKHM,
                idDTCHM = x.pdk.idDTCHM,
                tenDTCHM = x.dtchm.tenDotHienMau

            }).OrderByDescending(x => x.idDTCHM).ThenBy(q => q.idPDKHM).ToPagedList(page, pagesize);
            return result;
        }

       
        public ChiTietPDKHvsKQHMView GetByIdLSDK2(string id)
        {
            var query = from kqhm in db.KetQuaHienMaus                      
                        join pdk in db.PhieuDKHMs on kqhm.idPDKHM equals pdk.idPDKHM
                        join tt in db.ThongTinCaNhans on pdk.idTTCN equals tt.IdTTCN
                        join dtchm in db.DotToChucHMs on pdk.idDTCHM equals dtchm.IdDTCHM
                        where tt.IdTTCN == id
                        select new { pdk, tt, kqhm , dtchm };

            //tạo biến result -> hiển thị sp ->           
            var result = query.Select(x => new ChiTietPDKHvsKQHMView()
            {
                IdTTCN = x.tt.IdTTCN,
                hoTen = x.tt.hoTen,
                gioiTinh = x.tt.gioiTinh,
                soDT = x.tt.soDT,
                coQuanTH = x.tt.coQuanTH,
                soLanHM = x.tt.soLanHM,
                trinhDo = x.tt.trinhDo,
                ngheNghiep = x.tt.ngheNghiep,
                diaChi = x.tt.diaChi,
                ngaySinh = x.tt.ngaySinh,
                CCCD = x.tt.CCCD,
                idPDKHM = x.pdk.idPDKHM,
                idDTCHM = x.pdk.idDTCHM,
                benhKhac = x.pdk.benhKhac,
                tgDuKien = x.pdk.tgDuKien,
                sutCan = x.pdk.sutCan,
                hienMau = x.pdk.hienMau,
                noiHach = x.pdk.noiHach,
                chamCu = x.pdk.chamCu,
                xamMinh = x.pdk.xamMinh,
                duocTruyenMau = x.pdk.duocTruyenMau,
                suDungMatuy = x.pdk.suDungMatuy,
                NguyCoHIV = x.pdk.NguyCoHIV,
                QHTD = x.pdk.QHTD,
                tiemVacXin = x.pdk.tiemVacXin,
                dungTKS = x.pdk.dungTKS,
                biSot = x.pdk.biSot,
                dTTT = x.pdk.dTTT,
                ungThu = x.pdk.ungThu,
                dangMangThai = x.pdk.dangMangThai,
                xacNhan = x.pdk.xacNhan,
                trangThai=x.pdk.trangThai,

                IdKQHM = x.kqhm.IdKQHM,
                nhomMau = x.kqhm.nhomMau,
                trangThai2 = x.kqhm.trangThai,
                nguoiKham = x.kqhm.idnguoiKham,
                nguoiXN = x.kqhm.idnguoiXN,
                nguoiLayMau = x.kqhm.idnguoiLayMau,
                canNang = x.kqhm.canNang,
                machMau = x.kqhm.machMau,
                tinhTrangLS = x.kqhm.tinhTrangLS,
                huyetAp = x.kqhm.huyetAp,
                luongMauHien = x.kqhm.luongMauHien,
                hienMau2 = x.kqhm.hienMau,
                noiDung = x.kqhm.noiDung,
                HST = x.kqhm.HST,
                HBV = x.kqhm.HBV,
                MSD = x.kqhm.MSD,
                phanUng = x.kqhm.phanUng,
                thoiGianLayMau = x.kqhm.tgLayMau,
                thoiGianKham = x.kqhm.tgKham,
                thoiGianXN = x.kqhm.tgXetNghiem,
                thoiGianCapNhat = x.kqhm.tgCapNhat,
                ghiChu = x.kqhm.ghiChu,
            }).SingleOrDefault();
            return result;
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
