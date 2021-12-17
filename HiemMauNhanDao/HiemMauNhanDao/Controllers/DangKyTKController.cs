using HiemMauNhanDao.Areas.Admin.Controllers;
using HiemMauNhanDao.Common;
using Models.EF;
using Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiemMauNhanDao.Controllers
{
    public class DangKyTKController : BaseController2
    {
        NguoiDungServices _DKTK = new NguoiDungServices();
        private DbContextHM db = new DbContextHM();

        // GET: DangKyTK
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NguoiDung()
        {
            return View();
        }     
        [HttpPost]
        public ActionResult NguoiDung(ThongTinCaNhan thongTinCaNhan, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                if (_DKTK.isExistTaiKhoan(thongTinCaNhan.userName))
                {
                    ViewBag.ThongBao = "Tài khoản đã tồn tại!";
                }
                else if (_DKTK.isExistTaiKhoan(thongTinCaNhan.soDT))
                {
                    ViewBag.ThongBao = "Số điện thoại đã tồn tại!";
                }
                else if (_DKTK.isExistTaiKhoan(thongTinCaNhan.Email))
                {
                    ViewBag.ThongBao = "Email đã tồn tại!";
                }
                else if (_DKTK.isExistTaiKhoan(thongTinCaNhan.CCCD))
                {
                    ViewBag.ThongBao = "CCCD/ CMND đã tồn tại!";
                }
                else if (fc["txtXacNhanMK"] != thongTinCaNhan.password)
                {
                    ViewBag.ThongBao = "Vui lòng nhập mật khẩu dài hơn!";
                }
                else
                {
                    var encryptedMd5Pas = Encryptor.MD5Hash(thongTinCaNhan.password);
                    thongTinCaNhan.password = encryptedMd5Pas;
                    _DKTK.ThemTTCN(thongTinCaNhan);
                    SetAlert("Tạo tài khoản thành công", "success");
                    return RedirectToAction("Index");
                }
            }
            else
            {
                SetAlert("Tạo tài khoản thất bại công", "success");
                return RedirectToAction("Index");
            }
            return View(thongTinCaNhan);
        }
        public ActionResult DonVi()
        {
            return View();
        }

        public ActionResult BenhVien()
        {
            return View();
        }
    }
}