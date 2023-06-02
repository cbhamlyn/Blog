using Admin.Models.Domain;

namespace Admin.Repositories
{
    public interface ICategoryRepository
    {
        // Get all Categories.
        Task<IEnumerable<Category>> GetAllAsync();
        //Get specific Category (using Id).
        Task<Category?> GetIdAsync(Guid id);
        // Get specific Category (using Title).
        Task<Category> GetTitleAsync(string title);
        // Add new Category.
        Task<Category> AddAsync(Category category);
        // Edit existing Category.
        Task<Category?> EditAsync(Category category);

    }
}
