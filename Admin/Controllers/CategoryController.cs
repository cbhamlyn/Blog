using Admin.Models.Domain;
using Admin.Models.ViewModels;
using Admin.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        // This returns the default view "AddCategoryRequest" when navigated to.
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        // This runs when the "Save" button is pressed on the "Add Category"  page is pressed.
        [HttpPost]
        // AddCategoryRequest is defined in Models\ViewModels\AddCategoryRequest.cs
        public async Task<IActionResult> Add(AddCategoryRequest addCategoryRequest) 
        {
            // Map AddCategoryRequest to Category domain model.
            var category = new Category
            {
                Title = addCategoryRequest.AddCategoryTitleInput
            };

            //TODO: Add error handling for Null or Duplicate Category Title

            await categoryRepository.AddAsync(category);
            
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            var categories = await categoryRepository.GetAllAsync();

            return View(categories);
        }


        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit(Guid id) 
        {
            
            var category = await categoryRepository.GetIdAsync(id);
            
            if (category != null)
            {
                var editCategoryRequest = new EditCategoryRequest
                {
                    Id = category.Id,
                    Title = category.Title
                };

                return View(editCategoryRequest);
            }

            return RedirectToAction(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditCategoryRequest editCategoryRequest)
        {           
            var category = new Category
            {
                Id = editCategoryRequest.Id,
                Title = editCategoryRequest.Title
            };

            //TODO: Add error handling for Null or Duplicate Category Title

            var editCategory = await categoryRepository.EditAsync(category);

            if (editCategory != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Show failure message.
                return RedirectToAction("Edit", new { id = category.Id });
            }
            
        }
    }
}
