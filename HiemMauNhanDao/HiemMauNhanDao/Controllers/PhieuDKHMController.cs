using HiemMauNhanDao.Areas.Admin.Controllers;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HiemMauNhanDao.Controllers
{
    public class PhieuDKHMController : BaseController3
    {
        private DbContextHM db = new DbContextHM();

        public ActionResult Index()
        {
            var phieuDKHMs = db.PhieuDKHMs.Include(p => p.DotToChucHM).Include(p => p.ThongTinCaNhan);
            return View(phieuDKHMs.ToList());
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuDKHM phieuDKHM = db.PhieuDKHMs.Find(id);
            if (phieuDKHM == null)
            {
                return HttpNotFound();
            }
            return View(phieuDKHM);
        }


        // get được idTTCN, nhưng không phải show ra drop list để chọn mà show ra đúng  idTTCN của mình luôn
        // get được idDTCHM, nhưng không phải show ra drop list để chọn mà show ra đúng  idDTCHM khi ấn vào luôn
        // chưa hết hạn thì vẫn có thể sửa
        // phải tích đủ các nội dung
        // khi đăng ký xong trả về thông báo : stt , idPDKHM, thời gian dự kiến = (thời gian tổ chức + 10' cho 5 người đk ) 
        public ActionResult Create(string id)
        {           
            return View();
        }       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idPDKHM,idDTCHM, idTTCN, benhKhac,trangThai,tgDuKien,sutCan, noiHach ,chamCu, xamMinh," +
                                                   "duocTruyenMau,suDungMatuy, NguyCoHIV , QHTD, tiemVacXin, dungTKS ,biSot, dTTT, dangMangThai," +
                                                   "ungThu,hienMau, xacNhan")] PhieuDKHM phieuDKHM, string id)
        {
            var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];
            var tempDTCHM = db.DotToChucHMs.Where(x => x.IdDTCHM == id).SingleOrDefault();
            string idpdk = db.PhieuDKHMs.Max(x => x.idPDKHM);
            int stt = Convert.ToInt32(idpdk.Substring(3)) + 1;
         
            if (ModelState.IsValid ==  false)
            {
                var tempTTCN = db.PhieuDKHMs.Where(x => x.idTTCN == session.UserID).FirstOrDefault();             
                phieuDKHM.idPDKHM = stt > 9 ? "PDK" + stt : "PDK0" + stt;
                phieuDKHM.idTTCN = tempTTCN.idTTCN;
                phieuDKHM.idDTCHM = id;            
                phieuDKHM.trangThai = false;
                phieuDKHM.tgDuKien = tempDTCHM.ngayToChuc;
             
                db.PhieuDKHMs.Add(phieuDKHM);
                db.SaveChanges();
                SetAlert("Đăng ký thành công ", "success");
                return RedirectToAction("Index", "DotToChucHM");  
            }
            else
            {
                SetAlert("Đăng ký thất bại ", "error");
                return RedirectToAction("Create");
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