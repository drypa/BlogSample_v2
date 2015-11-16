using System;
using System.Collections.Generic;
using Blog.BusinessEntities;

namespace Blog.Dal
{
    interface IBlogDalc
    {
        void AddPost(BlogPost post);

        List<BlogPost> GetPosts();

        BlogPost GetPost(Guid postId);

        void DeletePost(Guid postId);
    }
}
