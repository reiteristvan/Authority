using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using IdentityServer.EntityFramework;

namespace IdentityServer.IntegrationTests.Fixtures
{
    public class SimpleFixture : IDisposable
    {
        public SimpleFixture()
        {
            Context = new IdentityServerContext();
        }

        public IdentityServerContext Context { get; private set; }

        public void Dispose()
        {
            IEnumerable<DbEntityEntry> changes = Context.ChangeTracker.Entries();

            foreach (DbEntityEntry changedEntry in changes)
            {
                Context.Entry(changedEntry.Entity).State = EntityState.Deleted;
            }

            Context.SaveChanges();
            Context.Dispose();
        }
    }
}
