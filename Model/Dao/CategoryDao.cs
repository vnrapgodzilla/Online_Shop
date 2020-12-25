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
    public class CategoryDao
    {
        Online_ShopDbContext db = null;

        public CategoryDao()
        {
            db = new Online_ShopDbContext();
        }

        public long Insert(Category category)
        {
            //Xử lý Alias
            if (string.IsNullOrEmpty(category.MetaTitle))
            {
                category.MetaTitle = StringHelper.ConvertToUnSign(category.Name);
            }
            db.Categories.Add(category);
            db.SaveChanges();
            return category.ID;
        }

        public bool Update(Category entity)
        {
            try
            {
                var category = db.Categories.Find(entity.ID);
                category.Name = entity.Name;
                //Xử lý Alias
                if (string.IsNullOrEmpty(entity.MetaTitle))
                {
                    entity.MetaTitle = StringHelper.ConvertToUnSign(entity.Name);
                }
                category.MetaTitle = entity.MetaTitle;
                category.ParentID = entity.ParentID;
                category.DisplayOrder = entity.DisplayOrder;
                category.SeoTitle = entity.SeoTitle;
                category.ModifiedDate = DateTime.Now;
                category.ModifiedBy = entity.ModifiedBy;
                category.MetaKeywords = entity.MetaKeywords;
                category.MetaDescriptions = entity.MetaDescriptions;
                category.Status = entity.Status;
                category.ShowOnHome = entity.ShowOnHome;
                category.Language = entity.Language;
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public List<Category> ListAll()
        {
            return db.Categories.Where(x => x.Status == true).ToList();
        }

        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategories.Find(id);
        }

        public IEnumerable<Category> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Category> model = db.Categories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.MetaTitle.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public bool ChangeStatus(long id)
        {
            var category = db.Categories.Find(id);
            category.Status = !category.Status;
            db.SaveChanges();
            return category.Status;
        }

        public Category GetByID(long id)
        {
            return db.Categories.Find(id);
        }
    }
}
