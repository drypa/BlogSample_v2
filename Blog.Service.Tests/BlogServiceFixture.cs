using System;
using Blog.BusinessEntities;
using Blog.BusinessLogic;
using Moq;
using Xunit;

namespace Blog.Service.Tests
{
    //public class BlogServiceFixture
    //{
    //    [Fact]
    //    public void BlogServiceShouldAddThrougthWriteRepository()
    //    {
    //        var readRepoMock = new Mock<IBlogRepository>();

    //        IBlogService service = new BlogService(null, readRepoMock.Object);

    //        service.AddPost(new BlogPost());
    //        readRepoMock.Verify(x => x.AddPost(It.IsAny<BlogPost>()), Times.Once());

    //        service.AddComment(new Comment());
    //        readRepoMock.Verify(x => x.AddComment(It.IsAny<Comment>()), Times.Once());
    //    }

    //    [Fact]
    //    public void BlogServiceShouldDeleteThrougthWriteRepository()
    //    {
    //        var readRepoMock = new Mock<IBlogRepository>();

    //        IBlogService service = new BlogService(null, readRepoMock.Object);

    //        service.DeletePost(new BlogPost());
    //        readRepoMock.Verify(x => x.DeletePost(It.IsAny<BlogPost>()), Times.Once());

    //        service.DeleteComment(new Comment());
    //        readRepoMock.Verify(x => x.DeleteComment(It.IsAny<Comment>()), Times.Once());
    //    }

    //    [Fact]
    //    public void BlogServiceShouldReadThrougthReadRepository()
    //    {
    //        Guid guid = Guid.NewGuid();
    //        var readRepoMock = new Mock<IBlogRepository>();

    //        IBlogService service = new BlogService(readRepoMock.Object, null);

    //        service.GetPost(guid);
    //        readRepoMock.Verify(x => x.GetPost(It.IsAny<Guid>()), Times.Once());

    //        service.GetPosts();
    //        readRepoMock.Verify(x => x.GetPosts(), Times.Once());

    //        service.GetComments(guid);
    //        readRepoMock.Verify(x => x.GetComments(It.IsAny<Guid>()), Times.Once());
    //    }
    //}
}
