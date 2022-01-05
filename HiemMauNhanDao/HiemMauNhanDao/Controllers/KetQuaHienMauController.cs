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
    public class KetQuaHienMauController : Controller
    {
        private DbContextHM db = new DbContextHM();
        KetQuaHienMauServices _kqhm = new KetQuaHienMauServices();

        //Index
        // - chỉ hiện những người dùng đăng ký đợt tổ chức của mình đăng bài
        // VD: bênh viện 1 - đăng bài đợt tổ chức 1 -> thì chỉ hiện những người đk đợt tổ chức 1
        public ActionResult Index(string searchString, int page = 1, int pageSize = 100)
        {
            var dsdk = new KetQuaHienMauServices();
            var model = dsdk.GetListKQHM(searchString, page, pageSize);
            ViewBag.SearchStringDK = searchString;          
            return View(model);
        }

        //Create 1
        // - người đầu tiên sẽ có nhiệm vụ tạo kết quả hiến máu cho người đăng ký
        // - nhập các nội dung ở mục : Khám lâm sàng
        // - các trường dữ liệu tạo ở mục Khám lâm sàn: idPDKHM, trangThai,ghiChu,LayMau,luongMauHien,hienMau,
        //                                              tinhTrangLS,huyetAp,machMau,canNang,nguoiKham(lấy từ list nhân viên y tế "idNVYT")
        // ở chỗ này có thể bị bug thuộc tính idNVYT (chú ý)
        public ActionResult Create()
        {
            ViewBag.idPDKHM = new SelectList(db.PhieuDKHMs, "idPDKHM", "idDTCHM");
            return View();
        }     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdKQHM,idPDKHM,nhomMau,nguoiKham,nguoiXN,nguoiLayMau,canNang,machMau,tinhTrangLS,huyetAp,luongMauHien,hienMau,noiDung,HST,HBV,MSD,phanUng,thoiGianLayMau,ghiChu,trangThai")] KetQuaHienMau ketQuaHienMau)
        {
            if (ModelState.IsValid)
            {
                db.KetQuaHienMaus.Add(ketQuaHienMau);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idPDKHM = new SelectList(db.PhieuDKHMs, "idPDKHM", "idDTCHM", ketQuaHienMau.idPDKHM);
            return View(ketQuaHienMau);
        }

        

        //Edit 
        // - 2 người có nhiện vụ còn lại sẽ cập nhật
        // - các trường dữ liệu tạo ở mục Lấy máu,Xét nghiệm trước HM:
        //  Xét nghiệm: HST,HBV,trangThai,nguoiXN
        // Lấy máu: trangThai,nhomMau,noiDung,phanUng,thoiGianLayMau,MSD, nguoiLayMau
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdKQHM,idPDKHM,nhomMau,nguoiKham,nguoiXN,nguoiLayMau,canNang,machMau,tinhTrangLS,huyetAp,luongMauHien,hienMau,noiDung,HST,HBV,MSD,phanUng,thoiGianLayMau,ghiChu,trangThai")] KetQuaHienMau ketQuaHienMau)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ketQuaHienMau).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idPDKHM = new SelectList(db.PhieuDKHMs, "idPDKHM", "idDTCHM", ketQuaHienMau.idPDKHM);
            return View(ketQuaHienMau);
        }


        public ActionResult Details(string id)
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