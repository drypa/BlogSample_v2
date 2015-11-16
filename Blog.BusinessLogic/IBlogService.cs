using System;
using System.Collections.Generic;
using Blog.BusinessEntities;

namespace Blog.BusinessLogic
{
    public interface IBlogService
    {
        void AddPost(BlogPost post);

        void DeletePost(Guid postId);
        BlogPost GetPost(Guid postId);
        IList<BlogPost> GetPosts();
    }
}
