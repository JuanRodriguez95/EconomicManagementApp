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

        public async Task<Users> Login(string email, string password)
        {
            var result = await _context.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();
            return result;
        }  

    }


}
