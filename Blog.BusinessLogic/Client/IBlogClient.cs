using System;
using System.Collections.Generic;
using Blog.Contract;

namespace Blog.BusinessLogic.Client
{
    public interface IBlogClient
    {
        void AddComment(CommentDto comment);

        void AddPost(BlogPostDto post);

        void DeleteComment(CommentDto comment);

        void DeletePost(BlogPostDto post);

        BlogPostDto GetPost(Guid postId);

        List<BlogPostDto> GetPosts();
    }
}
