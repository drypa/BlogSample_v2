using System;
using System.Collections.Generic;
using Blog.BusinessEntities;
using Blog.BusinessLogic;
using Blog.BusinessLogic.Client;
using Blog.BusinessLogic.Server;
using Blog.Test.Common;
using Moq;
using Ninject;
using TechTalk.SpecFlow;
using Xunit;

namespace Blog.SpecflowTests
{
    [Binding]
    public class SpecflowSteps
    {
        private const string GetPostsResponseKey = "GetPostsResponseKey";
        private const string serviceUrl = "http://localhost:9999/blog";
        private const string connectionstring = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=BlogServiceTest;User ID=tests_user;Password=tests_user";
        private BlogServiceProvider provider;
        [Given("Server was started")]
        public void AndServerWasStarted()
        {
            Cleanup();
            provider = new BlogServiceProvider(serviceUrl, GetNinjectKernel()).Open();

        }
        private static IKernel GetNinjectKernel()
        {
            var kernel = new StandardKernel();

            kernel.Bind<IBlogReader>()
                .To<DapperBlogRepository>();

            Mock<IAppSettingsHelper> settingsMock = new Mock<IAppSettingsHelper>();
            settingsMock.Setup(x => x.GetConnectionString()).Returns(connectionstring);
            kernel.Bind<IAppSettingsHelper>()
                .ToConstant(settingsMock.Object);

            return kernel;
        }

        [Given("There are posts in database")]
        public void GivenThereArePostsInDatabase(Table data)
        {
            var dalc = new TestDalc(connectionstring);
            dalc.CleanDatabase();
            foreach (TableRow row in data.Rows)
            {
                BlogPost post = ParseTableRow(row);
                dalc.AddPost(post);
            }
        }

        [Then("I get posts list")]
        public void IGetPostsList(Table data)
        {
            var expectedPosts = new List<BlogPost>(data.RowCount);
            foreach (TableRow row in data.Rows)
            {
                BlogPost post = ParseTableRow(row);
                expectedPosts.Add(post);
            }
            var actualPosts = ScenarioContext.Current[GetPostsResponseKey] as List<BlogPost>;
            Assert.Equal(expectedPosts.Count, actualPosts.Count);
            foreach (BlogPost expectedPost in expectedPosts)
            {
                Assert.Contains(expectedPost, actualPosts, new PostsComparer());
            }
        }

        [When("I request all posts")]
        public void WhenIRequestAllPosts()
        {

            IBlogClient client = new BlogClient(serviceUrl);
            var response = client.GetPosts();
            ScenarioContext.Current[GetPostsResponseKey] = response;
        }

        private BlogPost ParseTableRow(TableRow row)
        {
            return new BlogPost
            {
                Id = Guid.Parse(row["Id"]),
                Title = row["Title"],
                Text = row["Text"],
                CreateDate = DateTime.Parse(row["CreateDate"])
            };
        }

        [AfterScenario]
        public void Cleanup()
        {
            if (provider != null)
            {
                provider.Dispose();
            }
        }
    }
}
