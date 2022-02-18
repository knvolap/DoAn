using ClosedXML.Excel;
using HiemMauNhanDao.Areas.Admin.Controllers;
using Models.EF;
using Models.Services;
using Models.ViewModel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
        NguoiDungServices _ngDung = new NguoiDungServices();
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


            if (ModelState.IsValid==false  )
            {
                if (ketQuaHienMau.canNang == null)
                {
                    SetAlert("Vui lòng nhập cân nặng", "error");
                }
                else if (ketQuaHienMau.machMau == null)
                {
                    SetAlert("Vui lòng nhập mạch máu", "error");
                }
                else if (ketQuaHienMau.huyetAp == null)
                {
                    SetAlert("Vui lòng nhập huyết áp", "error");
                }
                else if (ketQuaHienMau.luongMauHien == null)
                {
                    SetAlert("Vui lòng chọn lượng máu hiến", "error");
                }
                else
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
            }
            else
            {
                SetAlert("Vui lòng cập nhập thất bại.Vui lòng kiểm tra trường dữ liệu ", "error");
                return RedirectToAction("Create", "KetQuaHienMau");
            }
            return View(ketQuaHienMau);
          
        }
     

     
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
                if (ketQuaHienMau.HST == null)
                {
                    SetAlert("Vui lòng nhập kết quả huyết sắc tố", "error");
                }
                else 
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
            }
            else
            {
                SetAlert("Vui lòng cập nhập thất bại.Vui lòng kiểm tra trường dữ liệu ", "error");
                return RedirectToAction("Edit", "KetQuaHienMau");
            }
            return View(ketQuaHienMau);
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
                if (ketQuaHienMau.MSD == null)
                {
                    SetAlert("Vui lòng nhập kết quả hiến máu", "error");
                }
                else
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
            }
            else
            {
                SetAlert("Vui lòng cập nhập thất bại.Vui lòng kiểm tra trường dữ liệu ", "error");
                return RedirectToAction("layMau", "KetQuaHienMau");
            }
            return View(ketQuaHienMau);
        }

        public ActionResult Details(string id)
        {
            var model = _kqhm.GetByIdTTDK2(id);
            return View(model);
        }


        [HttpGet]
        public ActionResult TTDK(string id)
        {
            var model = _kqhm.GetByIdTTDK(id);
            return View(model);
        }
        [HttpGet]
        public ActionResult LSDK(string searchString, string id,int page = 1, int pageSize = 5)
        {           
            var dsdk = new NguoiDungServices();
            var model = dsdk.GetByIdLSDK(searchString, id,page, pageSize);
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


        [HttpGet]
        public ActionResult xuatFileExcel(string searchString, int page = 1, int pageSize = 100)
        {
            var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];
            var tempNVYT = db.NhanVienYTes.Where(x => x.idTTCN == session.UserID).FirstOrDefault();
            var dsdk = new KetQuaHienMauServices();

            var model = dsdk.GetListKQHM2(searchString, tempNVYT.idBenhVien, tempNVYT.IdNVYT, page, pageSize);
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var sheet = package.Workbook.Worksheets.Add("DanhSachHBKK");
                //đỗ dữ liệu vào sheet
                sheet.Cells[1, 1].Value = "Tên đợt tổ chức";
                sheet.Cells[1, 2].Value = "Mã kết quả";
                sheet.Cells[1, 3].Value = "Họ tên";
                sheet.Cells[1, 4].Value = "Giới tính";
                sheet.Cells[1, 5].Value = "Số điện thoại";
                sheet.Cells[1, 6].Value = "Nhóm máu";
                sheet.Cells[1, 7].Value = "Trạng thái";
                sheet.Cells[1, 8].Value = "Người cập nhật";

                int rowInd = 2;
                foreach (var item in model)
                {
                    sheet.Cells[rowInd, 1].Value = item.tenDTCHM;
                    sheet.Cells[rowInd, 2].Value = item.IdKQHM;
                    sheet.Cells[rowInd, 3].Value = item.hoTen;
                    sheet.Cells[rowInd, 4].Value = item.gioiTinh;
                    sheet.Cells[rowInd, 5].Value = item.soDT;
                    sheet.Cells[rowInd, 6].Value = item.nhomMau;
                    sheet.Cells[rowInd, 7].Value = item.trangThai2;
                    sheet.Cells[rowInd, 8].Value = item.nguoiLayMau;
                    rowInd++;
                }
                //save
                package.Save();
            }
            stream.Position = 0;
            var fileName = $"DSHBKK_{DateTime.Now.ToString("ddMMyyyy")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

    }
}