using System;
using System.Collections.Generic;
using Blog.BusinessEntities;
using Blog.BusinessLogic.Common;
using Blog.Contract;
using Nelibur.ServiceModel.Services.Operations;

namespace Blog.BusinessLogic.Server.Server
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
            IList<BlogPost> posts = reader.GetPosts();
            return new PostListResponse
            {
                Posts = posts == null ? null : posts.ToDto()
            };
        }
    }
}
