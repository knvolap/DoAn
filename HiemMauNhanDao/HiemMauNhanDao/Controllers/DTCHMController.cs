using Models.EF;
using Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiemMauNhanDao.Controllers
{
    public class DTCHMController : Controller
    {
        TCDHMServices _DkDHM = new TCDHMServices();
        // GET: DotHienMau
        public ActionResult Index(DotToChucHM dotToChucHM, string searchString, int page = 1, int pageSize = 5)
        {
            var hienthiDTCHM = new TCDHMServices();
            var model = hienthiDTCHM.ListAlldTCHM(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            ViewBag.listDTCHM = hienthiDTCHM.GetListDTCHM();
            return View(model);
        }


        public ActionResult DangKyHiemMau()
        {
            return View();
        }


        public ActionResult ChiTietDKHM()
        {
            return View();
        }

     

    }
}