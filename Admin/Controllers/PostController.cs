using Admin.Models.Domain;
using Admin.Models.ViewModels;
using Admin.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Admin.Controllers
{
    public class PostController : Controller
    {
         private readonly ICategoryRepository categoryRepository;
        private readonly IPostRepository postRepository;

        public PostController(ICategoryRepository categoryRepository, IPostRepository postRepository)
        {
            this.categoryRepository = categoryRepository;
            this.postRepository = postRepository;
        }

        // This returns the default view "AddPostRequest" when navigated to.
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            // Get Categories listing
            var categoryList = await categoryRepository.GetAllAsync();

            var model = new AddPostRequest
            {
                CategoriesList = categoryList.Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() })
            };
            return View(model);
        }
        // This runs when the "Save" button is pressed on the "Add Post" page is pressed.
        [HttpPost]
        // AddPostRequest is defined in Models\ViewModels\AddPostRequest.cs
        public async Task<IActionResult> Add(AddPostRequest addPostRequest) 
        {
            // Map AddPostRequest to Post domain model.
            var post = new BlogPost
            {
                Title = addPostRequest.AddPostTitleInput,
                CategoryId = addPostRequest.AddPostCategoryInput,
                PublicationDate = addPostRequest.AddPostDateInput,
                Content = addPostRequest.AddPostContentInput
            };

            //TODO: Add error handling for Null or Duplicate Post Title

            // Use mapped Request to add data to table.
            await postRepository.AddAsync(post);
            
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit(Guid id) 
        {
            // Get Categories listing
            var categoryList = await categoryRepository.GetAllAsync();

            // Use dbContext to find the Post to edit
            var post = await postRepository.GetIdAsync(id);

            if (post != null)
            {
                var editPostRequest = new EditPostRequest
                {
                    Title = post.Title,
                    CategoryId = post.CategoryId,
                    CategoriesList = categoryList.Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }),
                    PublicationDate = post.PublicationDate,
                    Content = post.Content
                };

                return View(editPostRequest);
            }

            return RedirectToAction(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditPostRequest editPostRequest)
        {
            var post = new BlogPost
            {
                Id = editPostRequest.Id,
                Title = editPostRequest.Title,
                CategoryId = editPostRequest.CategoryId,
                PublicationDate=editPostRequest.PublicationDate,
                Content = editPostRequest.Content
            };

            //TODO: Add error handling for Null or Duplicate Category Title

            var editPost = await postRepository.EditAsync(post);

            if (editPost != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Show failure message.
                return RedirectToAction("Edit", new { id = post.Id });
            }

        }
    }
}
