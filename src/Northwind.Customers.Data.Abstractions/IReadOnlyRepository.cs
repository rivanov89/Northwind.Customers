//using System.Linq.Expressions;

//namespace Northwind.Customers.Data.Abstractions
//{
//    public interface IReadOnlyRepository<TEntity> where TEntity : class
//    {
//        Task<TEntity?> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> filter, CancellationToken token, params Expression<Func<TEntity, object>>[] includes);

//        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter,
//            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
//            CancellationToken token,
//            params Expression<Func<TEntity, object>>[] includes);

//        Task<TEntity[]> FindAsync(
//            Expression<Func<TEntity, bool>> filter,
//            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
//            int pageIndex,
//            int pageSize,
//            CancellationToken token,
//            params Expression<Func<TEntity, object>>[] includes);
//    }
//}
