using ClosedXML.Excel;
using HiemMauNhanDao.Areas.Admin.Controllers;
using Models.EF;
using Models.Services;
using Models.ViewModel;
using OfficeOpenXml;
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
        public ActionResult Index(string searchString ,string id ,int page = 1, int pageSize = 100)
        {
            var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];
            var tempNVYT = db.NhanVienYTes.Where(x => x.idTTCN == session.UserID).FirstOrDefault();

            var dsdk = new KetQuaHienMauServices();
            var model = dsdk.GetListKQHM(searchString, tempNVYT.idBenhVien, tempNVYT.IdNVYT, page, pageSize);
            ViewBag.SearchStringDK = searchString;          
            return View(model);
        }
        public ActionResult DanhSachKQ(string searchString,int page = 1, int pageSize = 100)
        {
            var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];
            var tempNVYT = db.NhanVienYTes.Where(x => x.idTTCN == session.UserID).FirstOrDefault();

            var dsdk = new KetQuaHienMauServices();
            var model = dsdk.GetListKQHM2(searchString, tempNVYT.idBenhVien, tempNVYT.IdNVYT, page, pageSize);
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
            
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdKQHM,idPDKHM,nhomMau,idnguoiKham,nguoiXN,nguoiLayMau,canNang,machMau,tinhTrangLS," +
                                                    "huyetAp,luongMauHien,hienMau,noiDung,HST,HBV,MSD,phanUng,thoiGianLayMau,ghiChu," +
                                                    "nguoiKham,trangThai")] KetQuaHienMau ketQuaHienMau, string id)
        {
            var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];
            var tempNVYT = db.NhanVienYTes.Where(x => x.idTTCN == session.UserID).SingleOrDefault();

            string ids = db.KetQuaHienMaus.Max(x => x.IdKQHM);
            string phanDau = ids.Substring(0, 2);
            int so = Convert.ToInt32(ids.Substring(2)) + 1;

            //string ids = db.KetQuaHienMaus.Max(x => x.IdKQHM);
            //int stt = Convert.ToInt32(ids.Substring(2)) + 1;

            if (ModelState.IsValid==false  )
            {
                ketQuaHienMau.IdKQHM = so > 9 ? phanDau + so : phanDau + "0" + so;
                ketQuaHienMau.idPDKHM = id;
                ketQuaHienMau.idnguoiKham = tempNVYT.IdNVYT;
                ketQuaHienMau.nguoiKham = session.Name;
                ketQuaHienMau.tgKham = DateTime.Now;
                ketQuaHienMau.trangThai = "Đang cập nhật";
                db.KetQuaHienMaus.Add(ketQuaHienMau);
                db.SaveChanges();
                SetAlert("Cập nhật thành công ", "success");
                return RedirectToAction("Index", "KetQuaHienMau");
            }
            else
            {
                SetAlert("Vui lòng cập nhập thất bại.Vui lòng kiểm tra trường dữ liệu ", "error");
                return RedirectToAction("Create", "KetQuaHienMau");
            }    
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
                                                   "hienMau, noiDung, HST, HBV,MSD, phanUng,ghiChu,trangThai,nguoiXN,nguoiKham,nguoiLayMau," )] KetQuaHienMau ketQuaHienMau, string id)
        {
            var session2 = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];
            var tempNVYT2 = db.NhanVienYTes.Where(x => x.idTTCN == session2.UserID).SingleOrDefault();

            if (ModelState.IsValid  )
            {
                var kqhm = _kqhm.GetByIdKQHM(ketQuaHienMau.IdKQHM);

                ketQuaHienMau.idnguoiXN = tempNVYT2.IdNVYT;         
                ketQuaHienMau.tgXetNghiem = DateTime.Now;
                ketQuaHienMau.tgCapNhat = DateTime.Now;
                ketQuaHienMau.nguoiXN = session2.Name;

                kqhm.IdKQHM = kqhm.IdKQHM;
                kqhm.idPDKHM = kqhm.idPDKHM;
                kqhm.idnguoiKham = kqhm.idPDKHM;
                kqhm.nguoiKham = kqhm.nguoiKham;
                kqhm.idnguoiLayMau = kqhm.idnguoiLayMau;
                kqhm.nguoiLayMau = kqhm.nguoiLayMau;
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
                return RedirectToAction("DanhSachKQ", "KetQuaHienMau");

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
        public ActionResult layMau([Bind(Include = "IdKQHM,idPDKHM, idnguoiKham,idnguoiXN,idnguoiLayMau,tgKham,tgXetNghiem," +
                                                  "tgLayMau,tgCapNhat,nhomMau,canNang,machMau,tinhTrangLS,huyetAp,luongMauHien," +
                                                   "hienMau, noiDung, HST, HBV,MSD, phanUng,ghiChu,trangThai,nguoiXN,nguoiKham,nguoiLayMau," )] KetQuaHienMau ketQuaHienMau, string id)
        {
            var session3 = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];
            var tempNVYT3 = db.NhanVienYTes.Where(x => x.idTTCN == session3.UserID).SingleOrDefault();

            if (ModelState.IsValid )
            {
                var kqhm = _kqhm.GetByIdKQHM(ketQuaHienMau.IdKQHM);

                ketQuaHienMau.idnguoiLayMau = tempNVYT3.IdNVYT;
                ketQuaHienMau.nguoiLayMau = session3.Name;
                ketQuaHienMau.IdKQHM = ketQuaHienMau.IdKQHM;
                ketQuaHienMau.tgCapNhat = DateTime.Now;

                kqhm.IdKQHM = kqhm.IdKQHM;
                kqhm.idPDKHM = kqhm.idPDKHM;
                kqhm.idnguoiKham = kqhm.idPDKHM;
                kqhm.nguoiKham = kqhm.nguoiKham;
                kqhm.idnguoiXN = kqhm.idnguoiXN;
                kqhm.nguoiXN = kqhm.nguoiXN;
                kqhm.tgKham = kqhm.tgKham;
                kqhm.tgXetNghiem = kqhm.tgXetNghiem;
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
                return RedirectToAction("DanhSachKQ", "KetQuaHienMau");
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

        public ActionResult ExporExcel()
        {
            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("KetQuaHM");

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public void xuatFileExcel()
        {
            List<KetQuaHienMau> dskq = db.KetQuaHienMaus.Select(x => new KetQuaHienMau
            {
                idPDKHM = x.idPDKHM,
                IdKQHM =x.IdKQHM,             
                luongMauHien=x.luongMauHien,
                nhomMau=x.nhomMau,
                trangThai=x.trangThai,
                tgCapNhat=x.tgCapNhat,
                nguoiLayMau=x.nguoiLayMau,
            }).ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Reoprt");

            ws.Cells["A1"].Value = "Kết quả hiến máu";
            ws.Cells["B1"].Value = "Đợt 1";
            ws.Cells["A2"].Value = "Report";
            ws.Cells["B2"].Value = "Trang 1";
            ws.Cells["A3"].Value = "Thời gian tạo";
            ws.Cells["B3"].Value = string.Format("{0:dd/mm/yyyy} at {0:H:mm}",DateTimeOffset.Now);
            
            ws.Cells["A6"].Value = "Mã đăng ký";
            ws.Cells["B6"].Value = "Mã kết quả";
            ws.Cells["C6"].Value = "lượng máu  ";
            ws.Cells["D6"].Value = "nhóm máu";
            ws.Cells["E6"].Value = "trạng thái";
            ws.Cells["F6"].Value = "thời gian cập nhật";
            ws.Cells["G6"].Value = "người lấy máu";

            int rowStar = 8;
            foreach(var item in dskq)
            {
                ws.Row(rowStar).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
              
            }

        }

    }
}