using System;
using System.Collections.Generic;
using System.Linq;
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
                throw new ApplicationException("Authentication failed.");
            }

            bool correctPassword = account.VerifyPassword(password);
            if (!correctPassword)
            {
                throw new ApplicationException("Authentication failed.");
            }

            string token = _tokenProvider.GenerateToken(account.Login, account.Role);
            return new User(account, token);
        }

        public async Task<User> GetByLogin(string login)
        {
            Account? account = await _accountRepository.GetByLogin(login);
            if (account is null)
            {
                throw new ApplicationException($"Login '{login}' not found.");
            }

            return new User(account);
        }

        public async Task<User?> GetByToken(string token)
        {
            string? login = _tokenProvider.ValidateTokenAndGetLogin(token);
            if (login != null)
            {
                return await GetByLogin(login);
            }

            return null;
        }

        public async Task<List<User>> GetAll()
        {
            List<Account> accounts = await _accountRepository.GetAll();
            return accounts.Select(x => new User(x)).ToList();
        }

        public async Task Delete(string login)
        {
            await _accountRepository.Delete(login);
        }

        private readonly IAccountRepository _accountRepository;
        private readonly ITokenProvider _tokenProvider;
    }
}
