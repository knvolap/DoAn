using HiemMauNhanDao.Areas.Admin.Controllers;
using HiemMauNhanDao.Common;
using Models.EF;
using Models.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiemMauNhanDao.Controllers
{
    public class DVLKController : BaseController2
    {
        DonViLienKetServices _dvlk = new DonViLienKetServices();
        private DbContextHM db = new DbContextHM();
        // GET: DVLK
        public ActionResult Index()
        {
            return View();
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
 
                if (_dvlk.isExistIDTK(donViLienKet.idTTCN))
                {
                    SetAlert("Bạn đã đăng ký rồi", "error");
                    return RedirectToAction("DKDVLK", "DVLK");
                }
                else if (_dvlk.isExistSDT(donViLienKet.soDT))
                {
                    SetAlert("Số điện thoại đã tồn tại", "error");
                    return RedirectToAction("DKDVLK", "DVLK");
                }
                else if (_dvlk.isExistEmail(donViLienKet.Email))
                {
                    SetAlert("Email đã tồn tại", "error");
                    return RedirectToAction("DKDVLK", "DVLK");
                }
                else
                {
                    donViLienKet.idTTCN = session.UserID;
                    string duongDan = Server.MapPath("~/FileUpLoad/dvlk/");
                    string fileName = Path.GetFileName(file.FileName);
                    string fullDuongDan = Path.Combine(duongDan, fileName);
                    file.SaveAs(fullDuongDan);

                    _dvlk.AddDVLK(donViLienKet, fileName);
                    db.SaveChanges();
                    SetAlert("Đăng ký thành công", "success");
                    return RedirectToAction("DKDVLK", "DVLK");
                }
          
                SetAlert("Đăng ký thất bại!! Vui lòng kiểm tra lại thông tin nhập", "error");
                return RedirectToAction("DKDVLK", "DVLK");
     
          
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

        public ActionResult CapNhatTTDV()
        {
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