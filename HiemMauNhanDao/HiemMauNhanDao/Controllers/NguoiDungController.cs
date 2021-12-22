using HiemMauNhanDao.Areas.Admin.Controllers;
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

       
        public ActionResult CapnhatTTCN(string id)
        {
            ThongTinCaNhan thongTinCaNhan = db.ThongTinCaNhans.Find(id);
            return View(thongTinCaNhan);
        }
      

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CapnhatTTCN(ThongTinCaNhan model)
        {
            if (ModelState.IsValid)
                {
                var dao = new NguoiDungServices();
                ThongTinCaNhan ttcn = dao.GetByIdTTCN(model.IdTTCN);
                ttcn.IdTTCN = model.IdTTCN;
                ttcn.hoTen = model.hoTen;
                ttcn.gioiTinh = model.gioiTinh;
                ttcn.soDT = model.soDT;
                ttcn.soLanHM = model.soLanHM;
                ttcn.CCCD = model.CCCD;
                ttcn.userName = model.userName;
                ttcn.ngheNghiep = model.ngheNghiep;
                ttcn.ngaySinh = model.ngaySinh;
                ttcn.diaChi = model.diaChi;
                ttcn.nhomMau = model.nhomMau;
                ttcn.soLanHM = model.soLanHM;
                ttcn.trinhDo = model.trinhDo;
                ttcn.coQuanTH = model.coQuanTH;
                dao.CapNhatTTCN(ttcn);
                SetAlert("Cập nhật thành công", "success");
                return RedirectToAction("CapnhatTTCN", "NguoiDung");
            }
            else
            {
                SetAlert("Cập nhật thất bại", "error");
                return RedirectToAction("CapnhatTTCN", "NguoiDung");
            }
            return View(model);
        }
        



        //var dao = new NguoiDungServices();
        //ThongTinCaNhan ttcn = dao.GetByIdTTCN(model.IdTTCN);
        //ttcn.IdTTCN = model.IdTTCN;
        //    ttcn.hoTen = model.hoTen;
        //    ttcn.gioiTinh = model.gioiTinh;
        //    ttcn.soDT = model.soDT;
        //    ttcn.soLanHM = model.soLanHM;
        //    ttcn.CCCD = model.CCCD;
        //    ttcn.userName = model.userName;
        //    ttcn.ngheNghiep = model.ngheNghiep;
        //    ttcn.ngaySinh = model.ngaySinh;
        //    ttcn.diaChi = model.diaChi;
        //    ttcn.nhomMau = model.nhomMau;
        //    ttcn.soLanHM = model.soLanHM;
        //    ttcn.trinhDo = model.trinhDo;
        //    ttcn.coQuanTH = model.coQuanTH;         
        //    dao.CapNhatTTCN(ttcn);
        //    SetAlert("Cập nhật thành công", "success");
        //    return RedirectToAction("Index", "NguoiDung");




        //[HttpPost]
        //public ActionResult CapnhatTTCN(ThongTinCaNhan thongTinCaNhan)
        //{

        //    var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];
        //    string id = session.UserID;
        //    thongTinCaNhan = db.ThongTinCaNhans.Find(id);

        //    _NguoiDung.SuaTTCN(thongTinCaNhan);
        //    return RedirectToAction("CapnhatTTCN");

        //    //if (ModelState.IsValid)
        //    //{
        //    //    var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];
        //    //    string id = session.UserID;
        //    //    thongTinCaNhan = db.ThongTinCaNhans.Find(id);
        //    //    _NguoiDung.SuaTTCN(thongTinCaNhan);

        //    //    SetAlert("Cập nhật thành công", "success");
        //    //    return RedirectToAction("CapnhatTTCN");
        //    //}
        //    //else
        //    //{
        //    //    SetAlert("Cập nhật thất bại", "error");
        //    //    return RedirectToAction("CapnhatTTCN");
        //    //}
        //    //return View(thongTinCaNhan);
        //}





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