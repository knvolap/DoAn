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
    public class ChiTietDHMController : BaseController
    {
        private DbContextHM db = new DbContextHM();
        ChiTietDHMServices _chitiet = new ChiTietDHMServices();

        // GET: Admin/
        public ActionResult Index(string searchString, int page = 1, int pageSize = 15)
        {
            var chiTietDHMs = db.chiTietDHMs.OrderByDescending(c=>c.IdChiTietDHM).Include(c => c.BenhVien).Include(c => c.DonViLienKet).Include(c => c.DotHienMau);           
            return View(chiTietDHMs.ToList( ));
        }
        public ActionResult Index2(string searchString, int page = 1, int pageSize = 15)
        {
            var chiTietDHMs = db.chiTietDHMs.OrderByDescending(c => c.IdChiTietDHM).Include(c => c.BenhVien).Include(c => c.DonViLienKet).Include(c => c.DotHienMau);
            return View(chiTietDHMs.ToList());
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            var model = _chitiet.GetByIdChiTietDHM(id);

            return View(model);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.idBenhVien = new SelectList(db.BenhViens, "IdBenhVien", "TenBenhVien" );
            ViewBag.idDVLK = new SelectList(db.DonViLienKets, "IdDVLK", "TenDonVi" );
            ViewBag.idDHM = new SelectList(db.DotHienMaus, "IdDHM", "TenDHM" );
            return View(_chitiet.GetByIdChiTietDHM(id));
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "IdChiTietDHM,idDHM,idDVLK,idBenhVien,ngayDK,trangThai")] chiTietDHM chiTietDHM)
        {
            if (ModelState.IsValid)
            {
                var ctdhm = _chitiet.GetByIdChiTietDHM(chiTietDHM.IdChiTietDHM);
                ctdhm.IdChiTietDHM = ctdhm.IdChiTietDHM;
                ctdhm.idDHM = ctdhm.idDHM;
                ctdhm.idBenhVien = ctdhm.idBenhVien;
                ctdhm.idDVLK = ctdhm.idDVLK;
                ctdhm.ngayDK = ctdhm.ngayDK;
                ctdhm.trangThai = ctdhm.trangThai;
                db.Entry(chiTietDHM).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Cập nhật thành công", "success");
                return RedirectToAction("Index");

            }
            ViewBag.idBenhVien = new SelectList(db.BenhViens, "IdBenhVien", "TenBenhVien" );
            ViewBag.idDVLK = new SelectList(db.DonViLienKets, "IdDVLK", "TenDonVi" );
            ViewBag.idDHM = new SelectList(db.DotHienMaus, "IdDHM", "TenDHM" );
            return View(chiTietDHM);
        }

        public ActionResult Delete(string id)
        {
            if (ModelState.IsValid)
            {
                _chitiet.Delete(id);
                SetAlert("Xóa thành công", "success");
                return RedirectToAction("Index");
            }
            else
            {
                SetAlert("Xóa thất bại", "error");
                return RedirectToAction("Index");
            }
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