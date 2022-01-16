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
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var benhVien = new BenhVienServices();
            var model = benhVien.ListAllBenhVien(searchString, page, pageSize);
            ViewBag.SearchStringBV = searchString;

            if (ModelState.IsValid)
            {
                string[] filepaths = Directory.GetFiles(Server.MapPath("~/FileUpLoad/benhvien/"));
                List<BenhVien> filesBV = new List<BenhVien>();
                foreach (string strfile in filepaths)
                {
                    filesBV.Add(new BenhVien { minhChung = Path.GetFileName(strfile) });

                }            
            }             
            return View(model);
        }

        [HttpGet]
        public ActionResult CreateBV()
        {          
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBV(BenhVien benhVien, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (_benhVien.isExistEmailBV(benhVien.Email))
                {
                    SetAlert("Email đã được đăng ký ", "error");
                    return RedirectToAction("CreateBV", "BenhVien");
                }
                else if (_benhVien.isExistSDTlBV(benhVien.soDTBV))
                {
                    SetAlert("Số điện thoại đã được đăng ký ", "error");
                    return RedirectToAction("CreateBV", "BenhVien");
                }
                else if (_benhVien.isExistMinhChungBV(benhVien.minhChung))
                {
                    SetAlert("Minh chứng đã tồn tại", "error");
                    return RedirectToAction("CreateBV", "BenhVien");
                }
                else
                {
                    string duongDan = Server.MapPath("~/FileUpLoad/benhvien/");
                    string fileName = Path.GetFileName(file.FileName);
                    string fullDuongDan = Path.Combine(duongDan, fileName);
                    file.SaveAs(fullDuongDan);

                    _benhVien.AddBV(benhVien, fileName);
                    db.SaveChanges();
                    SetAlert("Thêm thành công", "success");
                    return RedirectToAction("Index");
                }
            }
            else
            {
                SetAlert("Thêm thất bại!! Vui lòng kiểm tra lại thông tin nhập", "error");
                return RedirectToAction("CreateBV","BenhVien");
            }
            //return View(benhVien);
        }


        public ActionResult EditBV(string id)
        {
            return View(_benhVien.GetByIdBenhVien1(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBV(BenhVien benhVien, HttpPostedFileBase file)
        {            
            benhVien = _benhVien.GetByIdBenhVien1(benhVien.IdBenhVien);
            if (ModelState.IsValid)
            {
                if (_benhVien.isExistEmailBV(benhVien.Email))
                {
                    SetAlert("Email đã được đăng ký ", "error");
                    return RedirectToAction("CreateBV", "BenhVien");
                }
                else if (_benhVien.isExistSDTlBV(benhVien.soDTBV))
                {
                    SetAlert("Số điện thoại đã được đăng ký ", "error");
                    return RedirectToAction("CreateBV", "BenhVien");
                }
                else if (_benhVien.isExistMinhChungBV(benhVien.minhChung))
                {
                    SetAlert("Minh chứng đã tồn tại", "error");
                    return RedirectToAction("CreateBV", "BenhVien");
                }
                else
                {
                    string duongDan = Server.MapPath("~/FileUpLoad/benhvien/");
                    string fileName = Path.GetFileName(file.FileName);
                    string fullDuongDan = Path.Combine(duongDan, fileName);
                    file.SaveAs(fullDuongDan);

                    _benhVien.EditBV(benhVien, fileName);
                    db.SaveChanges();
                    SetAlert("Thêm thành công", "success");
                    return RedirectToAction("Index");
                }
            }
            else
            {
                SetAlert("Thêm thất bại!! Vui lòng kiểm tra lại thông tin nhập", "error");
                return RedirectToAction("CreateBV", "BenhVien");
            }
            
        }

        private string GetFileTypeByExtension(string fileExtension)
        {
            switch (fileExtension.ToLower())
            {
                case ".docx":
                case ".doc":
                    return "Microsoft Word Document";
                case ".pdf":
                    return "PDF";
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
        public FileResult Download(string fileName)
        {
            string fullPath = Server.MapPath("~/FileUpLoad/benhvien/")+ fileName;
            byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);
            return File(fileBytes, "applocation.octet-stream", fileName);
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
            var model = _benhVien.GetByIdBenhVien1(id);          
            return View(model);
        }
       

      
    }
}

