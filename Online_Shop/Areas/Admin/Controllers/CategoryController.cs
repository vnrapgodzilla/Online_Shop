using BotDetect;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StaticResources;
using Online_Shop.Common;

namespace Online_Shop.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Index(string searchString, int page = 1, int pageSize = 2)
        {
            var dao = new CategoryDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.CurrentCulture = Session[CommonConstants.CurrentCulture].ToString();
            return View();
        }

        public ActionResult Edit(long id)
        {
            var dao = new CategoryDao();
            var category = dao.GetByID(id);
            ViewBag.CurrentCulture = Session[CommonConstants.CurrentCulture].ToString();
            return View(category);
        }

        [HttpPost]
        public ActionResult Create(Category model)
        {
            if (ModelState.IsValid)
            {
                var currentCulture = Session[CommonConstants.CurrentCulture];
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.CreatedBy = session.UserName;
                model.CreatedDate = DateTime.Now;
                model.Language = currentCulture.ToString();
                var id = new CategoryDao().Insert(model);
                if (id > 0)
                {
                    SetAlert("Thêm mới danh mục thành công", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", Resources.InsertCategoryFailed);
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDao();
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.ModifiedBy = session.UserName;
                var culture = Session[CommonConstants.CurrentCulture];
                model.Language = culture.ToString();
                var result = dao.Update(model);
                if (result)
                {
                    SetAlert("Cập nhật danh mục thành công", "success");
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật danh mục thất bại");
                }
            }
            return View("Edit");
        }

        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new CategoryDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}