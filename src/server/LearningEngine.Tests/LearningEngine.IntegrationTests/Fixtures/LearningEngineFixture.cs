using LearningEngine.Persistence.Models;

namespace LearningEngine.IntegrationTests.Fixtures
{
    public class LearningEngineFixture : BaseDatabaseFixture<LearnEngineContext>
    {
        public LearningEngineFixture()
            : base((options) => new LearnEngineContext(options))
        {
        }
    }
}
