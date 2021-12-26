using HiemMauNhanDao.Areas.Admin.Controllers;
using HiemMauNhanDao.Common;
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
    public class NhanVienYTeController : BaseController2
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
        public ActionResult CreateNVYT(ThongTinCaNhan taiKhoanNV)
        {         
            if (ModelState.IsValid)
            {
                var encryptedMd5Pas = Encryptor.MD5Hash(taiKhoanNV.password);
                taiKhoanNV.password = encryptedMd5Pas;
              
                _nhanvien.addNVYT(taiKhoanNV);
                SetAlert("Thêm thành công", "success");
                return RedirectToAction("Index");
            }
            else
            {
                SetAlert("Thêm thất bại", "error");
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

        public ActionResult LienKetNV()
        {
            var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];
            string id = session.UserID;
            ViewBag.IdUser = id;
            ViewBag.IdBenhVien = new SelectList(db.BenhViens, "IdBenhVien", "TenBenhVien");
            ViewBag.IdChucVu = new SelectList(db.ChucVus, "IdChucVu", "TenChucVu");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LienKetNV([Bind(Include = "IdNVYT,idTTCN,trangThai,idChucVu,idBenhVien,khoa,trinhDo,")]NhanVienYTe nhanVienYTe)
        {
            var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];
            ViewBag.IdBenhVien = new SelectList(db.BenhViens, "IdBenhVien", "TenBenhVien", nhanVienYTe.idBenhVien);
            ViewBag.IdChucVu = new SelectList(db.ChucVus, "IdChucVu", "TenChucVu", nhanVienYTe.idChucVu);
            if (ModelState.IsValid)
            {
                nhanVienYTe.IdNVYT = "NV" + (db.DonViLienKets.Count() + 1).ToString();
                nhanVienYTe.idTTCN = session.UserID;
                nhanVienYTe.trangThai = false;                         
                db.NhanVienYTes.Add(nhanVienYTe);
                db.SaveChanges();
                SetAlert("Đăng ký thành công", "success");
                return RedirectToAction("DKDVLK", "DVLK");
            }
            else
            {
                SetAlert("Đăng ký thất bại", "error");
                return RedirectToAction("DKDVLK", "DVLK");
            }        
            return View(nhanVienYTe);
        }

        public ActionResult EditKQHM()
        {
            return View();
        }
        public ActionResult XemNV()
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

            _BenhVien.EditBV2(benhVien, fileName);
            return RedirectToAction("Index");
        }
    }
}