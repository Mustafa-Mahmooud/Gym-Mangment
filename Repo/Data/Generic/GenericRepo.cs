using Core.Entites;
using Microsoft.EntityFrameworkCore;
using Presentation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Data.Generic
{
    public class GenericRepo<T> : IGeneric<T> where T : BaseEntity
    {
        private readonly GymContext _gymContext;

        public GenericRepo(GymContext gymContext)
        {
            _gymContext = gymContext;
            // Initialize the repository, e.g., set up a database context or in-memory collection

        }
        public async Task<bool> Add(T entity)
        {
           return await Task.Run(() =>
            {
                _gymContext.Set<T>().Add(entity);
                return _gymContext.SaveChanges() > 0;
            });
        }

        public async Task<bool> Delete(int id)
        {
            return await Task.Run(() =>
            {
                var entity = _gymContext.Set<T>().Find(id);
                if (entity != null)
                {
                    _gymContext.Set<T>().Remove(entity);
                    return _gymContext.SaveChanges() > 0;
                }
                return false;
            });
        }

        public async Task<IEnumerable<T>> GetAll()
        {
          return await _gymContext.Set<T>().ToListAsync(); // ToListAsync is asynchronous ,Differed Execution
        }

        public async Task<T> GetById(int id)
        {
            return await _gymContext.Set<T>().FindAsync(id); // FindAsync is asynchronous ,Search Local Then Access Database
        }

        public async Task<bool> Update(T entity)
        {
            return await    Task.Run(() =>
            {
                _gymContext.Set<T>().Update(entity);
                return _gymContext.SaveChanges() > 0;
            });
        }
    }
}
