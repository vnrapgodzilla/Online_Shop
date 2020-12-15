using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Shop.Controllers
{
    public class ContentController : Controller
    {
        // GET: Content
        public ActionResult Index(int page = 1, int pageSize = 2)
        {
            var dao = new ContentDao();
            var model = dao.ListAllPaging(page, pageSize);
            return View(model);
        }

        public ActionResult Detail(long id)
        {
            var model = new ContentDao().GetByID(id);
            ViewBag.ListTag = new ContentDao().ListTag(id);
            return View(model);
        }

        public ActionResult Tag(string tagID, int page = 1, int pageSize = 2)
        {
            int totalRecord = 0;
            var model = new ContentDao().ListAllByTag(tagID, ref totalRecord, page, pageSize);
            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)totalRecord / pageSize);
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Previous = page - 1;
            ViewBag.Tag = new ContentDao().GetTag(tagID);
            return View(model);
        }
    }
}