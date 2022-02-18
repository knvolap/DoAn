using HiemMauNhanDao.Areas.Admin.Controllers;
using HiemMauNhanDao.Common;
using Models.EF;
using Models.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiemMauNhanDao.Controllers
{
    public class DVLKController : BaseController2
    {
        DonViLienKetServices _dvlk = new DonViLienKetServices();
        DotToChucHMServices _DTCHM = new DotToChucHMServices();
        private DbContextHM db = new DbContextHM();
        // GET: DVLK
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];

            var tempDVLK = db.DonViLienKets.Where(x => x.idTTCN == session.UserID).FirstOrDefault();

            var dotToChucHMs = new DotToChucHMServices();
            var model = dotToChucHMs.ListAllBaiDang2(searchString, tempDVLK.IdDVLK, page, pageSize);
            ViewBag.SearchStringDTCHM = searchString;
            return View(model);

        }

        [HttpGet]
        public ActionResult DKDVLK( )
        {
            var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];
            string id = session.UserID;
            ViewBag.IdUser = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DKDVLK(DonViLienKet donViLienKet, HttpPostedFileBase file)
        {
            var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];
            if (ModelState.IsValid == false)
            {
                if (_dvlk.isExistIDTK(donViLienKet.idTTCN))
                {
                    SetAlert("Bạn đã đăng ký rồi", "error");
                }
                else if (_dvlk.isExistSDT(donViLienKet.soDT))
                {
                    SetAlert("Số điện thoại đã tồn tại", "error");
                }
                else if (_dvlk.isExistEmail(donViLienKet.Email))
                {
                    SetAlert("Email đã tồn tại", "error");
                }
                else if (file==null)
                {
                    SetAlert("Vui lòng chọn File", "error");
                }
                else
                {
                    donViLienKet.idTTCN = session.UserID;
                    donViLienKet.trangThai = false;
                    string duongDan = Server.MapPath("~/FileUpLoad/dvlk/");
                    string fileName = Path.GetFileName(file.FileName);
                    string fullDuongDan = Path.Combine(duongDan, fileName);
                    file.SaveAs(fullDuongDan);

                    _dvlk.AddDVLK(donViLienKet, fileName);
                    db.SaveChanges();
                    SetAlert("Đăng ký thành công", "success");
                    return RedirectToAction("DKDVLK", "DVLK");
                }
            }
            else
            {
                SetAlert("Đăng ký thất bại!! Vui lòng kiểm tra lại thông tin nhập", "error");
                return RedirectToAction("DKDVLK", "DVLK");
            }
            return View(donViLienKet);
               
        }
        public ActionResult CreateDV()
        {
            var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];

            var tempNVYT = db.DonViLienKets.Where(x => x.idTTCN == session.UserID).FirstOrDefault();

            ViewBag.IdChiTietDHM = new SelectList(db.chiTietDHMs.Where(x => x.idDVLK == tempNVYT.IdDVLK), "IdChiTietDHM", "idChiTietDHM");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDV([Bind(Include = "IdDTCHM,IdChiTietDHM,tenDotHienMau,noiDung,doiTuongThamGia,diaChiToChuc,soLuong,ngayBatDauDK,ngayKetThucDK,ngayToChuc,trangThai")] DotToChucHM dotToChucHM)
        {
            var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];

            string id = db.DotToChucHMs.Max(x => x.IdDTCHM);
            string phanDau = id.Substring(0, 3);
            int so = Convert.ToInt32(id.Substring(3)) + 1;

            var tempDSDK = db.chiTietDHMs.Where(x => x.IdChiTietDHM == dotToChucHM.idChiTietDHM).SingleOrDefault();
            var tempDHM = db.DotHienMaus.Where(x => x.IdDHM == tempDSDK.idDHM).SingleOrDefault();

            if (ModelState.IsValid)
            {
                if (_DTCHM.CheckTimeDuplicate(dotToChucHM.ngayBatDauDK, dotToChucHM.ngayKetThucDK, dotToChucHM.ngayToChuc, tempDHM.tgBatDau, tempDHM.tgKetThuc) == true)
                {
                    if (_DTCHM.isExistBaiDang(dotToChucHM.tenDotHienMau))
                    {
                        SetAlert("Tên bài đăng đã tồn tại !", "error");

                    }
                    else if (dotToChucHM.ngayBatDauDK < dotToChucHM.ngayKetThucDK && dotToChucHM.ngayToChuc >= dotToChucHM.ngayKetThucDK)
                    {
                        dotToChucHM.IdDTCHM = so > 9 ? phanDau + so : phanDau + "0" + so;
                        dotToChucHM.trangThai = true;
                        dotToChucHM.tenNguoiDangBai = session.Name;

                        db.DotToChucHMs.Add(dotToChucHM);
                        db.SaveChanges();
                        SetAlert("Đăng bài thành công", "success");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        SetAlert("Đăng bài thất bại!!! Vui lòng nhập Ngày bắt đầu < Ngày kết thúc < Ngày đăng ký", "error");
                        
                    }
                }
                else
                {
                    SetAlert("Khoảng thời gian đăng ký không nằm trong đợt hiến máu!", "error");
                    
                }
            }
            ViewBag.IdChiTietDHM = new SelectList(db.chiTietDHMs, "IdChiTietDHM", "idChiTietDHM", dotToChucHM.idChiTietDHM);
            return View(dotToChucHM);
        }
        public ActionResult DanhSachDangKy(string searchString, int page = 1, int pageSize = 5)
        {
            var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];
            string id = session.UserID;
            var tempTTCN = db.DonViLienKets.Where(x => x.idTTCN == session.UserID).FirstOrDefault();

            var dsdk = new DonViLienKetServices();
            var model = dsdk.GetByIdDVLK(searchString, tempTTCN.IdDVLK, page, pageSize);
            ViewBag.SearchStringDK = searchString;
            return View(model);
        }

        public ActionResult DangBai()
        {
            return View();
        }


         [HttpGet]
        public ActionResult CapNhatTTDV( )
        {
            var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];
            string id = session.UserID;
            var tempDVLK = db.DonViLienKets.Where(x => x.idTTCN == session.UserID).FirstOrDefault();

            DonViLienKet donViLienKet = db.DonViLienKets.Find(id);
            return View(donViLienKet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CapNhatTTDV([Bind(Include = "IdDVLK,idTTCN,hoTen,")] DonViLienKet model)
        {
            if (ModelState.IsValid)
            {
                if (_dvlk.isExistSDT(model.soDT))
                {
                    SetAlert("Số điện thoại đã tồn tại", "error");
                }
                var dvlk = _dvlk.GetByIdDVLK(model.IdDVLK);
                dvlk.IdDVLK = dvlk.IdDVLK;
                dvlk.idTTCN = dvlk.idTTCN;
                dvlk.diaChi = dvlk.diaChi;
                dvlk.TenDonVi = dvlk.TenDonVi;
                dvlk.soDT = dvlk.soDT;
                dvlk.Email = dvlk.Email;

                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Cập nhật thành công", "success");
                return RedirectToAction("Index", "NguoiDung");
            }
            else
            {
                SetAlert("Cập nhật thất bại", "error");
                return RedirectToAction("CapnhatTTCN", "NguoiDung");
            }
            return View();
        }
        
        
    }
}


