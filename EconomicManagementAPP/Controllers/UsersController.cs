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
  

        public UsersController(RepositorieUsers repositorieUsers)
        {
            this.repositorieUsers = repositorieUsers;
           
        }

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
            await repositorieUsers.Create(users);
            return RedirectToAction("Login");
        }

        
        [HttpGet]
        public async Task<IActionResult> VerificaryUsers(string email)
        {
            Expression<Func<Users, bool>> expression = u => u.Email == email;
            var usersExist = await repositorieUsers.Exist(expression);
            if (usersExist)
            {
                return Json($"The account {email} already exist.");
            }
            return Json(true);
        }


        [HttpGet]
        public async Task<ActionResult> Modify(int id)        {
            var user = await repositorieUsers.getById(id);
            if (user is null)
            {
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
        public async Task<IActionResult> Delete(int id)
        {
            var user = await repositorieUsers.getById(id);
            if (user is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id)
        {          
            var user = await repositorieUsers.getById(id);
            if (user is null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            await repositorieUsers.Delete(id);
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
                HttpContext.Session.SetInt32("user",result.Id);
                HttpContext.Session.SetString("loged", "true");
                return RedirectToAction("Create","Accounts");
            }
        }
    }
}
