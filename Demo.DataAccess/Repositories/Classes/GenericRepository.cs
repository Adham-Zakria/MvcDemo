using Demo.DataAccess.Contexts;
using Demo.DataAccess.Models.EmployeeModels;
using Demo.DataAccess.Models.SharedModels;
using Demo.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Classes
{
    public class GenericRepository<TEntity>(AppDbContext appDbContext) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public TEntity? GetById(int id)
          => _appDbContext.Set<TEntity>().Find(id);

        public IEnumerable<TEntity> GetAll(bool IsTracking = false)
        {
            if (IsTracking)
                return _appDbContext.Set<TEntity>().ToList();
            else
                return _appDbContext.Set<TEntity>().AsNoTracking().ToList();
        }

        public int Add(TEntity entity)
        {
            _appDbContext.Set<TEntity>().Add(entity);
            return _appDbContext.SaveChanges();
        }

        public int Update(TEntity entity)
        {
            _appDbContext.Set<TEntity>().Update(entity);
            return _appDbContext.SaveChanges();
        }

        public int Remove(TEntity entity)
        {
            _appDbContext.Set<TEntity>().Remove(entity);
            return _appDbContext.SaveChanges();
        }

        //public IEnumerable<TResult> GetAll<TResult>(System.Linq.Expressions.Expression<Func<TEntity, TResult>> selector)
        //{
        //    return _appDbContext.Set<TEntity>()
        //        .Select(selector);
        //}
    }
}
