using Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HiemMauNhanDao.Controllers
{
    public class PhieuDKHMController : Controller
    {
        private DbContextHM db = new DbContextHM();

        public ActionResult Index()
        {
            var phieuDKHMs = db.PhieuDKHMs.Include(p => p.DotToChucHM).Include(p => p.ThongTinCaNhan);
            return View(phieuDKHMs.ToList());
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuDKHM phieuDKHM = db.PhieuDKHMs.Find(id);
            if (phieuDKHM == null)
            {
                return HttpNotFound();
            }
            return View(phieuDKHM);
        }

        public ActionResult Create(string id)
        {
            ViewBag.idDTCHM = new SelectList(db.DotToChucHMs, "IdDTCHM", "IdDTCHM");
            ViewBag.idTTCN = new SelectList(db.ThongTinCaNhans, "IdTTCN", "IdTTCN");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idPDKHM,idDTCHM,idTTCN,benhKhac,trangThai,tgDuKien,sutCan,noiHach,chamCu,xamMinh,duocTruyenMau,suDungMatuy,NguyCoHIV,QHTD,tiemVacXin,dungTKS,biSot,dTTT,dangMangThai,xacNhan")] PhieuDKHM phieuDKHM, string id)
        {
            string idpdk = db.PhieuDKHMs.Max(x => x.idPDKHM);
            int stt = Convert.ToInt32(idpdk.Substring(5)) + 1;

            if (ModelState.IsValid)
            {
                phieuDKHM.idPDKHM = stt > 9 ? "PDKHM" + stt : "PDKHM0" + stt;
                phieuDKHM.idTTCN = "TT06";

                db.PhieuDKHMs.Add(phieuDKHM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idDTCHM = new SelectList(db.DotToChucHMs, "IdDTCHM", "IdDTCHM", phieuDKHM.idDTCHM);
            ViewBag.idTTCN = new SelectList(db.ThongTinCaNhans, "IdTTCN", "IdTTCN", phieuDKHM.idTTCN);
            return View(phieuDKHM);
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