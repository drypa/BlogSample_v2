using System;
using System.Collections.Generic;
using Blog.Client.Models;

namespace Blog.Client
{
    public interface IBlogClient
    {
        void AddComment(PostComment comment);

        void AddPost(Post post);

        void DeleteComment(PostComment comment);

        void DeletePost(Post post);

        PostDetails GetPost(Guid postId);

        List<Post> GetPosts();
    }
}
