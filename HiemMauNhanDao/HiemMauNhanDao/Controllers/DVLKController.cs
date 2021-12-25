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
    public class DVLKController : BaseController
    {
        DonViLienKetServices _dvlk = new DonViLienKetServices();
        public UserLogin userSession = new UserLogin();
        private DbContextHM db = new DbContextHM();
        // GET: DVLK
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DKDVLK( )
        {
            var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];
            string id = session.UserID;
            DonViLienKet IdTTCN = db.DonViLienKets.Find(id);
            return View(IdTTCN);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DKDVLK(DonViLienKet donViLienKet)
        {
            var session = (HiemMauNhanDao.Common.UserLogin)Session[HiemMauNhanDao.Common.CommonConstant.USER_SESSION];
            string id = session.UserID;
             donViLienKet = db.DonViLienKets.Find(id);

            if (ModelState.IsValid)
            {
                donViLienKet.idTTCN = id;
                _dvlk.AddDVLK(donViLienKet);
                SetAlert("Đăng ký thành công", "success");
                return RedirectToAction("DKDVLK", "DVLK");
            }
            else
            {
                SetAlert("Đăng ký  thất bại", "error");
                return RedirectToAction("DKDVLK", "DVLK");
            }
            return View(donViLienKet);
        }


        public ActionResult DangBai()
        {
            return View();
        }

        public ActionResult CapNhatTTDV()
        {
            return View();
        }
    }
}