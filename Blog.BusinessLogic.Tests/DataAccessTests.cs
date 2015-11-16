using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Tool.hbm2ddl;
using Xunit;

namespace Blog.BusinessLogic.Tests
{
    [TestClass]
    public class DataAccessTests
    {
        [Fact]
        public void CanGenerateSchema()
        {
            var configurator = new NHibernateConfigurator();
            var cfg = configurator.GetConfiguration();

            new SchemaExport(cfg).Execute(false, true, false);
            
        }
    }
}
