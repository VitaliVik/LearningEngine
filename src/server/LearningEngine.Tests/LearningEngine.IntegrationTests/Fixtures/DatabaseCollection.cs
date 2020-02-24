using Xunit;

namespace LearningEngine.IntegrationTests.Fixtures
{
    [CollectionDefinition("DatabaseCollection")]
    public class DatabaseCollection : ICollectionFixture<LearningEngineFixture>
    {
        public DatabaseCollection()
        {
        }
    }
}
