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
    public class BenhVienController : Controller
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
        public ActionResult CreateBV(BenhVien benhVien, HttpPostedFileBase file)
        {
            string duongDan1 = Server.MapPath("~/FileUpLoad");
            string fileName1 = Path.GetFileName(file.FileName);
            string fullDuongDan = Path.Combine(duongDan1, fileName1);
            file.SaveAs(fullDuongDan);

            if (ModelState.IsValid)
            {
                _benhVien.AddBV(benhVien,fileName1);
                //SetAlert("Thêm thành công", "success");
                return RedirectToAction("Index");
            }
            return View(benhVien);
        }

        public ActionResult EditBV(string id)
        {
            return View(_benhVien.GetByIdBenhVien(id));
        }

        [HttpPost]
        public ActionResult EditBV(BenhVien benhVien, HttpPostedFileBase file)
        {
            string duongDan = Server.MapPath("~/FileUpLoad");
            string fileName = Path.GetFileName(file.FileName);
            string fullDuongDan = Path.Combine(duongDan, fileName);

            file.SaveAs(fullDuongDan);

            _benhVien.EditBV(benhVien, fileName);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            _benhVien.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(string id)
        {
            var model = _benhVien.GetByIdBenhVien(id);
            return View(model);
        }
    }
}