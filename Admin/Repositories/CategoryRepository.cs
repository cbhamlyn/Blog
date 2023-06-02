using Admin.Data;
using Admin.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Admin.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BlogDbContext blogDbContext;

        public CategoryRepository(BlogDbContext blogDbContext)
        {
            this.blogDbContext = blogDbContext;
        }

        public async Task<Category> AddAsync(Category category)
        {
            // Use mapped Request to add data to table.
            await blogDbContext.Categories.AddAsync(category);
            await blogDbContext.SaveChangesAsync();

            return category;
        }

        public async Task<Category?> EditAsync(Category category)
        {
            var existingCategory = await blogDbContext.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);

            if (existingCategory != null)
            {
                existingCategory.Title = category.Title;
                await blogDbContext.SaveChangesAsync();

                return existingCategory;
            }
            return null;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await blogDbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetIdAsync(Guid id)
        {
            return await blogDbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);   
        }

        public async Task<Category> GetTitleAsync(string title)
        {
            throw new NotImplementedException();
        }
    }
}
