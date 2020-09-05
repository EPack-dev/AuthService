using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Model
{
    public class UserService : IUserService
    {
        public UserService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<User> Register(string login, string password)
        {
            int accountsCount = await _accountRepository.GetCount();
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

            return new User(account);
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
    }
}
