
using HiemMauNhanDao.Common;
using HiemMauNhanDao.Models;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HiemMauNhanDao.Areas.Admin.Controllers
{
    public class BaseController3 : Controller
    {
        

        // GET: Admin/CheckErro

        protected void SetAlert(String message, String type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else
            if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else
            if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
    }
}