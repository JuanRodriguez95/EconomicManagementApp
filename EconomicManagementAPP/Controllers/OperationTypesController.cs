using EconomicManagementAPP.Models;
using EconomicManagementAPP.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace EconomicManagementAPP.Controllers
{
    public class OperationTypesController : Controller
    {
        private readonly RepositorieOperationTypes repositorieOperationTypes;

        //Inicializamos l variable repositorieAccountTypes para despues inyectarle las funcionalidades de la interfaz
        public OperationTypesController(RepositorieOperationTypes repositorieOperationTypes)
        {
            this.repositorieOperationTypes = repositorieOperationTypes;
        }

        // Creamos index para ejecutar la interfaz
        public async Task<IActionResult> Index()
        {
            var operationTypes = await repositorieOperationTypes.ListData();
            return View(operationTypes);
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

            // Validamos si ya existe antes de registrar
            var operationTypeExist =
               await repositorieOperationTypes.Exist(expression);

            if (operationTypeExist)
            {
                // AddModelError ya viene predefinido en .net
                // nameOf es el tipo del campo
                ModelState.AddModelError(nameof(operationTypes.Description),
                    $"The operation types {operationTypes.Description} already exist.");

                return View(operationTypes);
            }

            await repositorieOperationTypes.Create(operationTypes);
            // Redireccionamos a la lista
            return RedirectToAction("Index");
        }

        // Hace que la validacion se active automaticamente desde el front
        [HttpGet]
        public async Task<IActionResult> VerificaryOperationType(string Name)
        {
            Expression<Func<OperationTypes, bool>> expression = c => c.Name == Name;
            var operationType = await repositorieOperationTypes.Exist(expression);

            if (operationType)
            {
                // permite acciones directas entre front y back
                return Json($"The Operation {Name} already exist");
            }

            return Json(true);
        }






        //Actualizar
        //Este retorna la vista tanto del modify
        [HttpGet]
        public async Task<ActionResult> Modify(int Id)
        {
            var operationType = await repositorieOperationTypes.getById(Id);

            if (operationType is null)
            {
                //Redireccio cuando esta vacio
                return RedirectToAction("NotFound", "Home");
            }

            return View(operationType);
        }
        //Este es el que modifica y retorna al index
        [HttpPost]
        public async Task<ActionResult> Modify(OperationTypes operationTypes)
        {
            var operationType = await repositorieOperationTypes.getById(operationTypes.Id);

            if (operationType is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieOperationTypes.Modify(operationTypes.Id,operationTypes);// el que llega
            return RedirectToAction("Index");
        }
        // Eliminar
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

