using HiemMauNhanDao.Areas.Admin.Controllers;
using Models.EF;
using Models.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HiemMauNhanDao.Controllers
{
    public class DotToChucHMController : BaseController2
    {
        private DbContextHM db = new DbContextHM();
        DotToChucHMServices _DTCHM = new DotToChucHMServices();


        //chỉ hiển thị các đợt tổ chức của bệnh viện
        //bệnh viện Đa khoa đăng bài thì hiển thị những bài đăng của nhân viên y tế thuộc bệnh viên đa khoa đăng
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];

            var tempNVYT = db.NhanVienYTes.Where(x => x.idTTCN == session.UserID).FirstOrDefault();

            var dotToChucHMs = new DotToChucHMServices();
            var model = dotToChucHMs.ListAllBaiDang2(searchString, tempNVYT.idBenhVien, page, pageSize);
            ViewBag.SearchStringDTCHM = searchString;
            return View(model);

        }

        [HttpGet]
        public ActionResult Index2(string searchString, int page = 1, int pageSize = 10)
        {
            var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];
         
            var tempNVYT = db.NhanVienYTes.Where(x => x.idTTCN == session.UserID).FirstOrDefault();

            var dotToChucHMs = new DotToChucHMServices();
            var model = dotToChucHMs.ListAllBaiDang2(searchString, tempNVYT.idBenhVien , page, pageSize);
            ViewBag.SearchStringDTCHM = searchString;
            return View(model);
                              
        }
       

        // thời gian bắt đầu và kết thúc nằm trong khoảng tg của đợt hiến máu
        // admin duyệt bài thì lên trang chủ nằm ở /DTCHM/Index và /DTCHM/ChiTietDTCHM/
        // Khi hết hạn thì trạng thái tự chuyển thành hết hạn 
        // hết hạn thì không ấn vào dăng ký được nữa
        // 

        public ActionResult Create()
        {
            var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];

            var tempNVYT = db.NhanVienYTes.Where(x => x.idTTCN == session.UserID).FirstOrDefault();

            ViewBag.IdChiTietDHM = new SelectList(db.chiTietDHMs.Where(x=>x.idBenhVien==tempNVYT.idBenhVien), "IdChiTietDHM", "idChiTietDHM");
                
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDTCHM,IdChiTietDHM,tenDotHienMau,noiDung,doiTuongThamGia,diaChiToChuc,soLuong,ngayBatDauDK,ngayKetThucDK,ngayToChuc,trangThai")] DotToChucHM dotToChucHM)
        {
            var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];

            string id = db.DotToChucHMs.Max(x => x.IdDTCHM);
            int stt = Convert.ToInt32(id.Substring(4)) + 1;
           
            var tempDSDK = db.chiTietDHMs.Where(x => x.IdChiTietDHM == dotToChucHM.idChiTietDHM).SingleOrDefault();
            var tempDHM = db.DotHienMaus.Where(x => x.IdDHM == tempDSDK.idDHM).SingleOrDefault();

            if (ModelState.IsValid)
            {
                if (_DTCHM.CheckTimeDuplicate(dotToChucHM.ngayBatDauDK, dotToChucHM.ngayKetThucDK, dotToChucHM.ngayToChuc, tempDHM.tgBatDau, tempDHM.tgKetThuc) == true)
                {
                    if (dotToChucHM.ngayBatDauDK < dotToChucHM.ngayKetThucDK && dotToChucHM.ngayToChuc >= dotToChucHM.ngayKetThucDK)
                    {
                        dotToChucHM.IdDTCHM = stt > 9 ? "DTC" + stt : "DTC0" + stt;
                        dotToChucHM.trangThai = true;
                        dotToChucHM.tenNguoiDangBai = session.Name;

                        db.DotToChucHMs.Add(dotToChucHM);
                        db.SaveChanges();
                        SetAlert("Đăng bài thành công", "success");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        SetAlert("Đăng bài thất bại!!! Vui lòng nhập Ngày bắt đầu < Ngày kết thúc < Ngày đăng ký" , "error");
                        return RedirectToAction("Create");                
                    }
                }
                else
                {
                    SetAlert("Khoảng thời gian đăng ký không nằm trong đợt hiến máu!" , "error");
                    return RedirectToAction("Create");                
                }
            }
            ViewBag.IdChiTietDHM = new SelectList(db.chiTietDHMs, "IdChiTietDHM", "idChiTietDHM", dotToChucHM.idChiTietDHM);
            return View(dotToChucHM);
        }
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DotToChucHM dotToChucHM = db.DotToChucHMs.Find(id);
            if (dotToChucHM == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdChiTietDHM = new SelectList(db.chiTietDHMs, "IdChiTietDHM", "idChiTietDHM", dotToChucHM.idChiTietDHM);
            return View(dotToChucHM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDTCHM,IdChiTietDHM,tenDotHienMau,noiDung,doiTuongThamGia,diaChiToChuc,soLuong,ngayBatDauDK,ngayKetThucDK,ngayToChuc,trangThai")] DotToChucHM dotToChucHM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dotToChucHM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdChiTietDHM = new SelectList(db.chiTietDHMs, "IdChiTietDHM", "idDHM", dotToChucHM.idChiTietDHM);
            return View(dotToChucHM);
        }

        public ActionResult TaoNV(string id)
        {
            var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];
            var tempNVYT = db.NhanVienYTes.Where(x => x.idTTCN == session.UserID).FirstOrDefault();
            ViewBag.IdNVYT = new SelectList(db.NhanVienYTes.Where(x => x.idBenhVien == tempNVYT.idBenhVien), "IdNVYT", "idNVYT");
            return View( );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TaoNV([Bind(Include = "idDTCHM,idNVYT,nhiemVu ")] DSNVTH dsnv, string id)
        {
            var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];
            var tempNVYT = db.NhanVienYTes.Where(x => x.idTTCN == session.UserID).FirstOrDefault();
            ViewBag.IdNVYT = new SelectList(db.NhanVienYTes.Where(x => x.idBenhVien == tempNVYT.idBenhVien), "IdNVYT", "idNVYT");

            if (ModelState.IsValid)
            {
                dsnv.idDTCHM = id;
                db.DSNVTHs.Add(dsnv);
                db.SaveChanges();               
                SetAlert("Phân chia nhiệm vụ thành công", "success");
                return RedirectToAction("Index");
            }
            else
            {
                SetAlert("Phân chia nhiệm vụ thất bại!", "error");
                return RedirectToAction("TaoNV");
            }            
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DotToChucHM dotToChucHM = db.DotToChucHMs.Find(id);
            if (dotToChucHM == null)
            {
                return HttpNotFound();
            }
            return View(dotToChucHM);
        }

        public ActionResult Delete(string id)
        {
            if (ModelState.IsValid)
            {
                _DTCHM.Delete(id);
                SetAlert("Xóa thành công", "success");
                return RedirectToAction("Index");
            }
            else
            {
                SetAlert("Xóa thất bại", "error");
                return RedirectToAction("Index");
            }
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

//public ActionResult Index()
//{
//    var dotToChucHMs = db.DotToChucHMs.Include(d => d.chiTietDHM);
//    return View(dotToChucHMs.ToList());
//}