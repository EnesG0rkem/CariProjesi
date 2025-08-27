using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CariProjesi.Data;
using CariProjesi.Models;

namespace CariProjesi.Services
{
    public class MovementService : IService<Movement>
    {
        private readonly GenericRepository<Movement> _movementRepository;
        private readonly GenericRepository<Account> _accountRepository;

        public MovementService(
            GenericRepository<Movement> movementRepsoitory,
            GenericRepository<Account> accountRepsoitory)
        {
            _movementRepository = movementRepsoitory;
            _accountRepository = accountRepsoitory;
        }

        public async Task AddAsync(Movement entity)
        {
            await _movementRepository.AddAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _movementRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Movement>> GetAllAsync()
        {
            return await _movementRepository.GetAllAsync();
        }

        public async Task<Movement> GetByIdAsync(string id)
        {
            return await _movementRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Movement entity)
        {
            await _movementRepository.UpdateAsync(entity);
        }

        public IQueryable<Movement> Where(Expression<Func<Movement, bool>> predicate)
        {
            return _movementRepository.Where(predicate);
        }

        public async Task<string> FindAccountNameAsync(string accountCode)
        {
            var account = await _accountRepository.GetByIdAsync(accountCode);
            if (account == null) return null;
            return account.AccountName + " " + account.AccountSurname;
        }
    }
}