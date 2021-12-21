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
    public class NguoiDungController : BaseController2
    {
        NguoiDungServices _NguoiDung = new NguoiDungServices();
        public UserLogin userSession = new UserLogin();
        private DbContextHM db = new DbContextHM();

 
        // GET: NguoiDung
        public ActionResult Index()
        {
          
            return View();
        }

       

        [HttpPost]
        public ActionResult Index(ThongTinCaNhan thongTinCaNhan )
        { 
            if (ModelState.IsValid)
            {
                ThongTinCaNhan ttcn = _NguoiDung.GetByIdTTCN(thongTinCaNhan.IdTTCN);
                ttcn.hoTen = thongTinCaNhan.hoTen;
                ttcn.Email = thongTinCaNhan.Email;
                ttcn.CCCD = thongTinCaNhan.CCCD;
                ttcn.soDT = thongTinCaNhan.soDT;
                ttcn.ngaySinh = thongTinCaNhan.ngaySinh;
                ttcn.ngheNghiep = thongTinCaNhan.ngheNghiep;
                ttcn.gioiTinh = thongTinCaNhan.gioiTinh;
                ttcn.diaChi = thongTinCaNhan.diaChi;
                ttcn.trinhDo = thongTinCaNhan.trinhDo;
                ttcn.soLanHM = thongTinCaNhan.soLanHM;
                ttcn.coQuanTH = thongTinCaNhan.coQuanTH;
                ttcn.nhomMau = thongTinCaNhan.nhomMau;
                _NguoiDung.SuaTTCN(thongTinCaNhan);
                SetAlert("cập nhật thành công", "success");
                return RedirectToAction("CapnhatTTCN");
            }
            else
            {
                SetAlert("Cập nhật thông tin thất bại ", "error");
                return RedirectToAction("CapnhatTTCN");
            }
            return View(thongTinCaNhan);
        }


        public ActionResult CapnhatTTCN()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CapnhatTTCN(ThongTinCaNhan model)
        {
            if (ModelState.IsValid)
            {
                ThongTinCaNhan ttcn = _NguoiDung.GetByIdTTCN(model.IdTTCN);
                userSession.UserID = ttcn.IdTTCN;

                Session.Add(CommonConstant.USER_SESSION, userSession);
                ttcn.hoTen = model.hoTen;
                ttcn.Email = model.Email;
                ttcn.CCCD = model.CCCD;
                ttcn.soDT = model.soDT;
                ttcn.ngaySinh = model.ngaySinh;
                ttcn.ngheNghiep = model.ngheNghiep;
                ttcn.gioiTinh = model.gioiTinh;
                ttcn.diaChi = model.diaChi;
                ttcn.trinhDo = model.trinhDo;
                ttcn.soLanHM = model.soLanHM;
                ttcn.coQuanTH = model.coQuanTH;
                ttcn.nhomMau = model.nhomMau;
                _NguoiDung.SuaTTCN(model);
                SetAlert("cập nhật thành công", "success");
                return RedirectToAction("CapnhatTTCN");
            }
            else
            {
                SetAlert("Cập nhật thông tin thất bại ", "error");
                return RedirectToAction("CapnhatTTCN");
            }
            return View(model);
         
        }







        public ActionResult EditTTCN(string id)
        {
            return View(_NguoiDung.GetByIdTTCN(id));
        }
        [HttpPost]
        public ActionResult EditTTCN(ThongTinCaNhan thongTinCaNhan)
        {
            if(ModelState.IsValid)
            {
                ThongTinCaNhan ttcn = _NguoiDung.GetByIdTTCN(thongTinCaNhan.IdTTCN);
                _NguoiDung.SuaTTCN(thongTinCaNhan);
                SetAlert("cập nhật thành công", "success");
                return RedirectToAction("Index");
            }    
            else
            {
                SetAlert("Cập nhật thông tin thất bại công", "error");
               return RedirectToAction("Index");
            }
            return View(thongTinCaNhan);
        }








        //[HttpPost]
        //public ActionResult Index(ThongTinCaNhan thongTinCaNhan)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (_NguoiDung.isExistTaiKhoan(thongTinCaNhan.userName))
        //        {
        //            ViewBag.ThongBao = "Tài khoản đã tồn tại!";
        //        }
        //        else if (_NguoiDung.isExistSDT(thongTinCaNhan.soDT))
        //        {
        //            ViewBag.ThongBao = "Số điện thoại đã tồn tại!";
        //        }
        //        else if (_NguoiDung.isExistEmail(thongTinCaNhan.Email))
        //        {
        //            ViewBag.ThongBao = "Email đã tồn tại!";
        //        }
        //        else if (_NguoiDung.isExistCCCD(thongTinCaNhan.CCCD))
        //        {
        //            ViewBag.ThongBao = "CCCD/ CMND đã tồn tại!";
        //        }
        //        else
        //        {
        //            _NguoiDung.SuaTTCN(thongTinCaNhan);
        //            SetAlert("Cập nhật thông tin thành công", "success");
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    else
        //    {
        //        SetAlert("Cập nhật thông tin  thất bại ", "error");
        //        return RedirectToAction("Index");
        //    }

            
        //    return View(thongTinCaNhan);
        //}

        //[HttpPost]
        //public ActionResult EditTTCN(ThongTinCaNhan thongTinCaNhan)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (_NguoiDung.isExistTaiKhoan(thongTinCaNhan.CCCD))
        //        {
        //            ViewBag.ThongBao = "CCCD/ CMND đã tồn tại!";
        //        }
        //        else if (_NguoiDung.isExistTaiKhoan(thongTinCaNhan.soDT))
        //        {
        //            ViewBag.ThongBao = "Số điện thoại đã tồn tại!";
        //        }
        //        else if (_NguoiDung.isExistTaiKhoan(thongTinCaNhan.Email))
        //        {
        //            ViewBag.ThongBao = "Email đã tồn tại!";
        //        }
        //        else
        //        {
        //            var encryptedMd5Pas = Encryptor.MD5Hash(thongTinCaNhan.password);
        //            thongTinCaNhan.password = encryptedMd5Pas;
        //            _NguoiDung.SuaTTCN(thongTinCaNhan);
        //            SetAlert("Cập nhật thông tin thành công", "success");
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    else
        //    {
        //        SetAlert("Cập nhật thông tin thất bại công", "success");
        //        return RedirectToAction("Index");
        //    }
        //    return View(thongTinCaNhan);          
        //}

    }
}