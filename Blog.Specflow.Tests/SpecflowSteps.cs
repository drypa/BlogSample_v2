using System;
using System.Collections.Generic;
using Blog.BusinessEntities;
using Blog.BusinessEntities.Contract;
using Blog.BusinessLogic;
using Blog.BusinessLogic.Client;
using Blog.BusinessLogic.Server;
using Blog.Test.Common;
using Moq;
using Nelibur.ServiceModel.Services.Operations;
using Ninject;
using TechTalk.SpecFlow;
using Xunit;

namespace Blog.SpecflowTests
{
    [Binding]
    public class SpecflowSteps
    {
        private const string GetPostsResponseKey = "GetPostsResponseKey";
        private const string connectionstring = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=BlogServiceTest;User ID=tests_user;Password=tests_user";
        private const string serviceUrl = "http://localhost:9999/blog";
        private BlogServiceProvider provider;

        [Given("Server was started")]
        public void AndServerWasStarted()
        {
            Cleanup();
            provider = new BlogServiceProvider(serviceUrl, GetNinjectKernel()).Open();
        }

        [AfterScenario]
        public void Cleanup()
        {
            if (provider != null)
            {
                provider.Dispose();
            }
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
            List<BlogPost> response = client.GetPosts();
            ScenarioContext.Current[GetPostsResponseKey] = response;
        }

        private static IKernel GetNinjectKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IBlogReader>()
                .To<DapperBlogRepository>();
            var addPostMock = new Mock<IPostOneWay<AddPostRequest>>();
            var addCommentMock = new Mock<IPostOneWay<AddCommentRequest>>();
            var delCommentMock = new Mock<IDeleteOneWay<DeleteCommentRequest>>();
            var delPostMock = new Mock<IDeleteOneWay<DeletePostRequest>>();
            kernel.Bind<IPostOneWay<AddPostRequest>>()
                .ToConstant(addPostMock.Object);
            kernel.Bind<IPostOneWay<AddCommentRequest>>()
                .ToConstant(addCommentMock.Object);
            kernel.Bind<IDeleteOneWay<DeleteCommentRequest>>()
                .ToConstant(delCommentMock.Object);
            kernel.Bind<IDeleteOneWay<DeletePostRequest>>()
               .ToConstant(delPostMock.Object);
            var settingsMock = new Mock<IAppSettingsHelper>();
            settingsMock.Setup(x => x.GetConnectionString()).Returns(connectionstring);
            kernel.Bind<IAppSettingsHelper>()
                .ToConstant(settingsMock.Object);

            return kernel;
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
    }
}
