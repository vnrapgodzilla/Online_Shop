using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ViewModel;
using Model.Common;
using Common;
using System.Data.Entity;

namespace Model.Dao
{
    public class ContentDao
    {
        Online_ShopDbContext db = null;

        public ContentDao()
        {
            db = new Online_ShopDbContext();
        }

        public Content GetByID(long id)
        {
            return db.Contents.Find(id);
        }

        public long Insert(Content entity)
        {
            //Xử lý Alias
            if (string.IsNullOrEmpty(entity.MetaTitle))
            {
                entity.MetaTitle = StringHelper.ConvertToUnSign(entity.Name);
            }
            db.Contents.Add(entity);
            db.SaveChanges();

            //Xử lý tag
            if (!string.IsNullOrEmpty(entity.Tags))
            {
                string[] tags = entity.Tags.Split(',');
                foreach (var tag in tags)
                {
                    var tagID = StringHelper.ConvertToUnSign(tag);
                    var existedTag = CheckTag(tagID);
                    //Thêm vào Tag table
                    if (!existedTag)
                    {
                        InsertTag(tagID, tag);
                    }
                    //Thêm vào ContentTag table
                    InsertContentTag(entity.ID, tagID);
                }
            }
            return entity.ID;
        }

        public void InsertTag(string id, string name)
        {
            var tag = new Tag();
            tag.ID = id;
            tag.Name = name;
            db.Tags.Add(tag);
            db.SaveChanges();
        }

        public void InsertContentTag(long contentID, string tagID)
        {
            var contentTag = new ContentTag();
            contentTag.ContentID = contentID;
            contentTag.TagID = tagID;
            db.ContentTags.Add(contentTag);
            db.SaveChanges();
        }

        public bool CheckTag(string id)
        {
            return db.Tags.Count(x => x.ID == id) > 0;
        }

        public IEnumerable<ContentViewModel> ListAllPaging(string searchString, int page, int pageSize)
        {
            //IQueryable<Content> model = db.Contents;

            IQueryable<ContentViewModel> model = from a in db.Contents
                                                 join b in db.Categories
                                                 on a.CategoryID equals b.ID
                                                 select new ContentViewModel
                                                 {
                                                     ID = a.ID,
                                                     CateName = b.Name,
                                                     ContentName = a.Name,
                                                     MetaTitle = a.MetaTitle,
                                                     CreatedDate = a.CreatedDate,
                                                     Status = a.Status
                                                 };
            if (!string.IsNullOrEmpty(searchString))
            {
                var search = ConvertToUnsign.ConvertToUnSign(searchString);
                model = model.Where(x => x.ContentName.Contains(search) || x.MetaTitle.Contains(search)
                                    || x.ContentName.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public IEnumerable<Content> ListAllPaging(int page, int pageSize)
        {
            IQueryable<Content> model = db.Contents;
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public bool Update(Content entity)
        {
            try
            {
                var content = db.Contents.Find(entity.ID);
                content.Name = entity.Name;
                //Xử lý Alias
                if (string.IsNullOrEmpty(entity.MetaTitle))
                {
                    entity.MetaTitle = StringHelper.ConvertToUnSign(entity.Name);
                }
                content.MetaTitle = entity.MetaTitle;
                content.Description = entity.Description;
                content.Image = entity.Image;
                content.CategoryID = entity.CategoryID;
                content.Detail = entity.Detail;
                content.Warranty = entity.Warranty;
                content.MetaKeywords = entity.MetaKeywords;
                content.MetaDescriptions = entity.MetaDescriptions;
                content.TopHot = entity.TopHot;
                //content.ViewCount = entity.ViewCount;
                //Xử lý tag
                if (!string.IsNullOrEmpty(entity.Tags))
                {
                    RemoveAllContentTag(entity.ID);
                    string[] tags = entity.Tags.Split(',');
                    foreach (var tag in tags)
                    {
                        var tagID = StringHelper.ConvertToUnSign(tag);
                        var existedTag = CheckTag(tagID);
                        //Thêm vào Tag table
                        if (!existedTag)
                        {
                            InsertTag(tagID, tag);
                        }
                        //Thêm vào ContentTag table
                        InsertContentTag(entity.ID, tagID);
                    }
                }
                content.Tags = entity.Tags;
                content.ModifiedBy = entity.ModifiedBy;
                content.ModifiedDate = DateTime.Now;
                content.Status = entity.Status;
                content.Language = entity.Language;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void RemoveAllContentTag(long contentID)
        {
            db.ContentTags.RemoveRange(db.ContentTags.Where(x => x.ContentID == contentID));
            db.SaveChanges();
        }

        public bool Delete(long id)
        {
            try
            {
                var content = db.Contents.Find(id);
                db.Contents.Remove(content);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Tag> ListTag(long contentID)
        {
            var model = (from a in db.Tags
                         join b in db.ContentTags
                         on a.ID equals b.TagID
                         where b.ContentID == contentID
                         select new
                         {
                             ID = b.TagID,
                             Name = a.Name
                         }).AsEnumerable().Select(x => new Tag
                         {
                             ID = x.ID,
                             Name = x.Name
                         });
            return model.ToList();
        }

        public List<Content> ListAllByTag(string tag, ref int totalRecord, int pageIndex, int pageSize)
        {
            var model = (from a in db.Contents
                         join b in db.ContentTags
                         on a.ID equals b.ContentID
                         where b.TagID == tag
                         select new 
                         {
                             ID = a.ID,
                             Name = a.Name,
                             MetaTitle = a.MetaTitle,
                             Image = a.Image,
                             Description = a.Description,
                             CreatedDate = a.CreatedDate,
                             CreatedBy = a.CreatedBy
                         }
                         ).AsEnumerable().Select(x => new Content
                         {
                             ID = x.ID,
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Image = x.Image,
                             Description = x.Description,
                             CreatedDate = x.CreatedDate,
                             CreatedBy = x.CreatedBy
                         });
            totalRecord = model.Select(x => x.ID).Count();
            return model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public Tag GetTag(string id)
        {
            return db.Tags.Find(id);
        }
    }
}
