using LearningEngine.IntegrationTests.Fixtures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LearningEngine.IntegrationTests.Handlers
{
    public class BaseContextTests<TContext>
        where TContext : DbContext
    {
        private readonly BaseDatabaseFixture<TContext> _fixture;

        public BaseContextTests(BaseDatabaseFixture<TContext> fixture)
        {
            _fixture = fixture;
        }

        public async Task UseContext(Func<TContext, Task> useContext)
        {
            await _fixture.UseDbContext(useContext);
        }
    }
}
