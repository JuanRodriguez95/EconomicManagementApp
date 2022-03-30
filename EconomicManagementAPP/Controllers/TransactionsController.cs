using EconomicManagementAPP.Models;
using EconomicManagementAPP.Services;
using Microsoft.AspNetCore.Mvc;

namespace EconomicManagementAPP.Controllers
{
    public class TransactionsController : Controller
    {

        private readonly RepositorieTransactions repositorieTransactions;

        //Inicializamos l variable repositorieAccountTypes para despues inyectarle las funcionalidades de la interfaz
        public TransactionsController(RepositorieTransactions repositorieTransactions)
        {
            this.repositorieTransactions = repositorieTransactions;
        }

        // Creamos index para ejecutar la interfaz
        public async Task<IActionResult> Index()
        {
            var transactions = await repositorieTransactions.ListData();
            return View(transactions);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Transactions transactions)
        {
            if (!ModelState.IsValid)
            {
                return View(transactions);
            }
            
            await repositorieTransactions.Create(transactions);
            // Redireccionamos a la lista
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

            await repositorieTransactions.Modify(transactions);// el que llega
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
    }
}
