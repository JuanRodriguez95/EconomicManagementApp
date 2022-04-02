# EconomicManagementApp
Desarrollo con ASP.NET Core 6 MVC, SQLServer y EntityFramework para S1ESA y el semillero FedeSoft
***
![banner_asp net-core-6-mvc](https://user-images.githubusercontent.com/78867527/161318514-26d98896-01ef-4d30-8892-326f090ca7e4.png)

## Descripcion

Este proyecto es el resultado de todo lo aprendido durante el Bootcam de FedeSoft para la empresa SIESA, si bien el proyecto aun esta en desarrollo, me esforce por implementar la mayor parte de temas que se trataron en este proceso de formacion.

EconomicManagementApp es una WebApp para la gestion de transacciones sobre una o varias cuentas de un usuario.

![transaction](https://user-images.githubusercontent.com/78867527/161384357-05b0e8c2-c432-4614-86e5-164679d8da08.jpg)


## Tecnologias

Las tecnologias utilzadas en este proyecto son:
* ASP.NET Core 6
* EntityFramework 6
* SQL Server Management Studio 18

## Temas tratados

Dentro del desarrollo de este proyecto se trataron los siguientes temas:
* Patron de diseño MVC 

###### Modelo
```c#
using System.ComponentModel.DataAnnotations;

namespace EconomicManagementAPP.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage="{0}")]
        [EmailAddress(ErrorMessage ="Invalid Format Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "{0}")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

```

##### Vista

![sd](https://user-images.githubusercontent.com/78867527/161346926-522e832f-7436-4581-ad30-50f603982753.jpg)

##### Controlador
```c#
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

```

* Implementacion de Interfaces e Interfaces Genericas.
##### Interfaz Generica
```c#
using System.Linq.Expressions;

namespace EconomicManagementAPP.Service
{
    public interface IGenericRepositorie<T> where T : class
    {
        Task Create(T entity); 
        Task<IEnumerable<T>> ListData();
        Task<T> getById(int Id);
        Task Delete(int Id);
        Task Modify(int Id, T entity);
        Task<bool> Exist(Expression<Func<T, bool>> expression);
    }
}
```
* Entity Framework 6 
##### Repositorio Generico, implementacion de la interfaz generica
```c#
using EconomicManagementAPP.Data;
using EconomicManagementAPP.Service;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace EconomicManagementAPP.Services
{
    public class GenericRepositorie<T> : IGenericRepositorie<T> where T : class
    {
        private EconomicContext _context;

        public GenericRepositorie(EconomicContext context)
        {
            _context = context;
        }
        public async Task Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}
```
* Gestion de bases de datos SQL SERVER.
## Diagrama MER (Modelo Entidad Relacion) EconomicManagementDB

![DiagramaMER](https://user-images.githubusercontent.com/78867527/161315374-8497ec63-eaf7-46c1-a22c-633acd6b9e61.png)

***
## Ejecucion
1. **Ejecutar el script EconomicManagementDB**
https://github.com/JuanRodriguez95/EconomicManagementApp/blob/main/EconomicManagementDB.sql
2. **Configurar la conexion en appsettings.Development.json especificando el nombre del server y desmas atributos de seguridad**

![cadena](https://user-images.githubusercontent.com/78867527/161320919-190b262e-02c3-477a-bda9-abcd54464c15.png)

3. **Compilar y Ejecutar la solucion en Visual Studio 2022** 

***

