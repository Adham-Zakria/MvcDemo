using Demo.DataAccess.Contexts;
using Demo.DataAccess.Models.SharedModels;
using Demo.DataAccess.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Demo.DataAccess.Repositories.Classes
{
    public class GenericRepository<TEntity>(AppDbContext appDbContext) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        #region Get
        public TEntity? GetById(int id)
            => _appDbContext.Set<TEntity>().Find(id);

        public IEnumerable<TEntity> GetAll(bool IsTracking = false)
        {
            if (IsTracking)
                return _appDbContext.Set<TEntity>().ToList();
            else
                return _appDbContext.Set<TEntity>().AsNoTracking().ToList();
        }

        public IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            return _appDbContext.Set<TEntity>()
                .Select(selector);
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter)
        {
            return _appDbContext.Set<TEntity>()
                      .Where(e=>e.IsDeleted != true)
                      .Where(filter)
                      .ToList();
        }

        #endregion

        public void /*int*/ Add(TEntity entity)
        {
            _appDbContext.Set<TEntity>().Add(entity);
            //return _appDbContext.SaveChanges();
        }

        public void /*int*/ Update(TEntity entity)
        {
            _appDbContext.Set<TEntity>().Update(entity);
            //return _appDbContext.SaveChanges();
        }

        public void /*int*/ Remove(TEntity entity)
        {
            _appDbContext.Set<TEntity>().Remove(entity);
           // return _appDbContext.SaveChanges();
        }

    }
}
