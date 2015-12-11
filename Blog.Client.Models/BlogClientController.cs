using System;
using System.Collections.Generic;
using Blog.BusinessLogic.Client;
using Blog.Client.Common.Model;

namespace Blog.Client.Common
{
    public class BlogClientController
    {
        private readonly IBlogClient client;

        public BlogClientController(string serviceUrl)
        {
            client = new BlogClient(serviceUrl);
        }

        public void AddComment(PostComment comment)
        {
            client.AddComment(comment.ToDto());
        }

        public void AddPost(PostDetails post)
        {
            client.AddPost(post.ToDto());
        }

        public void DeleteComment(PostComment comment)
        {
            client.DeleteComment(comment.ToDto());
        }

        public void DeletePost(Post post)
        {
            client.DeletePost(post.ToDto());
        }

        public PostDetails GetPost(Guid postId)
        {
            return client.GetPost(postId).ToPostDetails();
        }

        public List<Post> GetPosts()
        {
            return client.GetPosts().ToViewModel();
        }
    }
}
