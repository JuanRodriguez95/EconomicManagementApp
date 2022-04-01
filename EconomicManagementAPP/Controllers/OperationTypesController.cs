using EconomicManagementAPP.Models;
using EconomicManagementAPP.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace EconomicManagementAPP.Controllers
{
    public class OperationTypesController : Controller
    {
        private readonly RepositorieOperationTypes repositorieOperationTypes;

        public OperationTypesController(RepositorieOperationTypes repositorieOperationTypes)
        {
            this.repositorieOperationTypes = repositorieOperationTypes;
        }


        public async Task<IActionResult> Index()
        {
            string loginFlag = HttpContext.Session.GetString("loged");
            if (loginFlag == "true")
            {
                var operationTypes = await repositorieOperationTypes.ListData();
                return View(operationTypes);
            }
            return RedirectToAction("Login", "Users");
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OperationTypes operationTypes)
        {
            if (!ModelState.IsValid)
            {
                return View(operationTypes);
            }
            Expression<Func<OperationTypes, bool>> expression = c => c.Name == operationTypes.Name;
            var operationTypeExist =
               await repositorieOperationTypes.Exist(expression);
            if (operationTypeExist)
            {
                ModelState.AddModelError(nameof(operationTypes.Description),
                    $"The operation types {operationTypes.Description} already exist.");

                return View(operationTypes);
            }
            await repositorieOperationTypes.Create(operationTypes);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> VerificaryOperationType(string Name)
        {
            Expression<Func<OperationTypes, bool>> expression = c => c.Name == Name;
            var operationType = await repositorieOperationTypes.Exist(expression);
            if (operationType)
            {
                return Json($"The Operation {Name} already exist");
            }
            return Json(true);
        }

        [HttpGet]
        public async Task<ActionResult> Modify(int Id)
        {
            var operationType = await repositorieOperationTypes.getById(Id);
            if (operationType is null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            return View(operationType);
        }

        [HttpPost]
        public async Task<ActionResult> Modify(OperationTypes operationTypes)
        {
            var operationType = await repositorieOperationTypes.getById(operationTypes.Id);
            if (operationType is null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            await repositorieOperationTypes.Modify(operationTypes.Id, operationTypes);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var operation = await repositorieOperationTypes.getById(Id);
            if (operation is null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            return View(operation);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOperation(int Id)
        {
            var operation = await repositorieOperationTypes.getById(Id);
            if (operation is null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            await repositorieOperationTypes.Delete(Id);
            return RedirectToAction("Index");
        }
    }
}

