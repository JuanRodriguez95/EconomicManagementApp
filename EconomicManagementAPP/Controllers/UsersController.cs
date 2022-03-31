using EconomicManagementAPP.Models;
using EconomicManagementAPP.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace EconomicManagementAPP.Controllers
{
    public class UsersController: Controller
    {
        private readonly RepositorieUsers repositorieUsers;
        private readonly UserManager<Users> userManager;

        //Inicializamos  repositorieUsers para despues inyectarle las funcionalidades de la interfaz
        public UsersController(RepositorieUsers repositorieUsers,UserManager<Users> userManager)
        {
            this.repositorieUsers = repositorieUsers;
            this.userManager = userManager;       
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

            Expression<Func<Users, bool>> expression = u => u.Email == users.Email;
            var usersExist = await repositorieUsers.Exist(expression);

            if (usersExist)
            {
                ModelState.AddModelError(nameof(users.Email),
                    $"The account {users.Email} already exist.");

                return View(users);
            }
            var resultado = await userManager.CreateAsync(users, password: users.Password);

            if (resultado.Succeeded)
            {
               // await signInManager.SignInAsync(usuario, isPersistent: true);
                return RedirectToAction("Index", "Transacciones");
            }
            else
            {
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(users);
            }
            //await repositorieUsers.Create(users);
            return RedirectToAction("Index");
        }

        
        [HttpGet]
        public async Task<IActionResult> VerificaryUsers(string Email)
        {
            Expression<Func<Users, bool>> expression = u => u.Email == Email;
            var usersExist = await repositorieUsers.Exist(expression);

            if (usersExist)
            {
                return Json($"The account {Email} already exist.");
            }

            return Json(true);
        }


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

            await repositorieUsers.Modify(users.Id, users);
            return RedirectToAction("Index");
        }
       
        
        
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
