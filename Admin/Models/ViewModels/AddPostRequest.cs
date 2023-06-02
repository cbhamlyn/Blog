using Microsoft.AspNetCore.Mvc.Rendering;

namespace Admin.Models.ViewModels
{
    public class AddPostRequest
    {
        public string AddPostTitleInput { get; set; }
        public Guid AddPostCategoryInput { get; set; }
        public IEnumerable<SelectListItem> CategoriesList { get; set; }
        public DateTime AddPostDateInput { get; set; }
        public string AddPostContentInput { get; set; }

    }
}
