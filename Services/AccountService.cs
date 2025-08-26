using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CariProjesi.Data;
using CariProjesi.Models;

namespace CariProjesi.Services
{
    public class AccountService : IService<Account>
    {
        private readonly GenericRepository <Account> _accountRepository;

        public AccountService(GenericRepository<Account> accountRepsoitory)
        {
            _accountRepository = accountRepsoitory;
        }

       // AccountService.cs
        public async Task AddAsync(Account entity)
        {
            var exists = _accountRepository.Where(a => a.AccountCode == entity.AccountCode).Any();
            if (exists)
                throw new InvalidOperationException("Bu AccountCode ile zaten bir hesap var.");

            await _accountRepository.AddAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _accountRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _accountRepository.GetAllAsync();
        }

        public async Task<Account> GetByIdAsync(string id)
        {
            return await _accountRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Account entity)
        {
            await _accountRepository.UpdateAsync(entity);
        }

        public IQueryable<Account> Where(Expression<Func<Account, bool>> predicate)
        {
            return _accountRepository.Where(predicate);
        }
    }
}