//[HttpPost]
//[ValidateAntiForgeryToken]
//public ActionResult DKDVLK(DonViLienKet donViLienKet)
//{
//    var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];
//    if (ModelState.IsValid)
//    {
//        donViLienKet.idTTCN = session.UserID;
//        donViLienKet.trangThai = false;
//        _dvlk.AddDVLK(donViLienKet);
//        SetAlert("Đăng ký thành công", "success");
//        return RedirectToAction("DKDVLK", "DVLK");
//    }
//    else
//    {
//        SetAlert("Đăng ký  thất bại", "error");
//        return RedirectToAction("DKDVLK", "DVLK");
//    }
//}

//[HttpPost]
//[ValidateAntiForgeryToken]
//public ActionResult DKDVLK([Bind(Include = "IdDVLK,idTTCN,trangThai,TenDonVi,Email,soDT,minhChung,diaChi")] DonViLienKet donViLienKet)
//{
//    var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];
//    if (ModelState.IsValid)
//    {
//        if (_dvlk.isExistDVLK(donViLienKet.IdDVLK))
//        {
//            SetAlert("Bạn đã đăng ký rồi", "error");
//        }
//        else if (_dvlk.isExistIDTK(donViLienKet.idTTCN))
//        {
//            SetAlert("Bạn đã đăng ký rồi", "error");
//        }
//        else
//        {
//            donViLienKet.IdDVLK = "DV" + (db.DonViLienKets.Count() + 1).ToString();
//            donViLienKet.idTTCN = session.UserID;
//            donViLienKet.trangThai = !false;
//            db.DonViLienKets.Add(donViLienKet);

//            db.SaveChanges();
//            SetAlert("Đăng ký thành công", "success");
//            return RedirectToAction("DKDVLK", "DVLK");
//        }
//    }
//    else
//    {
//        SetAlert("Đăng ký thất bại", "error");
//        return RedirectToAction("DKDVLK", "DVLK");
//    }
//    return View(donViLienKet);

//}