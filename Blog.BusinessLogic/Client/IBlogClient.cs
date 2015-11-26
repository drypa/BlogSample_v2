using System;
using System.Collections.Generic;
using Blog.BusinessEntities;

namespace Blog.BusinessLogic.Client
{
    public interface IBlogClient
    {
        void AddComment(Comment comment);

        void AddPost(BlogPost post);

        void DeleteComment(Comment comment);

        void DeletePost(BlogPost post);

        BlogPost GetPost(Guid postId);

        List<BlogPost> GetPosts();
    }
}
