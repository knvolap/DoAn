using HiemMauNhanDao.Areas.Admin.Controllers;
using Models.EF;
using Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiemMauNhanDao.Controllers
{
    public class chiTietDHMController : BaseController2
    {
        // GET: chiTietDHM
       private DbContextHM db = new DbContextHM();
       DKDHMServices _DkDHM = new DKDHMServices();

       public ActionResult Index(DotHienMau dotHienMau, string searchString, int page = 1, int pageSize = 5)
       {
           var hienthiDHM = new DKDHMServices();
           var model = hienthiDHM.ListAllDKDHM2(searchString, page, pageSize);
           ViewBag.BenhVien = new DKDHMServices().ListAllLLeftMenuBV();
           ViewBag.SearchString = searchString;
           ViewBag.listDHM = hienthiDHM.GetDotHienMaus();
           return View(model);
       }

       public ActionResult Create()
       {
           ViewBag.idDVLK = new SelectList(db.DonViLienKets, "IdDVLK", "TenDonVi");
           ViewBag.idDHM = new SelectList(db.DotHienMaus, "IdDHM", "TenDHM");
           ViewBag.idBenhVien = new SelectList(db.BenhViens, "IdBenhVien", "TenBenhVien");
            return View();
       }

       [HttpPost]
       [ValidateAntiForgeryToken]
       public ActionResult Create([Bind(Include = "idChiTietDHM,idDHM,idDVLK,idNVYT,ngayDK,trangThai")] chiTietDHM chiTietDHM)
       {
           var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];

           if (ModelState.IsValid)
           {
               var tempNVYT = db.NhanVienYTes.Where(x => x.idTTCN == session.UserID).FirstOrDefault();
               //var tempDVLK = db.DonViLienKets.Where(x => x.idTTCN == session.UserID).FirstOrDefault();

               chiTietDHM.IdChiTietDHM = "CT" + (db.chiTietDHMs.Count() + 2);
               chiTietDHM.idNVYT = tempNVYT.IdNVYT;
               //chiTietDHM.idDVLK = tempDVLK.IdDVLK;
               chiTietDHM.ngayDK = DateTime.Now;
               chiTietDHM.trangThai = false;
               db.chiTietDHMs.Add(chiTietDHM);
               db.SaveChanges();
               SetAlert("Đăng ký thành công ", "success");
               return RedirectToAction("Index");
           }

           ViewBag.idDVLK = new SelectList(db.DonViLienKets, "IdDVLK", "idTTCN", chiTietDHM.idDVLK);
           ViewBag.idDHM = new SelectList(db.DotHienMaus, "IdDHM", "TenDHM", chiTietDHM.idDHM);
           ViewBag.idNVYT = new SelectList(db.NhanVienYTes, "IdNVYT", "idTTCN", chiTietDHM.idNVYT);
           return View(chiTietDHM);
       }


       protected override void Dispose(bool disposing)
       {
           if (disposing)
           {
               db.Dispose();
           }
           base.Dispose(disposing);
       }


       [ChildActionOnly]
       public PartialViewResult _RightMenu()
       {
           var model = new TCDHMServices().ListAllLLeftMenuBV();
           return PartialView(model);
       }
    
    }
}