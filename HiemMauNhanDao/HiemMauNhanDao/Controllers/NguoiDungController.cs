﻿using HiemMauNhanDao.Areas.Admin.Controllers;
using HiemMauNhanDao.Common;
using Models.EF;
using Models.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiemMauNhanDao.Controllers
{
    public class NguoiDungController : BaseController2
    {
        NguoiDungServices _NguoiDung = new NguoiDungServices();
        public UserLogin userSession = new UserLogin();
        private DbContextHM db = new DbContextHM();


        // GET: NguoiDung
        public ActionResult Index()
        {
            var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];
            string id = session.UserID;
            ThongTinCaNhan thongTinCaNhan = db.ThongTinCaNhans.Find(id);

            if (thongTinCaNhan.hoTen == "" || thongTinCaNhan.soDT == "" || thongTinCaNhan.userName == "" || thongTinCaNhan.CCCD == "" ||
                thongTinCaNhan.gioiTinh == null || thongTinCaNhan.ngaySinh == null || thongTinCaNhan.soLanHM == null || thongTinCaNhan.nhomMau == "" ||
                thongTinCaNhan.trinhDo == "" || thongTinCaNhan.ngheNghiep == "" || thongTinCaNhan.coQuanTH == "" || thongTinCaNhan.diaChi == ""
                )
            {
                SetAlert("Vui lòng cập nhập đủ thông tin cá nhân ", "error");
            }
            else if(thongTinCaNhan.hoTen != null || thongTinCaNhan.soDT != null || thongTinCaNhan.userName != null || thongTinCaNhan.CCCD != null ||
                thongTinCaNhan.gioiTinh != null || thongTinCaNhan.ngaySinh != null || thongTinCaNhan.soLanHM != null || thongTinCaNhan.nhomMau != null ||
                thongTinCaNhan.trinhDo != null || thongTinCaNhan.ngheNghiep != null || thongTinCaNhan.coQuanTH != null || thongTinCaNhan.diaChi != null
                )
            {
                SetAlert("Đã cập nhật đầy đủ thông tin cá nhân ", "success");
            }    
            return View(thongTinCaNhan);
        }


        [HttpGet]
        public ActionResult CapnhatTTCN(string id)
        {
            ThongTinCaNhan thongTinCaNhan = db.ThongTinCaNhans.Find(id);
            return View(thongTinCaNhan);
        }



        [HttpPost]
        public ActionResult CapnhatTTCN(ThongTinCaNhan model)
        {
            var dao = new NguoiDungServices();
            ThongTinCaNhan ttcn = dao.GetByIdTTCN(model.IdTTCN);
            ttcn.IdTTCN = model.IdTTCN;
            ttcn.hoTen = model.hoTen;
            ttcn.gioiTinh = model.gioiTinh;
            ttcn.soDT = model.soDT;
            ttcn.soLanHM = model.soLanHM;
            ttcn.CCCD = model.CCCD;
            ttcn.ngheNghiep = model.ngheNghiep;
            ttcn.diaChi = model.diaChi;
            ttcn.nhomMau = model.nhomMau;
            ttcn.soLanHM = model.soLanHM;
            ttcn.trinhDo = model.trinhDo;
            ttcn.coQuanTH = model.coQuanTH;
            dao.CapNhatTTCN(ttcn);
            SetAlert("Cập nhật thành công", "success");
            return RedirectToAction("Index", "NguoiDung");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CapnhatTTCN(ThongTinCaNhan model)
        //{
        //    var dao = new NguoiDungServices();
        //    if (ModelState.IsValid)
        //    {
        //        ThongTinCaNhan ttcn = dao.GetByIdTTCN(model.IdTTCN);
        //        ttcn.IdTTCN = model.IdTTCN;
        //        ttcn.hoTen = model.hoTen;
        //        ttcn.gioiTinh = model.gioiTinh;
        //        ttcn.soDT = model.soDT;
        //        ttcn.soLanHM = model.soLanHM;
        //        ttcn.CCCD = model.CCCD;
        //        ttcn.ngheNghiep = model.ngheNghiep;
        //        ttcn.diaChi = model.diaChi;
        //        ttcn.nhomMau = model.nhomMau;
        //        ttcn.soLanHM = model.soLanHM;
        //        ttcn.trinhDo = model.trinhDo;
        //        ttcn.coQuanTH = model.coQuanTH;
        //        dao.CapNhatTTCN(ttcn);
        //        SetAlert("Cập nhật thành công", "success");
        //        return RedirectToAction("Index", "NguoiDung");
        //    }
        //    else
        //    {
        //        SetAlert("Cập nhật thất bại", "error");
        //        return RedirectToAction("CapnhatTTCN", "NguoiDung");
        //    }
        //    return View(model);
        //}



    }
}