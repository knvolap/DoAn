using HiemMauNhanDao.Areas.Admin.Controllers;
using HiemMauNhanDao.Common;
using Models.EF;
using Models.Services;
using Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public ActionResult Index(string searchString1 , int page = 1, int pageSize = 10)
        {
            var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];
            string id = session.UserID;
            var tempNVYT = db.NhanVienYTes.Where(x => x.IdNVYT == session.UserID).FirstOrDefault();

            var viewNVYT = new NhanVienYTeServices();
            var model = viewNVYT.GetListNVYT2(searchString1, tempNVYT.idBenhVien, page, pageSize);
            if (!string.IsNullOrEmpty(searchString1))
            {
                ViewBag.SearchStringNV = searchString1;
            }
            return View(model);
        }
        public ActionResult ViewNVYT(string searchString1, string idbv, int page = 1, int pageSize = 10)
        {           
            var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];
            string id = session.UserID;
            var tempNVYT = db.NhanVienYTes.Where(x => x.idTTCN == session.UserID).FirstOrDefault();
          
            var viewNVYT = new NhanVienYTeServices();
            var model = viewNVYT.GetListNVYT2(searchString1, tempNVYT.idBenhVien, page, pageSize);
            if (!string.IsNullOrEmpty(searchString1))
            {
                ViewBag.SearchStringNV = searchString1;
            }

            return View(model);
        }

        public ActionResult XemNV(string searchString1, string idbv, int page = 1, int pageSize = 10)
        {
            var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];
            string id = session.UserID;
            var tempNVYT = db.NhanVienYTes.Where(x => x.idTTCN == session.UserID).FirstOrDefault();

            var viewNVYT = new NhanVienYTeServices();
            var model = viewNVYT.GetListNVYT3(searchString1, tempNVYT.idBenhVien, page, pageSize);
            if (!string.IsNullOrEmpty(searchString1))
            {
                ViewBag.SearchStringNV = searchString1;
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
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LienKetNV([Bind(Include = "IdNVYT,idTTCN,trangThai,idChucVu,idBenhVien,khoa,trinhDo,")]NhanVienYTe nhanVienYTe)
        {
            var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];
            ViewBag.IdBenhVien = new SelectList(db.BenhViens, "IdBenhVien", "TenBenhVien", nhanVienYTe.idBenhVien);
          
            if (ModelState.IsValid)
            {
                if (_nhanvien.isExistNVYT(nhanVienYTe.IdNVYT))
                {
                    SetAlert("Bạn đã đăng ký rồi", "error");                 
                }
                else if (_nhanvien.isExistIDTK(nhanVienYTe.idTTCN))
                {
                    SetAlert("Bạn đã đăng ký rồi", "error");                  
                }
                else
                {
                    nhanVienYTe.IdNVYT = "NV" + (db.NhanVienYTes.Count() + 1).ToString();
                    nhanVienYTe.idTTCN = session.UserID;
                    nhanVienYTe.trangThai = false;
                    db.NhanVienYTes.Add(nhanVienYTe);
                    db.SaveChanges();
                    SetAlert("Đăng ký thành công", "success");
                    return RedirectToAction("LienKetNV", "NhanVienYTe");
                }                   
            }
            else
            {
                SetAlert("Đăng ký thất bại", "error");
                return RedirectToAction("LienKetNV", "NhanVienYTe");
            }
            return View(nhanVienYTe);
        }
        public ActionResult CapQuyenNVYT(string id)
        {
            var model = _nhanvien.GetByIdTTCN(id);
            ViewBag.IdQuyen = new SelectList(db.Quyens, "IdQuyen", "tenQuyen");
            return View(model);
 
        }
        [HttpPost]
        public ActionResult CapQuyenNVYT([Bind(Include = "IdTTCN,hoTen,gioiTinh,soDT,soLanHM,ngheNghiep,nhomMau,trinhDo,coQuanTH,diaChi,userName,ngaySinh,CCCD,idQuyen,trangThai,password")] ThongTinCaNhan model)
        {
            var ttcn = _nhanvien.GetByIdTTCN(model.IdTTCN);
            if (ModelState.IsValid)
            {
                if (ttcn.idQuyen == model.idQuyen)
                {
                    SetAlert("Quyền này đã có", "error");
                }
                else
                {                    
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
                    ttcn.hoTen = ttcn.hoTen;
                    ttcn.gioiTinh = ttcn.gioiTinh;
                    ttcn.trangThai = ttcn.trangThai;
                    ttcn.nhomMau = ttcn.nhomMau;
                    ttcn.diaChi = ttcn.diaChi;
                    ttcn.ngheNghiep = ttcn.ngheNghiep;
                    ttcn.coQuanTH = ttcn.coQuanTH;
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();

                    SetAlert("Cấp quyền thành công", "success");
                    return RedirectToAction("Index");
                }    
               
            }
            else
            {
                SetAlert("Cấp quyền thất bại", "error");
                return RedirectToAction("CapQuyenNVYT");
            }
            return View(model);
        }
        
       

        public ActionResult EditKQHM()
        {
            return View();
        }
     
        public ActionResult EditBV(string id)
        {
            return View(_BenhVien.GetByIdBenhVien1(id));
        }

        [HttpPost]
        public ActionResult EditBV(BenhVien benhVien, HttpPostedFileBase file)
        {
            string duongDan = Server.MapPath("~/FileUpLoad");
            string fileName = Path.GetFileName(file.FileName);
            string fullDuongDan = Path.Combine(duongDan, fileName);

            file.SaveAs(fullDuongDan);

            _BenhVien.EditBV(benhVien, fileName);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            if (ModelState.IsValid)
            {
                _nhanvien.XoaNVYT(id);
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
            var model = _nhanvien.GetByIdNVYT2(id);

            return View(model);
        }

        //xử lý duyệt = json
        [HttpPost]
        public JsonResult ChangeStatus3(string id)
        {
            var result = new NhanVienYTeServices().ChangeStatus3(id);
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