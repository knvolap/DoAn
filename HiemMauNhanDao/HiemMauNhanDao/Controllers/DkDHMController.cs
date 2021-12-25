using Models.EF;
using Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiemMauNhanDao.Controllers
{
    public class DkDHMController : Controller
    {
        DKDHMServices _DkDHM = new DKDHMServices();
        // GET: DotTCHM
        public ActionResult Index(DotHienMau dotHienMau, string searchString, int page = 1, int pageSize = 5)
        {
            var hienthiDHM = new DKDHMServices();
            var model = hienthiDHM.ListAllDKDHM2(searchString, page, pageSize);
            ViewBag.BenhVien = new DKDHMServices().ListAllLLeftMenuBV();
            ViewBag.SearchString = searchString;
            ViewBag.listDHM = hienthiDHM.GetDotHienMaus();     
            return View(model);
        }
        
        public ActionResult DKTC()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult _RightMenu()
        {
            var model = new TCDHMServices().ListAllLLeftMenuBV();
            return PartialView(model);
        }
    }
}