using System;
using System.Linq;
using Blog.BusinessEntities;
using Blog.Client.Common;
using Blog.Client.Common.Model;
using Blog.Test.Common;
using fitlibrary;

namespace Blog.FitNesse.Tests
{
    public class BlogFixture : DoFixture
    {
        private const string commentText = "comment text";
        private const string postText = "post text";
        private const string postTitle = "post title";
        private readonly int MaxTimeoutSeconds = 10;

        public void AddComment()
        {
            PostDetails post = CreatePost();
            var repository = GetTestRepository();
            repository.AddPost(post);

            BlogClientController client = new BlogClientController(Commands.ServiceUrl);
            var comment = CreateComment(post);
            client.AddComment(comment);
            WaitUntilCommentAdded(comment);
        }

        private TestRepository GetTestRepository()
        {
            return new TestRepository(Commands.ConnectionString);
        }

        public void AddPost()
        {
            var client = GetClient();
            PostDetails postToAdd = CreatePost();
            client.AddPost(postToAdd);
            WaitUntilPostAdded(postToAdd);
        }

        public void DeleteComment()
        {
            PostDetails post = CreatePost();
            var repository = GetTestRepository();
            var comment = new PostComment
            {
                Id = Guid.NewGuid(),
                Post = post,
                Text = commentText,
                CreationDate = DateTime.Now
            };
            repository.AddPost(post);
            repository.AddComment(comment);

            var client = GetClient();
            client.DeleteComment(comment);
            WaitUntilCommentDeleted(comment);
        }

        public void DeletePostWithComments()
        {
            PostDetails post = CreatePost();
            var repository = GetTestRepository();
            var comment = CreateComment(post);
            repository.AddPost(post);
            repository.AddComment(comment);

            var client = GetClient();
            client.DeletePost(post);
            WaitUntilPostDeleted(post);
        }

        private BlogClientController GetClient()
        {
            return new BlogClientController(Commands.ServiceUrl);
        }

        private PostComment CreateComment(Post post)
        {
            return new PostComment
            {
                Id = Guid.NewGuid(),
                Post = post,
                Text = commentText,
                CreationDate = DateTime.Now
            };
        }

        private PostDetails CreatePost()
        {
            return new PostDetails { Text = postText, Title = postTitle, Id = Guid.NewGuid(), CreationDate = DateTime.Now };
        }

        private void WaitUntilCommentAdded(PostComment comment)
        {
            var repo = GetRepo();
            Helpers.WaitUntil(() => repo.GetComments().Any(x => x.Text == comment.Text), MaxTimeoutSeconds);
        }

        private TestRepository GetRepo()
        {
            return new TestRepository(Commands.ConnectionString);
        }

        private void WaitUntilCommentDeleted(PostComment comment)
        {
            var repo = GetRepo();
            Helpers.WaitUntil(() => !repo.GetComments().Any(x => x.Id == comment.Id), MaxTimeoutSeconds);
        }

        private void WaitUntilPostAdded(PostDetails post)
        {
            var repo = GetRepo();
            Helpers.WaitUntil(() => repo.GetPosts().Any(x => x.Text == post.Text && x.Title == post.Title), MaxTimeoutSeconds);
        }

        private void WaitUntilPostDeleted(Post post)
        {
            var repo = GetRepo();
            Helpers.WaitUntil(() => !repo.GetPosts().Any(x => x.Id == post.Id), MaxTimeoutSeconds);
        }
    }
}
