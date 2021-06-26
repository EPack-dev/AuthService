using System.Collections.Generic;
using System.Threading.Tasks;
using AuthService.Application.Entities;
using AuthService.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AuthService.EfCorePostgresProvider
{
    public class AccountRepository : IAccountRepository
    {
        public AccountRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Account?> GetByLogin(string login)
        {
            AccountEntity? entity = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.Login == login);
            if (entity is null)
            {
                return null;
            }

            Account account = _mapper.Map<Account>(entity);
            return account;
        }

        public async Task<List<Account>> GetAll()
        {
            List<AccountEntity> entities = await _dbContext.Accounts.ToListAsync();
            List<Account> accounts = _mapper.Map<List<Account>>(entities);
            return accounts;
        }

        public async Task<long> Count()
        {
            long count = await _dbContext.Accounts.CountAsync();
            return count;
        }

        public async Task Add(Account account)
        {
            AccountEntity entity = _mapper.Map<AccountEntity>(account);
            await _dbContext.Accounts.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(string login)
        {
            AccountEntity? entity = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.Login == login);
            if (entity is not null)
            {
                _dbContext.Accounts.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
    }
}
