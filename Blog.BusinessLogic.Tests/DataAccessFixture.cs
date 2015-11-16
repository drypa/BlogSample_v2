using System;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Xunit;

namespace Blog.BusinessLogic.Tests
{
    public class DataAccessFixture
    {
        [Fact]
        public void CanGenerateSchema()
        {
            var configurator = new NHibernateConfigurator();
            Configuration cfg = configurator.GetConfiguration();

            new SchemaExport(cfg).Execute(false, true, false);

            Assert.NotNull(configurator.GetSessionFactory());
        }
    }
}
