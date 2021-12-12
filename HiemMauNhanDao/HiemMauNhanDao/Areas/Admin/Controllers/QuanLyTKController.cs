﻿using HiemMauNhanDao.Common;
using Models.EF;
using Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiemMauNhanDao.Areas.Admin.Controllers
{
    public class QuanLyTKController : Controller
    {
        private DbContextHM db = new DbContextHM();
        QuanLyTKServices _taiKhoan = new QuanLyTKServices();
        // GET: Admin/QuanLyTK
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var taikhoan = new QuanLyTKServices();
            var model = taikhoan.ListAllTaiKhoan(searchString, page, pageSize);
            ViewBag.SearchStringTK = searchString;
            return View(model);
        }

        public ActionResult CreateTK()
        {
            ViewBag.IdQuyen = new SelectList(db.Quyens, "IdQuyen", "tenQuyen");
            return View();
        }

        [HttpPost]
        public ActionResult CreateTK(TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
              
                _taiKhoan.themTK(taiKhoan);
                var encryptedMd5Pas = Encryptor.MD5Hash(taiKhoan.password);
                taiKhoan.password = encryptedMd5Pas;
                //SetAlert("Thêm thành công", "success");
                return RedirectToAction("Index");
            }
            else
            {
                //SetAlert("Thêm thất bại", "error");
                return RedirectToAction("Index");
            }
            return View(taiKhoan);
        }

        public ActionResult EditTK(string id)
        {
            ViewBag.IdQuyen = new SelectList(db.Quyens, "IdQuyen", "tenQuyen");
            return View(_taiKhoan.GetByIdTK(id));
        }

        [HttpPost]
        public ActionResult EditTK(TaiKhoan taiKhoan )
        {
            _taiKhoan.SuaTk(taiKhoan);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            _taiKhoan.xoaTK(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(string id)
        {
            var model = _taiKhoan.GetByIdTK(id);
            return View(model);
        }
    }
}