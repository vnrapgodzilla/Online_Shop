using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class ContentViewModel
    {
        public long ID { get; set; }
        public string CateName { get; set; }
        public string ContentName { get; set; }
        public string MetaTitle { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool Status { get; set; }
    }
}
