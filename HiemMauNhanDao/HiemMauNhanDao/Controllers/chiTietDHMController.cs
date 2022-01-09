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
        public ActionResult Create(string id)
        {
            ViewBag.idDVLK = new SelectList(db.DonViLienKets, "IdDVLK", "TenDonVi");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdChiTietDHM,idDHM,idDVLK,idBenhVien,ngayDK,trangThai")] chiTietDHM chiTietDHM, string id)
        {
            var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];
            string ids = db.chiTietDHMs.Max(x => x.IdChiTietDHM);
            int stt = Convert.ToInt32(ids.Substring(2)) + 1;
            if (ModelState.IsValid == false)
            {
                var tempNVYT = db.NhanVienYTes.Where(x => x.idTTCN == session.UserID).FirstOrDefault();
                chiTietDHM.IdChiTietDHM = stt > 9 ? "CT" + stt : "CT0" + stt;
                chiTietDHM.idBenhVien = tempNVYT.idBenhVien;
                chiTietDHM.idDHM = id;
                chiTietDHM.ngayDK = DateTime.Now;
                chiTietDHM.trangThai = false;

                db.chiTietDHMs.Add(chiTietDHM);
                db.SaveChanges();

                SetAlert("Đăng ký thành công ", "success");
                return RedirectToAction("Index");
            }
            ViewBag.idDVLK = new SelectList(db.DonViLienKets, "IdDVLK", "TenDonVi", chiTietDHM.idDVLK);
            return View(chiTietDHM);
        }

        public ActionResult Create2(string id)
        {
            ViewBag.idBenhVien = new SelectList(db.BenhViens, "IdBenhVien", "TenBenhVien");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2([Bind(Include = "IdChiTietDHM,idDHM,idDVLK,idBenhVien,ngayDK,trangThai")] chiTietDHM chiTietDHM, string id)
        {
            var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];
            string ids = db.chiTietDHMs.Max(x => x.IdChiTietDHM);
            int stt = Convert.ToInt32(ids.Substring(2)) + 1;
            if (ModelState.IsValid == false)
            {
                var tempDVLK = db.DonViLienKets.Where(x => x.idTTCN == session.UserID).FirstOrDefault();
                chiTietDHM.IdChiTietDHM = stt > 9 ? "CT" + stt : "CT0" + stt;
                chiTietDHM.idDHM = id;
                chiTietDHM.idDVLK = tempDVLK.IdDVLK;
                chiTietDHM.ngayDK = DateTime.Now;
                chiTietDHM.trangThai = false;

                db.chiTietDHMs.Add(chiTietDHM);
                db.SaveChanges();

                SetAlert("Đăng ký thành công ", "success");
                return RedirectToAction("Index");
            }   
            ViewBag.idBenhVien = new SelectList(db.BenhViens, "IdBenhVien", "TenBenhVien", chiTietDHM.idBenhVien);
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