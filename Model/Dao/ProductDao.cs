using Common;
using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductDao
    {
        Online_ShopDbContext db = null;
        public ProductDao()
        {
            db = new Online_ShopDbContext();
        }

        public long Insert(Product entity)
        {
            //Xử lý Alias
            if (string.IsNullOrEmpty(entity.MetaTitle))
            {
                entity.MetaTitle = StringHelper.ConvertToUnSign(entity.Name);
            }
            db.Products.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

            /// <summary>
            /// Get list product by category
            /// </summary>
            /// <param name="CateID"></param>
            /// <returns></returns>
            public List<Product> ListByCategoryID(long CateID, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.Products.Where(x => x.CategoryID == CateID).Count();
            return db.Products.Where(x => x.CategoryID == CateID).OrderByDescending(x => x.CreatedDate).
                   Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<Product> Search(string keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.Products.Where(x => x.Name.Contains(keyword)).Count();
            return db.Products.Where(x => x.Name.Contains(keyword)).OrderByDescending(x => x.CreatedDate).
                   Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<Product> ListNewProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }

        /// <summary>
        /// List feature product
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<Product> ListFeatureProduct(int top)
        {
            return db.Products.Where(x => x.TopHot != null && x.TopHot > DateTime.Now).
                   OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }

        public List<Product> ListRelatedProduct(long ProductID)
        {
            var product = db.Products.Find(ProductID);
            return db.Products.Where(x => x.ID != ProductID && x.CategoryID == product.CategoryID).ToList();
        }

        public Product ViewDetail(long id)
        {
            return db.Products.Find(id);
        }

        public List<string>ListName(string keyword)
        {
            return db.Products.Where(x => x.Name.Contains(keyword)).Select(x => x.Name).ToList();
        }

        public IEnumerable<Product> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            { 
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public bool Delete(long id)
        {
            try
            {
                var product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void UpdateImages(long productID, string images)
        {
            var product = db.Products.Find(productID);
            product.MoreImages = images;
            db.SaveChanges();
        }
    }
}
