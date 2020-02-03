using LearningEngine.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LearningEngine.IntegrationTests.Handlers
{
    [CollectionDefinition("DatabaseCollection")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
        public DatabaseCollection()
        {

        }
    }

}
