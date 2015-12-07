using System;
using Blog.BusinessEntities.Contract;
using Blog.BusinessLogic.RabbitMQ;
using Nelibur.ServiceModel.Services.Operations;

namespace Blog.BusinessLogic.Server
{
    public class GetPostBlogProcessor :
        IGet<GetPostRequest>,
        IGet<GetPostsRequest>
    {
        private readonly IBlogReader reader;

        public GetPostBlogProcessor(IBlogReader readRepository)
        {
            reader = readRepository;
        }

        public object Get(GetPostRequest request)
        {
            return reader.GetPost(request.PostId);
        }

        public object Get(GetPostsRequest request)
        {
            return new PostListResponse
            {
                Posts = reader.GetPosts()
            };
        }

    }
}
