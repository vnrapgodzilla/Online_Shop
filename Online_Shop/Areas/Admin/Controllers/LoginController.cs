using Model.Dao;
using Online_Shop.Areas.Admin.Models;
using Online_Shop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Shop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password), true);
                switch(result)
                {
                    case 1:
                        var user = dao.GetByID(model.UserName);
                        var userSession = new UserLogin();
                        userSession.UserName = user.UserName;
                        userSession.UserID = user.ID;
                        userSession.GroupID = user.GroupID;
                        var listCredentials = dao.GetListCredential(model.UserName);
                        Session.Add(CommonConstants.SESSION_CREDENTIALS, listCredentials);
                        Session.Add(CommonConstants.USER_SESSION, userSession);
                        return RedirectToAction("Index", "Home");

                    case 0:
                        ModelState.AddModelError("", "Tài khoản không tồn tại");
                        break;

                    case -1:
                        ModelState.AddModelError("", "Tài khoản đang bị khóa");
                        break;

                    case -2:
                        ModelState.AddModelError("", "Sai mật khẩu");
                        break;
                    case -3:
                        ModelState.AddModelError("", "Tài khoản của bạn không có quyền đăng nhập");
                        break;
                }
            }
            return View("Index");
        }
    }
}