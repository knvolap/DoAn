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
    public class NguoiDungController : BaseController
    {
        NguoiDungServices _NguoiDung = new NguoiDungServices();
        private DbContextHM db = new DbContextHM();

        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(ThongTinCaNhan thongTinCaNhan)
        {
            if (ModelState.IsValid)
            {            
                if (_NguoiDung.isExistTaiKhoan(thongTinCaNhan.CCCD))
                {
                    ViewBag.ThongBao = "CCCD/ CMND đã tồn tại!";
                }
                else if (_NguoiDung.isExistTaiKhoan(thongTinCaNhan.soDT))
                {
                    ViewBag.ThongBao = "Số điện thoại đã tồn tại!";
                }
                else if (_NguoiDung.isExistTaiKhoan(thongTinCaNhan.Email))
                {
                    ViewBag.ThongBao = "Email đã tồn tại!";
                }                           
                else
                {                 
                    _NguoiDung.ThemTTCN(thongTinCaNhan);
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


        public ActionResult EditTTCN(string id)
        {
            return View(_NguoiDung.GetByIdTTCN(id));
        }
        [HttpPost]
        public ActionResult EditTTCN(ThongTinCaNhan thongTinCaNhan)
        {
            if (ModelState.IsValid)
            {
                if (_NguoiDung.isExistTaiKhoan(thongTinCaNhan.CCCD))
                {
                    ViewBag.ThongBao = "CCCD/ CMND đã tồn tại!";
                }
                else if (_NguoiDung.isExistTaiKhoan(thongTinCaNhan.soDT))
                {
                    ViewBag.ThongBao = "Số điện thoại đã tồn tại!";
                }
                else if (_NguoiDung.isExistTaiKhoan(thongTinCaNhan.Email))
                {
                    ViewBag.ThongBao = "Email đã tồn tại!";
                }
                else
                {
                    var encryptedMd5Pas = Encryptor.MD5Hash(thongTinCaNhan.password);
                    thongTinCaNhan.password = encryptedMd5Pas;
                    _NguoiDung.SuaTTCN(thongTinCaNhan);
                    SetAlert("Cập nhật thông tin thành công", "success");
                    return RedirectToAction("Index");
                }
            }
            else
            {
                SetAlert("Cập nhật thông tin thất bại công", "success");
                return RedirectToAction("Index");
            }
            return View(thongTinCaNhan);          
        }

    }
}