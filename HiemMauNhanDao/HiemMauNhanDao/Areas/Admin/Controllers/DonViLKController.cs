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
    public class DonViLKController : BaseController2
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
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            var model = _donvi.GetByIdTTCN1(id);           
            ViewBag.IdQuyen = new SelectList(db.Quyens, "IdQuyen", "tenQuyen");
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "IdTTCN,idQuyen,CCCD,hoTen,IdDVLK,TenDonVi,diaChi,Email,soDT,minhChung,trangThai")] NhanVienvaDVLKView thongTinCaNhan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thongTinCaNhan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            return View(thongTinCaNhan);
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
    }
}