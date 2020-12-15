using Model.Dao;
using Model.EF;
using Online_Shop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Online_Shop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        [HasCredential(RoleID = "VIEW_USER")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpGet]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create()
        {
            return View();
        }

        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(long id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user);
        }

        [HttpPost]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (!string.IsNullOrEmpty(user.UserName))
                {
                    if (dao.CheckUserName(user.UserName))
                    {
                        if (!string.IsNullOrEmpty(user.Password))
                        {
                            var encryptMd5Pass = Encryptor.MD5Hash(user.Password);
                            user.Password = encryptMd5Pass;
                            user.CreatedDate = DateTime.Now;
                            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                            user.CreatedBy = session.UserName;
                            long id = dao.Insert(user);
                            if (id > 0)
                            {
                                SetAlert("Thêm người dùng thành công", "success");
                                return RedirectToAction("Index", "User");
                            }
                            else
                            {
                                ModelState.AddModelError("", "Thêm người dùng thất bại");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Mật khẩu không được để trống");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Tên đăng nhập này đã tồn tại");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập không được bỏ trống");
                }
            }
            return View("Create");
        }

        [HttpPost]
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (!string.IsNullOrEmpty(user.Password))
                {
                    var encryptMd5Pass = Encryptor.MD5Hash(user.Password);
                    user.Password = encryptMd5Pass;
                }
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                user.ModifiedBy = session.UserName;
                var result = dao.Update(user);
                if (result)
                {
                    SetAlert("Cập nhật người dùng thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật người dùng thất bại");
                }
            }
            return View("Index");
        }

        [HttpDelete]
        [HasCredential(RoleID = "DELETE_USER")]
        public ActionResult Delete(long id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [HasCredential(RoleID = "EDIT_USER")]
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/Admin/Login");
        }
    }
}