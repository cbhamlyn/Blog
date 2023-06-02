using Admin.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Admin.Models.ViewModels
{
    public class EditPostRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid CategoryId { get; set; }
        public IEnumerable<SelectListItem> CategoriesList { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Content { get; set; }
    }
}
