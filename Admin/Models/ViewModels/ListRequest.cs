using Admin.Models.Domain;
using Admin.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Admin.Models.ViewModels
{
    public class ListRequest
    {
        public List<Category> Categories { get; set; }
        public IEnumerable<BlogPost> Posts { get; set; }

        public ListRequest()
        {
            this.Categories = new List<Category>();
            this.Posts = new List<BlogPost>();
        }
    }
}   
