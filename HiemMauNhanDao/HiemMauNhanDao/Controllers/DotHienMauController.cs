using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiemMauNhanDao.Controllers
{
    public class DotHienMauController : Controller
    {
        // GET: DotHienMau
        public ActionResult Index()
        {
            return View();
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