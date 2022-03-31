using EconomicManagementAPP.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using EconomicManagementAPP.Data;
using Microsoft.EntityFrameworkCore;

namespace EconomicManagementAPP.Services
{
    
    public class RepositorieOperationTypes : GenericRepositorie<OperationTypes>
    {
        private readonly EconomicContext _context;

        public RepositorieOperationTypes(EconomicContext context) : base(context)
        {
            _context = context;
        }

        /*
        // El async va acompañado de Task
        public async Task<bool> Exist(string name)
        {
            var result = await _context.OperationTypes.Where(u => u.Name == name).FirstOrDefaultAsync();
            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task Modify(OperationTypes operationTypes)
        {
            OperationTypes localOperationTypes = new OperationTypes();
            localOperationTypes = await _context.OperationTypes.FindAsync(operationTypes.Id);
            localOperationTypes.Name = operationTypes.Name;
            localOperationTypes.Description = operationTypes.Description;
            await _context.SaveChangesAsync();
        }
        */
    }


}
