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
                //return View(filesBV);
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
        public ActionResult CreateBV(BenhVien benhVien1, HttpPostedFileBase file)
        {      
            if (ModelState.IsValid & file != null & file.ContentLength > 0)
            {
               string duongDan = Server.MapPath("~/FileUpLoad/benvien/");
               string fileName = Path.GetFileName(file.FileName);
               string fullDuongDan = Path.Combine(duongDan, fileName);
               file.SaveAs(fullDuongDan);
                
               _benhVien.AddBV(benhVien1, fileName);
               db.SaveChanges();
               SetAlert("Thêm thành công", "success");
               return RedirectToAction("Index");
            }         
            return View(benhVien1);
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
//public ActionResult CreateBV(BenhVien benhVien1, HttpPostedFileBase upfile)
//{
//    if (ModelState.IsValid)
//    {
//        if (upfile != null & upfile.ContentLength > 0)
//        {
//            //string fileName = Path.Combine(Server.MapPath("~/FileUpLoad/benhvien/"), Path.GetFileName(file.FileName));
//            //upfile.SaveAs(fileName);

//            string path = Server.MapPath("~/FileUpLoad/benhvien/");
//            if (!Directory.Exists(path))
//            {
//                Directory.CreateDirectory(path);
//            }
//            upfile.SaveAs(path + Path.GetFileName(upfile.FileName));

//        }
//        benhVien1.minhChung = upfile.FileName;
//        _benhVien.AddBV(benhVien1);
//        SetAlert("Thêm thành công", "success");
//        return RedirectToAction("Index");
//    }
//    return View(benhVien1);
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



//[HttpPost]
//[ValidateAntiForgeryToken]
//public ActionResult CreateBV(BenhVien benhVien1, HttpPostedFileBase file)
//{

//    if (ModelState.IsValid)
//    {     // kiểm tra nếu độ dài tập tin là không hoặc không có tệp đính kèm
//        if (Request.Files.Count != 1 || Request.Files[0].ContentLength == 0)
//        {
//            SetAlert("Không được để trống", "error");
//            return View(benhVien1);
//        }
//        // kiểm tra nếu kích thước tập tin là trên 4 MB
//        if (Request.Files[0].ContentLength > 1024 * 1024 * 4)
//        {
//            SetAlert("Kích thước File quá lớn. Phải < 4 MB", "error");
//            return View(benhVien1);
//        }
//        // nếu kích thước tập tin nhỏ hơn 100 byte
//        if (Request.Files[0].ContentLength < 200)
//        {
//            SetAlert("Kích thước File quá nhỏ. Phải > 200byte", "error");
//            return View(benhVien1);
//        }
//        // kiểm tra định dạng tập tin
//        string extension = Path.GetExtension(Request.Files[0].FileName).ToLower();

//        if (extension != ".pdf" && extension != ".doc" && extension != ".docx" && extension != ".rtf" && extension != ".txt")
//        {
//            SetAlert("Chỉ hỗ trợ các định dạng: pdf, doc, docx, rtf, txt", "error");
//            //ModelState.AddModelError("Upload thất bại", "Chỉ hỗ trợ các định dạng: pdf, doc, docx, rtf, txt");
//            return View(benhVien1);
//        }

//        // lấy tên tập tin
//        var fileName = Path.GetFileName(Request.Files[0].FileName);

//        // lưu trữ các tập tin trong folder ~/App_Data/uploads
//        var path = Path.Combine(Server.MapPath("~/FileUpLoad/benhvien"), fileName);

//        try
//        {
//            if (System.IO.File.Exists(path))
//                System.IO.File.Delete(path);

//            Request.Files[0].SaveAs(path);
//        }
//        catch (Exception)
//        {
//            SetAlert("Không thể lưu file", "error");
//        }

//        _benhVien.AddBV(benhVien1);
//        SetAlert("Thêm thành công", "success");
//        return RedirectToAction("Index");
//    }
//    //else
//    //{
//    //    SetAlert("Thêm thất bại", "error");
//    //    return RedirectToAction("Index");
//    //}
//    return View(benhVien1);
//}