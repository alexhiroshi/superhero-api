using System;

namespace SuperHero.Domain.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity obj);
        void Update(TEntity obj);
        void Remove(Guid id);
        int SaveChanges();
    }
}