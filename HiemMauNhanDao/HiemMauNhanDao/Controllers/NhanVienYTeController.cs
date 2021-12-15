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
    public class NhanVienYTeController : Controller
    {
        NhanVienYTeServices _nhanvien = new NhanVienYTeServices();
        BenhVienServices _BenhVien = new BenhVienServices();
        private DbContextHM db = new DbContextHM();
        // GET: Client/NhanVienYTe
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewNVYT(string searchString1, int page = 1, int pageSize = 10)
        {
            var viewNVYT = new NhanVienYTeServices();
            var model = viewNVYT.ListAllPageTKNV(searchString1, page, pageSize);
            if (!string.IsNullOrEmpty(searchString1))
            {
                ViewBag.SearchString1 = searchString1;
            }
            return View(model);
        }



        public ActionResult CreateNVYT()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateNVYT(TaiKhoan taiKhoanNV)
        {
            if (ModelState.IsValid)
            {
                _nhanvien.addNVYT(taiKhoanNV);
                // SetAlert("Thêm thành công", "success");
                return RedirectToAction("Index");
            }
            return View(taiKhoanNV);
        }

        public ActionResult DangBai()
        {
            return View();
        }

        public ActionResult DangKyDHM()
        {
            return View();
        }


        public ActionResult EditKQHM()
        {
            return View();
        }

        public ActionResult EditBV(string id)
        {
            return View(_BenhVien.GetByIdBenhVien(id));
        }

        [HttpPost]
        public ActionResult EditBV(BenhVien benhVien, HttpPostedFileBase file)
        {
            string duongDan = Server.MapPath("~/FileUpLoad");
            string fileName = Path.GetFileName(file.FileName);
            string fullDuongDan = Path.Combine(duongDan, fileName);

            file.SaveAs(fullDuongDan);

            _BenhVien.EditBV(benhVien, fileName);
            return RedirectToAction("Index");
        }
    }
}