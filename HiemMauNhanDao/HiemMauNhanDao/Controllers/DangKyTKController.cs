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
    public class DangKyTKController : BaseController3
    {
        DKTKServices _DKTK = new DKTKServices();
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
        [ValidateAntiForgeryToken]
        public ActionResult NguoiDung(ThongTinCaNhan thongTinCaNhan, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                if (_DKTK.isExistTaiKhoan(thongTinCaNhan.userName))
                {
                    SetAlert("Tài khoản đã tồn tại!", "error");                   
                }
                else if (_DKTK.isExistSDT(thongTinCaNhan.soDT))
                {
                    SetAlert("Số điện thoại đã tồn tại!", "error");             
                }
                else if (_DKTK.isExistCCCD(thongTinCaNhan.CCCD))
                {
                    SetAlert("CCCD/ CMND đã tồn tại!!", "error");
                }                
                else
                {
                    var encryptedMd5Pas = Encryptor.MD5Hash(thongTinCaNhan.password);
                    thongTinCaNhan.password = encryptedMd5Pas;
                    thongTinCaNhan.idQuyen = "Q06";
                    thongTinCaNhan.trangThai = true;
                    _DKTK.ThemTTCN(thongTinCaNhan);
                    SetAlert("Tạo tài khoản thành công", "success");
                    return RedirectToAction("NguoiDung");
                }
            }
            else
            {
                SetAlert("Tạo tài khoản thất bại ", "error");
                return RedirectToAction("NguoiDung");
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