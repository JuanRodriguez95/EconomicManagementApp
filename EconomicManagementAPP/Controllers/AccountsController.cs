using EconomicManagementAPP.Models;
using EconomicManagementAPP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace EconomicManagementAPP.Controllers
{
    public class AccountsController : Controller
    {
        private readonly RepositorieAccounts repositorieAccounts;
        private readonly RepositorieAccountTypes repositorieAccountTypes;

        
        public AccountsController(RepositorieAccounts repositorieAccounts, RepositorieAccountTypes repositorieAccountTypes)
        {
            this.repositorieAccounts = repositorieAccounts;
            this.repositorieAccountTypes = repositorieAccountTypes;
        }

        
        public async Task<IActionResult> Index()
        {
            string loginFlag = HttpContext.Session.GetString("loged");
            if (loginFlag == "true")
            {
                int userId = (int)HttpContext.Session.GetInt32("user");
                var accounts = await repositorieAccounts.GetAccounts(userId);
                return View(accounts);
            }
            return RedirectToAction("Login", "Users");
            
        }

        
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var modelAccounTypes = new AccountCreationViewModel();
            modelAccounTypes.AccountTypes = await GetAccountTypes();
            return View(modelAccounTypes);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Accounts accounts)
        {
            if (!ModelState.IsValid)
            {
                return View(accounts);
            }
            accounts.UserId = (int)HttpContext.Session.GetInt32("user");
            Expression<Func<Accounts, bool>> expression = accountDb => accountDb.Name == accounts.Name;
            var accountExist =
               await repositorieAccounts.Exist(expression);

            if (accountExist)
            {
                ModelState.AddModelError(nameof(accounts.Name),
                    $"The account {accounts.Name} already exist.");

                return View(accounts);
            }
            await repositorieAccounts.Create(accounts);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> VerificaryAccount(string name)
        {
            Expression<Func<Accounts, bool>> expression = accountDb => accountDb.Name == name;
            var accountExist = await repositorieAccounts.Exist(expression);

            if (accountExist)
            {
                return Json($"The account {name} already exist");
            }

            return Json(true);
        }


        [HttpGet]
        public async Task<ActionResult> Modify(int id)
        {            
            var account = await repositorieAccounts.getById(id);

            if (account is null)
            {
                //Redireccio cuando esta vacio
                return RedirectToAction("NotFound", "Home");
            }

            return View(account);
        }

        [HttpPost]
        public async Task<ActionResult> Modify(Accounts accounts)
        {           
            var account = await repositorieAccounts.getById(accounts.Id);

            if (account is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieAccounts.Modify(accounts.Id, accounts);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {            
            var account = await repositorieAccounts.getById(id);

            if (account is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(account);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteAccount(int id)
        {            
            var account = await repositorieAccounts.getById(id);

            if (account is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieAccounts.Delete(id);
            return RedirectToAction("Index");
        }

        private async Task<IEnumerable<SelectListItem>> GetAccountTypes()
        {
            var accountTypes = await repositorieAccountTypes.ListData();
            return accountTypes.Select(selectAccountType => new SelectListItem(selectAccountType.Name, selectAccountType.Id.ToString()));
        }


    }
}
