using EconomicManagementAPP.Models;
using EconomicManagementAPP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EconomicManagementAPP.Controllers
{
    public class TransactionsController : Controller
    {

        private readonly RepositorieTransactions repositorieTransactions;
        private readonly RepositorieAccounts repositorieAccounts;
        private readonly RepositorieCategories repositorieCategories;
        private readonly RepositorieOperationTypes repositorieOperationTypes;


        public TransactionsController(RepositorieTransactions repositorieTransactions, RepositorieAccounts repositorieAccounts, RepositorieCategories repositorieCategories, RepositorieOperationTypes repositorieOperationTypes)
        {
            this.repositorieTransactions = repositorieTransactions;
            this.repositorieAccounts = repositorieAccounts;
            this.repositorieCategories = repositorieCategories;
            this.repositorieOperationTypes = repositorieOperationTypes;
        }

        public async Task<IActionResult> Index()
        {
            var transactions = await repositorieTransactions.ListData();
            return View(transactions);
        }
        public async Task<IActionResult> Create()
        {
            int userId = 2;
            var Model = new TransactionCreationViewModel();
            Model.Accounts = await GetAccounts(userId);
            Model.Categories = await GetCategories();
            Model.OperationTypes = await GetOperationTypes();
            return View(Model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransactionCreationViewModel transactions)
        {
            int usuarioId = 2;

            if (!ModelState.IsValid)
            {
                transactions.Accounts = await GetAccounts(usuarioId);
                transactions.Categories = await GetCategories();
                transactions.OperationTypes = await GetOperationTypes();
                return View(transactions);
            }

            var account = await repositorieAccounts.GetAccounts(usuarioId);

            if (account is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var category = await repositorieCategories.getById(transactions.CategoryId);

            if (category is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorieAccounts.UpdateAccountTotal(transactions);

            await repositorieTransactions.Create(transactions);
            return RedirectToAction("Index");
        }        




        //Actualizar
        //Este retorna la vista tanto del modify
        [HttpGet]
        public async Task<ActionResult> Modify(int Id)
        {
            var transaction = await repositorieTransactions.getById(Id);

            if (transaction is null)
            {
                //Redireccio cuando esta vacio
                return RedirectToAction("NotFound", "Home");
            }

            return View(transaction);
        }
        //Este es el que modifica y retorna al index
        [HttpPost]
        public async Task<ActionResult> Modify(Transactions transactions)
        {
            var transaction = await repositorieTransactions.getById(transactions.Id);

            if (transaction is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieTransactions.Modify(transactions.Id,transactions);// el que llega
            return RedirectToAction("Index");
        }
        // Eliminar
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var transaction = await repositorieTransactions.getById(Id);

            if (transaction is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(transaction);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteTransaction(int Id)
        {
            var transaction = await repositorieTransactions.getById(Id);

            if (transaction is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieTransactions.Delete(Id);
            return RedirectToAction("Index");
        }

        private async Task<IEnumerable<SelectListItem>> GetAccounts(int UserId)
        {
            var accounts = await repositorieAccounts.GetAccounts(UserId);
            return accounts.Select(x => new SelectListItem(x.Name, x.Id.ToString()));
        }
        private async Task<IEnumerable<SelectListItem>> GetCategories()
        {
            var categories = await repositorieCategories.ListData();
            return categories.Select(x => new SelectListItem(x.Name, x.Id.ToString()));
        }
        private async Task<IEnumerable<SelectListItem>> GetOperationTypes()
        {
                var operationTypes = await repositorieOperationTypes.ListData();
                return operationTypes.Select(x => new SelectListItem(x.Name, x.Id.ToString()));
            
        }
    }
}
