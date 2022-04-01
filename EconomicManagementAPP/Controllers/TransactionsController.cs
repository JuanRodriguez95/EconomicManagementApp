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
            string loginFlag = HttpContext.Session.GetString("loged");
            if (loginFlag == "true")
            {
                int userId = (int)HttpContext.Session.GetInt32("user");
                var transactions = await repositorieTransactions.getTransactionByUserId(userId);
                return View(transactions);
            }
            return RedirectToAction("Login", "Users");
            
        }
        public async Task<IActionResult> Create()
        {
            int userId = (int)HttpContext.Session.GetInt32("user");
            var Model = new TransactionCreationViewModel();
            Model.Accounts = await GetAccounts(userId);
            Model.Categories = await GetCategories();
            Model.OperationTypes = await GetOperationTypes();
            return View(Model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransactionCreationViewModel transactions)
        {
            int userId = (int)HttpContext.Session.GetInt32("user");
            if (!ModelState.IsValid)
            {
                transactions.Accounts = await GetAccounts(userId);
                transactions.Categories = await GetCategories();
                transactions.OperationTypes = await GetOperationTypes();
                return View(transactions);
            }
            var account = await repositorieAccounts.GetAccounts(userId);
            if (account is null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            var category = await repositorieCategories.getById(transactions.CategoryId);
            if (category is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            transactions.UserId= userId;
            await repositorieAccounts.UpdateAccountTotal(transactions);
            await repositorieTransactions.Create(transactions);
            return RedirectToAction("Index");
        }        

        [HttpGet]
        public async Task<ActionResult> Modify(int id)
        {
            var transaction = await repositorieTransactions.getById(id);
            if (transaction is null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            return View(transaction);
        }

        [HttpPost]
        public async Task<ActionResult> Modify(Transactions transactions)
        {
            var transaction = await repositorieTransactions.getById(transactions.Id);
            if (transaction is null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            await repositorieTransactions.Modify(transactions.Id,transactions);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var transaction = await repositorieTransactions.getById(id);
            if (transaction is null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            return View(transaction);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var transaction = await repositorieTransactions.getById(id);
            if (transaction is null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            await repositorieTransactions.Delete(id);
            return RedirectToAction("Index");
        }

        private async Task<IEnumerable<SelectListItem>> GetAccounts(int userId)
        {
            var accounts = await repositorieAccounts.GetAccounts(userId);
            return accounts.Select(selectAccount => new SelectListItem(selectAccount.Name, selectAccount.Id.ToString()));
        }
        private async Task<IEnumerable<SelectListItem>> GetCategories()
        {
            var categories = await repositorieCategories.ListData();
            return categories.Select(selectCategorie => new SelectListItem(selectCategorie.Name, selectCategorie.Id.ToString()));
        }
        private async Task<IEnumerable<SelectListItem>> GetOperationTypes()
        {
            var operationTypes = await repositorieOperationTypes.ListData();
            return operationTypes.Select(selectOperation => new SelectListItem(selectOperation.Name, selectOperation.Id.ToString()));
            
        }
    }
}
