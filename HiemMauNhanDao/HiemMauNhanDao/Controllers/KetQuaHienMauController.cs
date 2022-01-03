using Models.EF;
using Models.Services;
using Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HiemMauNhanDao.Controllers
{
    public class KetQuaHienMauController : Controller
    {
        private DbContextHM db = new DbContextHM();
        KetQuaHienMauServices _kqhm = new KetQuaHienMauServices();

        public ActionResult Index(string searchString, int page = 1, int pageSize = 100)
        {
            var dsdk = new KetQuaHienMauServices();
            var model = dsdk.GetListKQHM(searchString, page, pageSize);
            ViewBag.SearchStringDK = searchString;          
            return View(model);
        }

      

        public ActionResult Create()
        {
            ViewBag.idPDKHM = new SelectList(db.PhieuDKHMs, "idPDKHM", "idDTCHM");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdKQHM,idPDKHM,nhomMau,nguoiKham,nguoiXN,nguoiLayMau,canNang,machMau,tinhTrangLS,huyetAp,luongMauHien,hienMau,noiDung,HST,HBV,MSD,phanUng,thoiGianLayMau,ghiChu,trangThai")] KetQuaHienMau ketQuaHienMau)
        {
            if (ModelState.IsValid)
            {
                db.KetQuaHienMaus.Add(ketQuaHienMau);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idPDKHM = new SelectList(db.PhieuDKHMs, "idPDKHM", "idDTCHM", ketQuaHienMau.idPDKHM);
            return View(ketQuaHienMau);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdKQHM,idPDKHM,nhomMau,nguoiKham,nguoiXN,nguoiLayMau,canNang,machMau,tinhTrangLS,huyetAp,luongMauHien,hienMau,noiDung,HST,HBV,MSD,phanUng,thoiGianLayMau,ghiChu,trangThai")] KetQuaHienMau ketQuaHienMau)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ketQuaHienMau).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idPDKHM = new SelectList(db.PhieuDKHMs, "idPDKHM", "idDTCHM", ketQuaHienMau.idPDKHM);
            return View(ketQuaHienMau);
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KetQuaHienMau ketQuaHienMau = db.KetQuaHienMaus.Find(id);
            if (ketQuaHienMau == null)
            {
                return HttpNotFound();
            }
            return View(ketQuaHienMau);
        }

        [HttpGet]
        public ActionResult TTDK(string id)
        {
            var model = _kqhm.GetByIdTTDK(id);

            return View(model);
        }


        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KetQuaHienMau ketQuaHienMau = db.KetQuaHienMaus.Find(id);
            if (ketQuaHienMau == null)
            {
                return HttpNotFound();
            }
            return View(ketQuaHienMau);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            KetQuaHienMau ketQuaHienMau = db.KetQuaHienMaus.Find(id);
            db.KetQuaHienMaus.Remove(ketQuaHienMau);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //public ActionResult Index()
        //{
        //    var ketQuaHienMaus = db.KetQuaHienMaus.Include(k => k.PhieuDKHM);
        //    return View(ketQuaHienMaus.ToList());
        //}
    }
}