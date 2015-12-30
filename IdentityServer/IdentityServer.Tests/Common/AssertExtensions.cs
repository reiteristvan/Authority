using System;
using System.Threading.Tasks;
using Xunit;

namespace IdentityServer.Tests.Common
{
    public static class AssertExtensions
    {
        public static async Task ThrowAsync<TException>(Func<Task> task)
        {
            var expected = typeof (TException);
            Type actual = null;

            try
            {
                await task();
            }
            catch (Exception e)
            {
                actual = e.GetType();
            }

            Assert.Equal(expected, actual);
        }
    }
}
