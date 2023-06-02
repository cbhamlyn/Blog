using Admin.Models.Domain;

namespace Admin.Repositories
{
    public interface IPostRepository
    {
        // Get all Posts.
        Task<IEnumerable<BlogPost>> GetAllAsync();
        //Get specific Post (using Id).
        Task<BlogPost?> GetIdAsync(Guid id);
        // Add new Post.
        Task<BlogPost> AddAsync(BlogPost blogPost);
        // Edit existing Post.
        Task<BlogPost?> EditAsync(BlogPost blogPost);
    }
}
