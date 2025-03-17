using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IAccountService
    {
        Task<Account> Login(string username, string password);

        Task<Account> register(Account account, Customer customer);
        public Task<IList<Account>> GetAllAccount();
    }
}
