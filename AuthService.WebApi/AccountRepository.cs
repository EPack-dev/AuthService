using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthService.Model;
using AuthService.WebApi.Database;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AuthService.WebApi
{
    public class AccountRepository : IAccountRepository
    {
        public AccountRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Account?> GetByLogin(string login)
        {
            Account account = await _dbContext.Accounts.Find(x => x.Login == login).FirstOrDefaultAsync();
            return account;
        }

        public async Task<List<Account>> GetAll()
        {
            var filter = new BsonDocument();
            List<Account> accounts = await _dbContext.Accounts.Find(filter).ToListAsync();
            return accounts;
        }

        public async Task<long> Count()
        {
            var filter = new BsonDocument();
            long count = await _dbContext.Accounts.Find(filter).CountDocumentsAsync();
            return count;
        }

        public async Task Add(Account account)
        {
            Account existing = await _dbContext.Accounts.Find(x => x.Login == account.Login).FirstOrDefaultAsync();
            if (existing != null)
            {
                throw new ApplicationException("Login already exist.");
            }

            await _dbContext.Accounts.InsertOneAsync(account);
        }

        public async Task Delete(string login)
        {
            await _dbContext.Accounts.DeleteOneAsync(x => x.Login == login);
        }

        private readonly AppDbContext _dbContext;
    }
}
