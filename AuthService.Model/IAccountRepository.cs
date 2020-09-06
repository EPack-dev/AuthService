using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthService.Model
{
    public interface IAccountRepository
    {
        Task<Account?> GetByLogin(string login);

        Task<List<Account>> GetAll();

        Task<long> Count();

        Task Add(Account account);

        Task Delete(string login);
    }
}
