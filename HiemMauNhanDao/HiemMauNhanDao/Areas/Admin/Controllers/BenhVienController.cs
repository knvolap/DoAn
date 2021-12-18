using Models.EF;
using Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace HiemMauNhanDao.Areas.Admin.Controllers
{
    public class BenhVienController : BaseController
    {
        BenhVienServices _benhVien = new BenhVienServices();
        // GET: Admin/BenhVien
        public ActionResult Index(string searchString, int page=1, int pageSize=5)
        {
            var benhVien = new BenhVienServices();
            var model = benhVien.ListAllBenhVien(searchString, page, pageSize);
            ViewBag.SearchStringBV = searchString;
            return View(model);
        }

        public ActionResult CreateBV()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBV(BenhVien benhVien )
        {           
            if (ModelState.IsValid)
            {
                _benhVien.AddBV(benhVien);
                SetAlert("Thêm thành công", "success");
                return RedirectToAction("Index");
            }
            else
            {
                SetAlert("Thêm thất bại", "error");
                return RedirectToAction("Index");
            }
            return View(benhVien);
        }

        public ActionResult EditBV(string id)
        {
            return View(_benhVien.GetByIdBenhVien(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBV(BenhVien benhVien, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string duongDan = Server.MapPath("~/FileUpLoad");
                string fileName = Path.GetFileName(file.FileName);
                string fullDuongDan = Path.Combine(duongDan, fileName);

                file.SaveAs(fullDuongDan);

                _benhVien.EditBV(benhVien, fileName);
              
                SetAlert("Cập nhật thành công", "success");
                return RedirectToAction("Index");
            }
            else
            {
                SetAlert("Cập nhật thất bại", "error");
                return RedirectToAction("Index");
            }
            return View(benhVien);
        }

        public ActionResult Delete(string id)
        {
            if (ModelState.IsValid)
            {
                _benhVien.Delete(id);
                SetAlert("Xóa thành công", "success");
                return RedirectToAction("Index");
            }
            else
            {
                SetAlert("Xóa thất bại", "error");
                return RedirectToAction("Index");
            }
        }

        public ActionResult Details(string id)
        {
            var model = _benhVien.GetByIdBenhVien(id);
            return View(model);
        }
    }
}