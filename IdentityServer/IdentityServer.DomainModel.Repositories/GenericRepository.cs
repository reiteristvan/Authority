using System;
using System.Data.Entity;
using System.Threading.Tasks;
using IdentityServer.EntityFramework;

namespace IdentityServer.DomainModel.Repositories
{
    public class GenericRepository<TEntity>
        where TEntity : EntityBase
    {
        public virtual async Task<TEntity> FindById(Guid id)
        {
            using (IdentityServerContext context = new IdentityServerContext())
            {
                return await context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
            }
        }

        public virtual async Task Create(TEntity entity)
        {
            using (IdentityServerContext context = new IdentityServerContext())
            {
                context.Set<TEntity>().Attach(entity);
                context.Set<TEntity>().Add(entity);
                await context.SaveChangesAsync();
            }
        }

        public virtual async Task Update(TEntity entity)
        {
            using (IdentityServerContext context = new IdentityServerContext())
            {
                context.Set<TEntity>().Attach(entity);
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }

        public virtual async Task Delete(TEntity entity)
        {
            using (IdentityServerContext context = new IdentityServerContext())
            {
                context.Set<TEntity>().Attach(entity);
                context.Entry(entity).State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }
    }
}
