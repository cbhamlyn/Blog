using Admin.Data;
using Admin.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Admin.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly BlogDbContext blogDbContext;

        public PostRepository(BlogDbContext blogDbContext)
        {
            this.blogDbContext = blogDbContext;
        }

        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await blogDbContext.BlogPosts.AddAsync(blogPost);
            await blogDbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost> EditAsync(BlogPost blogPost)
        {
            var existingPost = await blogDbContext.BlogPosts.FirstOrDefaultAsync(b => b.Id == blogPost.Id);

            if (existingPost != null)
            {
                existingPost.Title = blogPost.Title;
                existingPost.CategoryId = blogPost.CategoryId;
                existingPost.PublicationDate = blogPost.PublicationDate;
                existingPost.Content = blogPost.Content;
                await blogDbContext.SaveChangesAsync();

                return existingPost;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await blogDbContext.BlogPosts.ToListAsync();
        }

        public async Task<BlogPost?> GetIdAsync(Guid id)
        {
            return await blogDbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}