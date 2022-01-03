using HiemMauNhanDao.Areas.Admin.Controllers;
using Models.EF;
using Models.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HiemMauNhanDao.Controllers
{
    public class DotToChucHMController : BaseController
    {
        private DbContextHM db = new DbContextHM();
        DotToChucHMServices _DTCHM = new DotToChucHMServices();
        //public ActionResult Index()
        //{
        //    var dotToChucHMs = db.DotToChucHMs.Include(d => d.chiTietDHM);
        //    return View(dotToChucHMs.ToList());
        //}
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dotToChucHMs = new DotToChucHMServices();
            var model = dotToChucHMs.ListAllDTCHM(searchString, page, pageSize);
            ViewBag.SearchStringDTCHM = searchString;
            return View(model);
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DotToChucHM dotToChucHM = db.DotToChucHMs.Find(id);
            if (dotToChucHM == null)
            {
                return HttpNotFound();
            }
            return View(dotToChucHM);
        }

        public ActionResult Create()
        {
            ViewBag.IdChiTietDHM = new SelectList(db.chiTietDHMs, "IdChiTietDHM", "IdChiTietDHM");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDTCHM,IdChiTietDHM,tenDotHienMau,noiDung,doiTuongThamGia,diaChiToChuc,soLuong,ngayBatDauDK,ngayKetThucDK,ngayToChuc,trangThai")] DotToChucHM dotToChucHM)
        {
            string id = db.DotToChucHMs.Max(x => x.IdDTCHM);
            int stt = Convert.ToInt32(id.Substring(4)) + 1;

            if (ModelState.IsValid)
            {
                dotToChucHM.IdDTCHM = stt > 9 ? "TCHM" + stt : "TCHM0" + stt;
                dotToChucHM.trangThai = "Chờ Duyệt";
                db.DotToChucHMs.Add(dotToChucHM);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.IdChiTietDHM = new SelectList(db.chiTietDHMs, "IdChiTietDHM", "idChiTietDHM", dotToChucHM.idChiTietDHM);
            return View(dotToChucHM);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DotToChucHM dotToChucHM = db.DotToChucHMs.Find(id);
            if (dotToChucHM == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdChiTietDHM = new SelectList(db.chiTietDHMs, "IdChiTietDHM", "idChiTietDHM", dotToChucHM.idChiTietDHM);
            return View(dotToChucHM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDTCHM,IdChiTietDHM,tenDotHienMau,noiDung,doiTuongThamGia,diaChiToChuc,soLuong,ngayBatDauDK,ngayKetThucDK,ngayToChuc,trangThai")] DotToChucHM dotToChucHM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dotToChucHM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdChiTietDHM = new SelectList(db.chiTietDHMs, "IdChiTietDHM", "idDHM", dotToChucHM.idChiTietDHM);
            return View(dotToChucHM);
        }

        
        public ActionResult Delete(string id)
        {
            if (ModelState.IsValid)
            {
                _DTCHM.Delete(id);
                SetAlert("Xóa thành công", "success");
                return RedirectToAction("Index");
            }
            else
            {
                SetAlert("Xóa thất bại", "error");
                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}