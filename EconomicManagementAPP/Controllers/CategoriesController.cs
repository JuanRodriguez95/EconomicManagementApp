using EconomicManagementAPP.Models;
using EconomicManagementAPP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace EconomicManagementAPP.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly RepositorieCategories repositorieCategories;

        public CategoriesController(RepositorieCategories repositorieCategories)
        {
            this.repositorieCategories = repositorieCategories;
        }

       public async Task<IActionResult> Index()
        {
            string loginFlag = HttpContext.Session.GetString("loged");
            if (loginFlag == "true")
            {
                var categories = await repositorieCategories.ListData();
                return View(categories);
            }
            return RedirectToAction("Login", "Users");
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Categories categories)
        {
            if (!ModelState.IsValid)
            {
                return View(categories);
            }
            Expression<Func<Categories, bool>> expression = categoriesDb => categoriesDb.Name == categories.Name;
            var categoriesExist =
               await repositorieCategories.Exist(expression);
            if (categoriesExist)
            {
                ModelState.AddModelError(nameof(categories.Name),
                    $"The categorie {categories.Name} already exist.");

                return View(categories);
            }
            await repositorieCategories.Create(categories);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> VerificaryCategory(string name)
        {
            Expression<Func<Categories, bool>> expression = categoriesDb => categoriesDb.Name == name;
            var categoriesExist = await repositorieCategories.Exist(expression);

            if (categoriesExist)
            { 
                return Json($"The categories {name} already exist");
            }
            return Json(true);
        }

        [HttpGet]
        public async Task<ActionResult> Modify(int id)
        {
            var categorie = await repositorieCategories.getById(id);
            if (categorie is null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            return View(categorie);
        }

        [HttpPost]
        public async Task<ActionResult> Modify(Categories categories)
        {
            var categorie = await repositorieCategories.getById(categories.Id);
            if (categorie is null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            await repositorieCategories.Modify(categories.Id, categories);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var categorie = await repositorieCategories.getById(id);
            if (categorie is null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            return View(categorie);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCategories(int id)
        {
            var categorie = await repositorieCategories.getById(id);
            if (categorie is null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            await repositorieCategories.Delete(id);
            return RedirectToAction("Index");
        }

        
    }
}
