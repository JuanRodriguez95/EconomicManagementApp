using EconomicManagementAPP.Models;
using EconomicManagementAPP.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace EconomicManagementAPP.Controllers
{
    public class AccountTypesController : Controller
    {
        private readonly RepositorieAccountTypes repositorieAccountTypes;

        public AccountTypesController(RepositorieAccountTypes repositorieAccountTypes)
        {
            this.repositorieAccountTypes = repositorieAccountTypes;
        }

        public async Task<IActionResult> Index()
        {
            string loginFlag = HttpContext.Session.GetString("loged");
            if (loginFlag == "true")
            {
                var accountTypes = await repositorieAccountTypes.ListData();
                return View(accountTypes);
            }
            return RedirectToAction("Login", "Users");
            
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AccountTypes accountTypes)
        {
            if (!ModelState.IsValid)
            {
                return View(accountTypes);
            }
            Expression<Func<AccountTypes, bool>> expression = accountTypesDb => accountTypesDb.Name == accountTypes.Name;
            var accountTypeExist = await repositorieAccountTypes.Exist(expression);
            if (accountTypeExist)
            {
                ModelState.AddModelError(nameof(accountTypes.Name),
                    $"The account {accountTypes.Name} already exist.");

                return View(accountTypes);
            }
            await repositorieAccountTypes.Create(accountTypes);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> VerificaryAccountType(string Name)
        {
            Expression<Func<AccountTypes, bool>> expression = accountTypesDb => accountTypesDb.Name == Name;
            var accountTypeExist = await repositorieAccountTypes.Exist(expression);
            if (accountTypeExist)
            {
                return Json($"The account {Name} already exist");
            }
            return Json(true);
        }

        [HttpGet]
        public async Task<ActionResult> Modify(int id)
        {
            var accountType = await repositorieAccountTypes.getById(id);
            if (accountType is null)
            { 
                return RedirectToAction("NotFound", "Home");
            }
            return View(accountType);
        }
        
        [HttpPost]
        public async Task<ActionResult> Modify(AccountTypes accountTypes)
        {
            var accountType = await repositorieAccountTypes.getById(accountTypes.Id);
            if (accountType is null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            await repositorieAccountTypes.Modify(accountTypes.Id,accountTypes);
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var account = await repositorieAccountTypes.getById(id);

            if (account is null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            return View(account);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var account = await repositorieAccountTypes.getById(id);
            if (account is null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            await repositorieAccountTypes.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
