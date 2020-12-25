using Model.EF;
using System.Linq;

namespace Model.Dao
{
    public class ContactDao
    {
        Online_ShopDbContext db = null;

        public ContactDao()
        {
            db = new Online_ShopDbContext();
        }

        public Contact GetActiveContact()
        {
            return db.Contacts.Single(x => x.Status == true);
        }

        public int InsertFeedback(Feedback fb)
        {
            db.Feedbacks.Add(fb);
            db.SaveChanges();
            return fb.ID;
        }
    }
}