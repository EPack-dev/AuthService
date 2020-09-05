using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthService.Model
{
    public interface IUserService
    {
        Task<User> Register(string login, string password, UserRole role);

        Task<User> Authenticate(string login, string password);

        Task<User> GetByLogin(string login);

        Task<List<User>> GetAll();

        Task Delete(string login);
    }
}
