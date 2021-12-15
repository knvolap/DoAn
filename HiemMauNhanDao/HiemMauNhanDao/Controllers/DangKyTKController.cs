using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiemMauNhanDao.Controllers
{
    public class DangKyTKController : Controller
    {
        // GET: DangKyTK
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NguoiDung()
        {
            return View();
        }
        public ActionResult DonVi()
        {
            return View();
        }

        public ActionResult BenhVien()
        {
            return View();
        }
    }
}