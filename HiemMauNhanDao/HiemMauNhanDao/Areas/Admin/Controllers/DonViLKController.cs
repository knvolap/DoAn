using Models.EF;
using Models.Services;
using Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HiemMauNhanDao.Areas.Admin.Controllers
{
    public class DonViLKController : BaseController
    {
        DonViLienKetServices _donvi = new DonViLienKetServices();
        DbContextHM db = new DbContextHM();
        // GET: Admin/DonViLK
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var donvi = new DonViLienKetServices();
            var model = donvi.ListAllDVLK(searchString, page, pageSize);
            ViewBag.SearchStringdv = searchString;

            if (ModelState.IsValid)
            {
                string[] filepaths = Directory.GetFiles(Server.MapPath("~/FileUpLoad/dvlk/"));
                List<DonViLienKet> filesBV = new List<DonViLienKet>();
                foreach (string strfile in filepaths)
                {
                    filesBV.Add(new DonViLienKet { minhChung = Path.GetFileName(strfile) });

                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {            
            var model = _donvi.GetByIdTTCN(id);           
            ViewBag.IdQuyen = new SelectList(db.Quyens, "IdQuyen", "tenQuyen");
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "IdTTCN,hoTen,gioiTinh,soDT,soLanHM,ngheNghiep,nhomMau,trinhDo,coQuanTH,diaChi,userName,ngaySinh,CCCD,idQuyen,trangThai,password")] ThongTinCaNhan model)
        {
            if (ModelState.IsValid)
            {
                var ttcn = _donvi.GetByIdTTCN(model.IdTTCN);
                ttcn.IdTTCN = ttcn.IdTTCN;
                ttcn.CCCD = ttcn.CCCD;
                ttcn.userName = ttcn.userName;
                ttcn.password = ttcn.password;
                ttcn.ngaySinh = ttcn.ngaySinh;
                ttcn.trangThai = ttcn.trangThai;
                ttcn.coQuanTH = ttcn.coQuanTH;
                ttcn.ngheNghiep = ttcn.ngheNghiep;
                ttcn.trinhDo = ttcn.trinhDo;
                ttcn.soLanHM = ttcn.soLanHM;
                ttcn.soDT = ttcn.soDT;
                ttcn.CCCD = ttcn.CCCD;
                ttcn.gioiTinh = ttcn.gioiTinh;
                ttcn.trangThai = ttcn.trangThai;
                ttcn.nhomMau = ttcn.nhomMau;

                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();

                //_donvi.UpdateDVLK(taiKhoan);
                SetAlert("Cấp quyền thành công", "success");
                return RedirectToAction("Index");
            }
            else
            {
                SetAlert("Cấp quyền thất bại", "error");
                return RedirectToAction("Index");
            }
        }


        public ActionResult Create( )
        {          
            return View( );
        }
        [HttpPost]
        public ActionResult Create(DonViLienKet donViLienKet, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string duongDan = Server.MapPath("~/FileUpLoad/benhvien/");
                string fileName = Path.GetFileName(file.FileName);
                string fullDuongDan = Path.Combine(duongDan, fileName);
                file.SaveAs(fullDuongDan);
                _donvi.AddDVLK(donViLienKet, fileName);
                SetAlert("Thêm thành công", "success");
                return RedirectToAction("Index");
            }
            else
            {
                SetAlert("Thêm thất bại", "error");
                return RedirectToAction("Index");
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
            string fullPath = Server.MapPath("~/FileUpLoad/dvlk/") + fileName;
            byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);
            return File(fileBytes, "applocation.octet-stream", fileName);
        }

        public ActionResult Delete(string id)
        {
            if (ModelState.IsValid)
            {
                _donvi.Delete(id);
                SetAlert("Xóa thành công", "success");
                return RedirectToAction("Index");
            }
            else
            {
                SetAlert("Xóa thất bại", "error");
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            var model = _donvi.GetByIdDVLK2(id);

            return View(model);
        }

        //xử lý duyệt = json
        [HttpPost]
        public JsonResult ChangeStatus2(string id)
        {
            var result = new DonViLienKetServices().ChangeStatus2(id);
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

//[HttpPost]
//public ActionResult Edit([Bind(Include = "IdTTCN,hoTen,gioiTinh,soDT,soLanHM,ngheNghiep,nhomMau,trinhDo,coQuanTH,diaChi,userName,ngaySinh,CCCD,idQuyen,trangThai,password")] ThongTinCaNhan thongTinCaNhan)
//{
//    if (ModelState.IsValid)
//    {
//        var ttcn = _donvi.GetByIdTTCN(thongTinCaNhan.IdTTCN);
//        ttcn.nhomMau = ttcn.nhomMau;
//        ttcn.trinhDo = ttcn.trinhDo;
//        ttcn.trangThai = ttcn.trangThai;
//        ttcn.userName = ttcn.userName;
//        ttcn.password = ttcn.password;
//        ttcn.diaChi = ttcn.diaChi;
//        ttcn.coQuanTH = ttcn.coQuanTH;
//        ttcn.CCCD = ttcn.CCCD;
//        ttcn.soDT = ttcn.soDT;
//        ttcn.gioiTinh = ttcn.gioiTinh;
//        ttcn.ngaySinh = ttcn.ngaySinh;
//        ttcn.ngheNghiep = ttcn.ngheNghiep;
//        ttcn.hoTen = ttcn.hoTen;
//        ttcn.soLanHM = ttcn.soLanHM;

//        db.Entry(thongTinCaNhan).State = EntityState.Modified;
//        db.SaveChanges();
//        SetAlert("cập nhật thành công", "success");
//        return RedirectToAction("Index");
//    }
//    else
//    {
//        SetAlert("cập nhật thất bại", "error");
//        return RedirectToAction("Index");
//    }