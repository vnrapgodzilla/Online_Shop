using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Online_Shop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var model = new ProductCategoryDao().ListAll();
            return PartialView(model);
        }

        public ActionResult Category(long CateID, int page = 1, int pageSize = 1)
        {
            var cate = new CategoryDao().ViewDetail(CateID);
            ViewBag.Category = cate;
            int totalRecord = 0;
            var model = new ProductDao().ListByCategoryID(CateID, ref totalRecord, page, pageSize);

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
            return View(model);
        }

        public ActionResult Search(string keyword, int page = 1, int pageSize = 1)
        {
            int totalRecord = 0;
            var model = new ProductDao().Search(keyword, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            ViewBag.KeyWord = keyword;

            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)totalRecord / pageSize);
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Previous = page - 1;
            return View(model);
        }

        [OutputCache(CacheProfile = "Cache1DayForProduct")]
        public ActionResult Detail(long id)
        {
            var product = new ProductDao().ViewDetail(id);
            ViewBag.Category = new ProductCategoryDao().ViewDetail(product.CategoryID.Value);
            ViewBag.RelatedProducts = new ProductDao().ListRelatedProduct(id);
            ViewBag.Title = product.Name;
            ViewBag.Keywords = product.MetaKeywords;
            ViewBag.Descriptions = product.MetaDescriptions;
            var images = product.MoreImages;
            if (images != null)
            {
                XElement xImages = XElement.Parse(images);

                List<string> listImages = new List<string>();
                foreach (XElement element in xImages.Elements())
                {
                    listImages.Add(element.Value);
                }
                ViewBag.ListImages = listImages;
            }
            return View(product);
        }

        public JsonResult ListName(string term)
        {
            var data = new ProductDao().ListName(term);
            return Json(new
            {
                data = data,
                response = true
            }, JsonRequestBehavior.AllowGet);
        }
    }
}