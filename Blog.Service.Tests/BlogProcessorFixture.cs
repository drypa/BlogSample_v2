using System;
using Blog.BusinessEntities.Contract;
using Blog.BusinessLogic;
using Blog.BusinessLogic.Server;
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

            var blogProcessor = new BlogProcessor(readRepoMock.Object, new ExchangeConfigurationProvider().Configuration);

            blogProcessor.Get(new GetPostRequest());
            readRepoMock.Verify(x => x.GetPost(It.IsAny<Guid>()), Times.Once());

            blogProcessor.Get(new GetPostsRequest());
            readRepoMock.Verify(x => x.GetPosts(), Times.Once());
        }
    }
}
