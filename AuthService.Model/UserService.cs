using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AuthService.Model
{
    public class UserService : IUserService
    {
        public UserService(IAccountRepository accountRepository, ITokenProvider tokenProvider)
        {
            _accountRepository = accountRepository;
            _tokenProvider = tokenProvider;
        }

        public async Task<User> Register(string login, string password)
        {
            await CheckUniqLogin(login);

            long accountsCount = await _accountRepository.Count();
            UserRole role = accountsCount > 0 ? UserRole.Regular : UserRole.Admin;

            var account = new Account(login, password, role);
            await _accountRepository.Add(account);

            return new User(account);
        }

        public async Task<User> Authenticate(string login, string password)
        {
            Account? account = await _accountRepository.GetByLogin(login);
            if (account is null)
            {
                throw new ServiceException("Authentication failed.", HttpStatusCode.BadRequest);
            }

            bool correctPassword = account.VerifyPassword(password);
            if (!correctPassword)
            {
                throw new ServiceException("Authentication failed.", HttpStatusCode.BadRequest);
            }

            string token = _tokenProvider.GenerateToken(account.Login, account.Role);
            return new User(account, token);
        }

        public async Task<User> GetByLogin(string login)
        {
            Account? account = await _accountRepository.GetByLogin(login);
            if (account is null)
            {
                throw new ServiceException($"Login '{login}' not found.", HttpStatusCode.BadRequest);
            }

            return new User(account);
        }

        public async Task<List<User>> GetAll()
        {
            List<Account> accounts = await _accountRepository.GetAll();
            return accounts.Select(x => new User(x)).ToList();
        }

        public async Task Delete(string login)
        {
            Account? account = await _accountRepository.GetByLogin(login);
            if (account is null)
            {
                throw new ServiceException($"Login '{login}' not found.", HttpStatusCode.BadRequest);
            }

            await _accountRepository.Delete(login);
        }

        private async Task CheckUniqLogin(string login)
        {
            Account? foundAccount = await _accountRepository.GetByLogin(login);
            if (foundAccount != null)
            {
                throw new ServiceException("Login taken.", HttpStatusCode.Conflict);
            }
        }

        private readonly IAccountRepository _accountRepository;
        private readonly ITokenProvider _tokenProvider;
    }
}
