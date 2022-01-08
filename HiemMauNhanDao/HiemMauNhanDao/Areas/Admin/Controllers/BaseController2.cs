
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
    public class BaseController2 : Controller
    {
        // GET: Admin/Base
        /*check đăng nhập thì phải login*/
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[CommonConstant.USER_SESSION];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "LoginClient", action = "Index" }));
            }
            base.OnActionExecuting(filterContext);
        }

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