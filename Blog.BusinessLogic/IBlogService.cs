using System;
using System.Collections.Generic;
using Blog.BusinessEntities;

namespace Blog.BusinessLogic
{
    public interface IBlogService
    {
        void AddPost(BlogPost post);

        List<BlogPost> GetPosts();

        BlogPost GetPost(Guid postId);

        void DeletePost(Guid postId);
    }
}