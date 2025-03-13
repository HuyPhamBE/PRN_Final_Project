using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Model.Blog
{
    public class BlogServiceModel
    {
        public Guid BlogID { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string imageUrl { get; set; }
    }
}
