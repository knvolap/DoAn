using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiemMauNhanDao.Areas.Client.Controllers
{
    public class DotHienMauClientController : Controller
    {
        // GET: Client/DotHienMau
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