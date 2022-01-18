using HiemMauNhanDao.Areas.Admin.Controllers;
using Models.EF;
using Models.Services;
using Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HiemMauNhanDao.Controllers
{
    public class KetQuaHienMauController : BaseController2
    {
        private DbContextHM db = new DbContextHM();
        KetQuaHienMauServices _kqhm = new KetQuaHienMauServices();

        //Index
        // - chỉ hiện những người dùng đăng ký đợt tổ chức của mình đăng bài
        // VD: bênh viện 1 - đăng bài đợt tổ chức 1 -> thì chỉ hiện những người đk đợt tổ chức 1
        public ActionResult Index(string searchString ,int page = 1, int pageSize = 100)
        {
            var dsdk = new KetQuaHienMauServices();
            var model = dsdk.GetListKQHM(searchString , page, pageSize);
            ViewBag.SearchStringDK = searchString;          
            return View(model);
        }
        public ActionResult DanhSachKQ(string searchString, int page = 1, int pageSize = 100)
        {
            var dsdk = new KetQuaHienMauServices();
            var model = dsdk.GetListKQHM2(searchString, page, pageSize);
            ViewBag.SearchStringKQ = searchString;
            return View(model);
        }


        //Create 1
        // - người đầu tiên sẽ có nhiệm vụ tạo kết quả hiến máu cho người đăng ký
        // - nhập các nội dung ở mục : Khám lâm sàng
        // - các trường dữ liệu tạo ở mục Khám lâm sàn: idPDKHM, trangThai,ghiChu,LayMau,luongMauHien,hienMau,
        //                                              tinhTrangLS,huyetAp,machMau,canNang,nguoiKham(lấy từ list nhân viên y tế "idNVYT")
        // ở chỗ này có thể bị bug thuộc tính idNVYT (chú ý)
        public ActionResult Create(string id)
        {
            //ViewBag.idPDKHM = new SelectList(db.PhieuDKHMs, "idPDKHM", "idDTCHM");
            var result = db.KetQuaHienMaus.Where(x => x.idPDKHM == id).FirstOrDefault();
            if (result == null)
            {
                return RedirectToAction("Error");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdKQHM,idPDKHM,nhomMau,idnguoiKham,nguoiXN,nguoiLayMau,canNang,machMau,tinhTrangLS,huyetAp,luongMauHien,hienMau,noiDung,HST,HBV,MSD,phanUng,thoiGianLayMau,ghiChu,trangThai")] KetQuaHienMau ketQuaHienMau, string id)
        {
            var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];
            var tempNVYT = db.NhanVienYTes.Where(x => x.idTTCN == session.UserID).SingleOrDefault();
            string ids = db.KetQuaHienMaus.Max(x => x.IdKQHM);
            int stt = Convert.ToInt32(ids.Substring(2)) + 1;

            if (ModelState.IsValid == false)
            {
                ketQuaHienMau.IdKQHM = stt > 9 ? "KQ" + stt : "KQ0" + stt;
                ketQuaHienMau.idPDKHM = id;
                ketQuaHienMau.idnguoiKham = tempNVYT.IdNVYT;
                ketQuaHienMau.nguoiKham = session.Name;
                ketQuaHienMau.tgKham = DateTime.Now;
                db.KetQuaHienMaus.Add(ketQuaHienMau);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.idPDKHM = new SelectList(db.PhieuDKHMs, "idPDKHM", "idDTCHM", ketQuaHienMau.idPDKHM);
            return View(ketQuaHienMau);
        }


        //Edit 
        // - 2 người có nhiện vụ còn lại sẽ cập nhật
        // - các trường dữ liệu tạo ở mục Lấy máu,Xét nghiệm trước HM:
        //  Xét nghiệm: HST,HBV,trangThai,nguoiXN
        // Lấy máu: trangThai,nhomMau,noiDung,phanUng,thoiGianLayMau,MSD, nguoiLayMau
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KetQuaHienMau ketQuaHienMau = db.KetQuaHienMaus.Find(id);
            if (ketQuaHienMau == null)
            {
                return HttpNotFound();
            }
            return View(ketQuaHienMau);
        }
          [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include =  "IdKQHM,idPDKHM, idnguoiKham,idnguoiXN,idnguoiLayMau,tgKham,tgXetNghiem," +
                                                  "tgLayMau,tgCapNhat,nhomMau,canNang,machMau,tinhTrangLS,huyetAp,luongMauHien," +
                                                   "hienMau, noiDung, HST, HBV,MSD, phanUng,ghiChu,trangThai" )] KetQuaHienMau ketQuaHienMau, string id)
        {
            var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];
            var tempNVYT = db.NhanVienYTes.Where(x => x.idTTCN == session.UserID).SingleOrDefault();

            if (ModelState.IsValid == false)
            {
                var kqhm = _kqhm.GetByIdKQHM(ketQuaHienMau.IdKQHM);

                ketQuaHienMau.idnguoiXN = tempNVYT.IdNVYT;         
                ketQuaHienMau.tgXetNghiem = DateTime.Now;
                ketQuaHienMau.tgCapNhat = DateTime.Now;
                ketQuaHienMau.nguoiXN = session.Name;

                kqhm.IdKQHM = kqhm.IdKQHM;
                kqhm.idPDKHM = kqhm.idPDKHM;
                kqhm.idnguoiKham = kqhm.idPDKHM;
                kqhm.idnguoiLayMau = kqhm.idnguoiLayMau;
                kqhm.tgKham = kqhm.tgKham;
                kqhm.tgLayMau = kqhm.tgLayMau;
                kqhm.nhomMau = kqhm.idPDKHM;
                kqhm.canNang = kqhm.canNang;
                kqhm.tinhTrangLS = kqhm.tinhTrangLS;
                kqhm.machMau = kqhm.machMau;
                kqhm.trangThai = kqhm.trangThai;
                kqhm.huyetAp = kqhm.huyetAp;
                kqhm.luongMauHien = kqhm.luongMauHien;
                kqhm.hienMau = kqhm.hienMau;
                kqhm.noiDung = kqhm.noiDung;
                kqhm.HST = kqhm.HST;
                kqhm.HBV = kqhm.HBV;
                kqhm.MSD = kqhm.MSD;
                kqhm.phanUng = kqhm.phanUng;
                kqhm.ghiChu = kqhm.ghiChu;

                db.Entry(ketQuaHienMau).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Cập nhật thành công ", "success");
                return RedirectToAction("Index", "KetQuaHienMau");
            }
            else
            {
                SetAlert("Vui lòng cập nhập thất bại.Vui lòng kiểm tra trường dữ liệu ", "error");
                return RedirectToAction("Edit", "KetQuaHienMau");
            }
           
        }

        public ActionResult layMau(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KetQuaHienMau ketQuaHienMau = db.KetQuaHienMaus.Find(id);
            if (ketQuaHienMau == null)
            {
                return HttpNotFound();
            }
            return View(ketQuaHienMau);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult layMau([Bind(Include = "MSD,trangThai,nhomMau,noiDung,phanUng,tgLayMau")] KetQuaHienMau ketQuaHienMau, string id)
        {
            var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];
            var tempNVYT = db.NhanVienYTes.Where(x => x.idTTCN == session.UserID).SingleOrDefault();

            if (ModelState.IsValid == false)
            {
                ketQuaHienMau.idnguoiLayMau = tempNVYT.IdNVYT;
                ketQuaHienMau.nguoiLayMau = session.Name;
                ketQuaHienMau.IdKQHM = ketQuaHienMau.IdKQHM;
                ketQuaHienMau.tgCapNhat = DateTime.Now;
                db.Entry(ketQuaHienMau).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Cập nhật thành công ", "success");
                return RedirectToAction("Index", "KetQuaHienMau");
            }
            else
            {
                SetAlert("Vui lòng cập nhập thất bại.Vui lòng kiểm tra trường dữ liệu ", "error");
                return RedirectToAction("layMau", "KetQuaHienMau");

            }

           
        }



        public ActionResult Details(string id)
        {
            var model = _kqhm.GetByIdTTDK2(id);
            return View(model);
        }

        //đã làm được, select từ 3 bảng : thông tin cá nhân + phiếu đăng ký + kết quả hiến máu
        //chi tiết gồm thông tin cá nhân + phiếu đk + kết quả hiến máu
        [HttpGet]
        public ActionResult TTDK(string id)
        {
            var model = _kqhm.GetByIdTTDK(id);
            return View(model);
        }


        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KetQuaHienMau ketQuaHienMau = db.KetQuaHienMaus.Find(id);
            if (ketQuaHienMau == null)
            {
                return HttpNotFound();
            }
            return View(ketQuaHienMau);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            KetQuaHienMau ketQuaHienMau = db.KetQuaHienMaus.Find(id);
            db.KetQuaHienMaus.Remove(ketQuaHienMau);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //xử lý duyệt = json
        [HttpPost]
        public JsonResult ChangeStatus4(string id)
        {
            var result = new KetQuaHienMauServices().ChangeStatus4(id);
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

        //public ActionResult Index()
        //{
        //    var ketQuaHienMaus = db.KetQuaHienMaus.Include(k => k.PhieuDKHM);
        //    return View(ketQuaHienMaus.ToList());
        //}
    }
}