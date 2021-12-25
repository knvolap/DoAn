using Models.EF;
using Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace HiemMauNhanDao.Areas.Admin.Controllers
{
    public class BenhVienController : BaseController
    {
        BenhVienServices _benhVien = new BenhVienServices();
        DbContextHM db = new DbContextHM();
     

        // GET: Admin/BenhVien
        public ActionResult Index(string searchString, int page=1, int pageSize=5)
        {
            var benhVien = new BenhVienServices();
            var model = benhVien.ListAllBenhVien(searchString, page, pageSize);
            string[] filepaths = Directory.GetFiles(Server.MapPath("~/FileUpLoad/benhvien/"));
            List<BenhVien> files = new List<BenhVien>();
            foreach(string filePath in filepaths)
            {
                files.Add(new BenhVien { minhChung = Path.GetFileName(filePath) });
            }
            ViewBag.SearchStringBV = searchString;
            return View(model);
        }

        public ActionResult CreateBV()
        {
            List<BenhVien> benhVien = new List<BenhVien>();
            foreach (string strfile in Directory.GetFiles(Server.MapPath("~/FileUpLoad/benhvien")))
            {
                FileInfo fi = new FileInfo(strfile);
                BenhVien bv = new BenhVien();
                bv.minhChung = GetFileTypeByExtension(fi.Extension);
                benhVien.Add(bv);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBV(BenhVien benhVien1 )
        {
            foreach (var file in benhVien1.files)
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(Server.MapPath("~/FileUpLoad/benhvien"), fileName);
                    file.SaveAs(filePath);
                }
            }
            if (ModelState.IsValid)
            {              
                _benhVien.AddBV(benhVien1);
                SetAlert("Thêm thành công", "success");
                return RedirectToAction("Index");
            }
            else
            {
                SetAlert("Thêm thất bại", "error");
                return RedirectToAction("Index");
            }
            return View(benhVien1);
        }
        public FileResult Download(string fileName)
        {
            string fullPath = Server.MapPath("~/FileUpLoad/benhvien/") + fileName;
            byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);
            return File(fileBytes, "application/octet-stream", fileName);
        }
        private string GetFileTypeByExtension(string fileExtension)
        {
            switch (fileExtension.ToLower())
            {
                case ".docx":
                case ".doc":
                    return "Microsoft Word Document";
                case ".xlsx":
                case ".xls":
                    return "Microsoft Excel Document";
                case ".txt":
                    return "Text Document";
                case ".jpg":
                case ".png":
                    return "Image";
                default:
                    return "Unknown";
            }
        }
       


        public ActionResult EditBV(string id)
        {
            return View(_benhVien.GetByIdBenhVien(id));
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
       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBV(BenhVien benhVien, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string duongDan = Server.MapPath("~/FileUpLoad/benhvien");
                string fileName = Path.GetFileName(file.FileName);
                string fullDuongDan = Path.Combine(duongDan, fileName);

                file.SaveAs(fullDuongDan);

                _benhVien.EditBV2(benhVien, fileName);

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
    }
}

//[HttpPost]
//[ValidateAntiForgeryToken]
//public ActionResult CreateBV(BenhVien benhVien)
//{
//    if (ModelState.IsValid)
//    {
//        _benhVien.AddBV(benhVien);
//        SetAlert("Thêm thành công", "success");
//        return RedirectToAction("Index");
//    }
//    else
//    {
//        SetAlert("Thêm thất bại", "error");
//        return RedirectToAction("Index");
//    }
//    return View(benhVien);
//}

//[HttpPost]
//[ValidateAntiForgeryToken]
//public ActionResult EditBV(BenhVien benhVien)
//{
//    if (ModelState.IsValid)
//    {
//        _benhVien.EditBV(benhVien);             
//        SetAlert("Cập nhật thành công", "success");
//        return RedirectToAction("Index");
//    }
//    else
//    {
//        SetAlert("Cập nhật thất bại", "error");
//        return RedirectToAction("Index");
//    }
//    return View(benhVien);
//}
