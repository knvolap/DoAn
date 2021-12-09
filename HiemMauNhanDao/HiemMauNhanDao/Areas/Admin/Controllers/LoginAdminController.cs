﻿using HiemMauNhanDao.Common;
using HiemMauNhanDao.Models;
using HiemMauNhanDao.Extensions;
using Models;
using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HiemMauNhanDao.Areas.Admin.Controllers
{
    public class LoginAdminController : Controller
    {

        //SESSSION
        public UserLogin userSession = new UserLogin();

        // GET: Admin/LoginAdmin
        public ActionResult Index()
        {
            return View();
        }


        //POSST
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Đăng nhập
        public ActionResult Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (model.UserName == null)
                {
                    ModelState.AddModelError("", "Vui lòng không bỏ trống tài khoản!");
                }
                else if (model.UserPassword == null)
                {
                    ModelState.AddModelError("", "Vui lòng không bỏ trống Mật khẩu!");
                }
                else
                {
                    var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.UserPassword));
                    if (result == 1)
                    {
                        var user = dao.GetById(model.UserName);
                        userSession.AuthorID = user.idQuyen;
                        userSession.Accounts = user.userName;
                        Session.Add(CommonConstant.USER_SESSION, userSession);
                        return RedirectToAction("Index", "Home");
                    }
                    else if (result == 0)
                    {
                        ModelState.AddModelError("", "Email hoặc mật khẩu không chính xác!");
                    }               
                }
            }
            return View("Index");
        }

        //Đăng xuất
        public ActionResult Logout()
        {
            Session[CommonConstant.USER_SESSION] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}