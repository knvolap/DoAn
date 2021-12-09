using HiemMauNhanDao.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiemMauNhanDao.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }

        //Đăng xuất   
        public ActionResult Logout()
        {
            Session[CommonConstant.USER_SESSION] = null;
            return RedirectToAction("Index", "Login");
        }
    }


}