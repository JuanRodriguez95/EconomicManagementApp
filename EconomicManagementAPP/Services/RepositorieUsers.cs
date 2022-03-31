using EconomicManagementAPP.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using EconomicManagementAPP.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EconomicManagementAPP.Services
{
    public class RepositorieUsers : GenericRepositorie<Users>
    {
        
        private EconomicContext _context;

        public RepositorieUsers(EconomicContext context) : base(context)
        {
            _context = context;
        }

        /*
        public async Task<bool> Exist(string email)
        {
            var result = await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            if(result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        */
        

        /*
        public async Task Modify(Users users)
        {
            Users localUser = new Users();
            localUser = await _context.Users.FindAsync(users.Id);
            localUser.Email = users.Email;
            localUser.StandarEmail = users.StandarEmail;
            localUser.Password = users.Password;
            await _context.SaveChangesAsync();
        }
        */
        public async Task<Users> Login(string email, string password)
        {
            var result = await _context.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();
            return result;
        }  

    }


}
