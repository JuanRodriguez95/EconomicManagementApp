using EconomicManagementAPP.Models;
using EconomicManagementAPP.Services;
using Microsoft.AspNetCore.Mvc;

namespace EconomicManagementAPP.Controllers
{
    public class UsersController: Controller
    {
        private readonly RepositorieUsers repositorieUsers;

        //Inicializamos  repositorieUsers para despues inyectarle las funcionalidades de la interfaz
        public UsersController(RepositorieUsers repositorieUsers)
        {
            this.repositorieUsers = repositorieUsers;
            
        }

        // Creamos index para ejecutar la interfaz
        // No enviamos parametros por el getUser ya que lo traemos todo mediante la interfaz
        public async Task<IActionResult> Index()        
        {
            
            var users = await repositorieUsers.ListData();
            return View(users);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Users users)
        {
            if (!ModelState.IsValid)
            {
                return View(users);
            }

           
            // Validamos si el usuario ya existe mediante el email, no enviamos parametros predefinidos, pero si el primer email que tome
            var usersExist =
               await repositorieUsers.Exist(users.Email);

            if (usersExist)
            {
                // Validamos la existencia del usuario mediante el email
                ModelState.AddModelError(nameof(users.Email),
                    $"The account {users.Email} already exist.");

                return View(users);
            }
            await repositorieUsers.Create(users);
            // Redireccionamos a la lista de usuarios
            return RedirectToAction("Index");
        }

        // Hace que la validacion se active automaticamente desde el front mediante el email
        [HttpGet]
        public async Task<IActionResult> VerificaryUsers(string Email)
        {
            
            var usersExist = await repositorieUsers.Exist(Email);

            if (usersExist)
            {
                return Json($"The account {Email} already exist.");
            }

            return Json(true);
        }





        //Actualizar
        //Este retorna la vista tanto del modify
        [HttpGet]
        public async Task<ActionResult> Modify(int Id)        {
            
            var user = await repositorieUsers.getById(Id);

            if (user is null)
            {
                //Redireccio cuando esta vacio
                return RedirectToAction("NotFound", "Home");
            }

            return View(user);
        }
        //Este es el que modifica y retorna al index
        [HttpPost]
        public async Task<ActionResult> Modify(Users users)
        {           
            var user = await repositorieUsers.getById(users.Id);

            if (user is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            if (!ModelState.IsValid)
            {
                return View(users);
            }

            await repositorieUsers.Modify(users);// el que llega
            return RedirectToAction("Index");
        }
       
        
        // Eliminar
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {            
            var user = await repositorieUsers.getById(Id);

            if (user is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(int Id)
        {          
            var user = await repositorieUsers.getById(Id);

            if (user is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieUsers.Delete(Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login (LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            var result = await repositorieUsers.Login(loginViewModel.Email, loginViewModel.Password);
            if(result is null)
            {
                ModelState.AddModelError(String.Empty, "Wrong Email or Password");
                return View(loginViewModel);
            }
            else
            {
                return RedirectToAction("Create","Accounts");
            }
        }
    }
}
