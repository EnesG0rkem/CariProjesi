using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CariProjesi.Data;
using CariProjesi.Models;

namespace CariProjesi.Services
{
    public class AccountMovementService : IService <AccountMovement>
    {
        private readonly GenericRepository <AccountMovement> _accountMovementRepository;

        public AccountMovementService(GenericRepository <AccountMovement> accountMovementRepsoitory)
        {
            _accountMovementRepository = accountMovementRepsoitory;
        }

        public async Task AddAsync(AccountMovement entity)
        {
            await _accountMovementRepository.AddAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _accountMovementRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<AccountMovement>> GetAllAsync()
        {
            return await _accountMovementRepository.GetAllAsync();
        }

        public async Task<AccountMovement> GetByIdAsync(string id)
        {
            return await _accountMovementRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(AccountMovement entity)
        {
            await _accountMovementRepository.UpdateAsync(entity);
        }

        public IQueryable<AccountMovement> Where(Expression<Func<AccountMovement, bool>> predicate)
        {
            return _accountMovementRepository.Where(predicate);
        }
    }
}