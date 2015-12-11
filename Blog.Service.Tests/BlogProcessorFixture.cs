using System;
using Blog.Contract;
using Blog.BusinessLogic;
using Blog.BusinessLogic.Server.Server;
using Moq;
using Xunit;

namespace Blog.Service.Tests
{
    public class BlogProcessorFixture
    {
        [Fact]
        public void BlogServiceShouldReadThrougthReadRepository()
        {
            var readRepoMock = new Mock<IBlogReader>();

            var blogProcessor = new GetPostBlogProcessor(readRepoMock.Object);

            blogProcessor.Get(new GetPostRequest());
            readRepoMock.Verify(x => x.GetPost(It.IsAny<Guid>()), Times.Once());

            blogProcessor.Get(new GetPostsRequest());
            readRepoMock.Verify(x => x.GetPosts(), Times.Once());
        }
    }
}
