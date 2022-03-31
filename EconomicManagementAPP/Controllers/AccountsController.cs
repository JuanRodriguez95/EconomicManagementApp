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

        //Inicializamos l variable repositorieAccountTypes para despues inyectarle las funcionalidades de la interfaz
        public AccountsController(RepositorieAccounts repositorieAccounts, RepositorieAccountTypes repositorieAccountTypes)
        {
            this.repositorieAccounts = repositorieAccounts;
            this.repositorieAccountTypes = repositorieAccountTypes;
        }

        // Creamos index para ejecutar la interfaz
        public async Task<IActionResult> Index()
        {            
            var accounts = await repositorieAccounts.ListData();
            return View(accounts);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userId = 2;
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
            accounts.UserId = 2;
            Expression<Func<Accounts, bool>> expression = a => a.Name == accounts.Name;
            var accountExist =
               await repositorieAccounts.Exist(expression);

            if (accountExist)
            {
                // AddModelError ya viene predefinido en .net
                // nameOf es el tipo del campo
                ModelState.AddModelError(nameof(accounts.Name),
                    $"The account {accounts.Name} already exist.");

                return View(accounts);
            }
            await repositorieAccounts.Create(accounts);
            // Redireccionamos a la lista
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> VerificaryAccount(string Name)
        {
            Expression<Func<Accounts, bool>> expression = a => a.Name == Name;
            var accountExist = await repositorieAccounts.Exist(expression);

            if (accountExist)
            {
                return Json($"The account {Name} already exist");
            }

            return Json(true);
        }

        //Actualizar
        //Este retorna la vista tanto del modify
        [HttpGet]
        public async Task<ActionResult> Modify(int Id)
        {            
            var account = await repositorieAccounts.getById(Id);

            if (account is null)
            {
                //Redireccio cuando esta vacio
                return RedirectToAction("NotFound", "Home");
            }

            return View(account);
        }
        //Este es el que modifica y retorna al index
        [HttpPost]
        public async Task<ActionResult> Modify(Accounts accounts)
        {           
            var account = await repositorieAccounts.getById(accounts.Id);

            if (account is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieAccounts.Modify(accounts.Id, accounts);// el que llega
            return RedirectToAction("Index");
        }
        // Eliminar
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {            
            var account = await repositorieAccounts.getById(Id);

            if (account is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(account);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteAccount(int Id)
        {            
            var account = await repositorieAccounts.getById(Id);

            if (account is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieAccounts.Delete(Id);
            return RedirectToAction("Index");
        }

        private async Task<IEnumerable<SelectListItem>> GetAccountTypes()
        {
            var accountTypes = await repositorieAccountTypes.ListData();
            return accountTypes.Select(x => new SelectListItem(x.Name, x.Id.ToString()));
        }


    }
}
