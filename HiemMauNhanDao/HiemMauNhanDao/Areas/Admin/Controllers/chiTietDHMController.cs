using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models.Services;

namespace HiemMauNhanDao.Areas.Admin.Controllers
{
    public class ChiTietDHMController : BaseController2
    {
        private DbContextHM db = new DbContextHM();

        public ActionResult Index()
        {
            var chiTietDHMs = db.chiTietDHMs.Include(c => c.DonViLienKet).Include(c => c.DotHienMau).Include(c => c.BenhVien);
            return View(chiTietDHMs.ToList());
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chiTietDHM chiTietDHM = db.chiTietDHMs.Find(id);
            if (chiTietDHM == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDHM);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chiTietDHM chiTietDHM = db.chiTietDHMs.Find(id);
            if (chiTietDHM == null)
            {
                return HttpNotFound();
            }
            ViewBag.idBenhVien = new SelectList(db.BenhViens, "IdBenhVien", "TenBenhVien", chiTietDHM.idBenhVien);
            ViewBag.idDVLK = new SelectList(db.DonViLienKets, "IdDVLK", "idTTCN", chiTietDHM.idDVLK);
            ViewBag.idDHM = new SelectList(db.DotHienMaus, "IdDHM", "TenDHM", chiTietDHM.idDHM);
            return View(chiTietDHM);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "IdChiTietDHM,idDHM,idDVLK,idBenhVien,ngayDK,trangThai")] chiTietDHM chiTietDHM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietDHM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idBenhVien = new SelectList(db.BenhViens, "IdBenhVien", "TenBenhVien", chiTietDHM.idBenhVien);
            ViewBag.idDVLK = new SelectList(db.DonViLienKets, "IdDVLK", "idTTCN", chiTietDHM.idDVLK);
            ViewBag.idDHM = new SelectList(db.DotHienMaus, "IdDHM", "TenDHM", chiTietDHM.idDHM);
            return View(chiTietDHM);
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chiTietDHM chiTietDHM = db.chiTietDHMs.Find(id);
            if (chiTietDHM == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDHM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            chiTietDHM chiTietDHM = db.chiTietDHMs.Find(id);
            db.chiTietDHMs.Remove(chiTietDHM);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //xử lý duyệt = json
        [HttpPost]
        public JsonResult ChangeStatus(string id)
        {
            var result = new DKDHMServices().ChangeStatus(id);
            return Json(new
            {
                tt = result
            });
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