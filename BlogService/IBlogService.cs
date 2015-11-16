using System;
using System.Collections.Generic;
using System.ServiceModel;
using Blog.BusinessEntities;

namespace Blog.Service
{
    [ServiceContract]
    public interface IBlogService
    {
        [OperationContract]
        void AddPost(BlogPost post);

        [OperationContract]
        void DeletePost(Guid postId);

        [OperationContract]
        BlogPost GetPost(Guid postId);

        [OperationContract]
        List<BlogPost> GetPosts();
    }
}
