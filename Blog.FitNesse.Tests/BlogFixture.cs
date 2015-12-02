using System;
using System.Linq;
using Blog.BusinessEntities;
using Blog.BusinessLogic.Client;
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
            BlogPost post = CreatePost();
            var dalc = new TestDalc(Commands.ConnectionString);
            dalc.AddPost(post);

            IBlogClient client = new BlogClient(Commands.ServiceUrl);
            Comment comment = CreateComment(post);
            client.AddComment(comment);
            WaitUntilCommentAdded(comment);
        }

        public void AddPost()
        {
            IBlogClient client = new BlogClient(Commands.ServiceUrl);
            BlogPost postToAdd = CreatePost();
            client.AddPost(postToAdd);
            WaitUntilPostAdded(postToAdd);
        }

        public void DeleteComment()
        {
            BlogPost post = CreatePost();
            var dalc = new TestDalc(Commands.ConnectionString);
            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                Post = post,
                Text = commentText,
                CreateDate = DateTime.Now
            };
            dalc.AddPost(post);
            dalc.AddComment(comment);

            IBlogClient client = new BlogClient(Commands.ServiceUrl);
            client.DeleteComment(comment);
            WaitUntilCommentDeleted(comment);
        }

        public void DeletePostWithComments()
        {
            BlogPost post = CreatePost();
            var dalc = new TestDalc(Commands.ConnectionString);
            Comment comment = CreateComment(post);
            dalc.AddPost(post);
            dalc.AddComment(comment);

            IBlogClient client = new BlogClient(Commands.ServiceUrl);
            client.DeletePost(post);
            WaitUntilPostDeleted(post);
        }

        private Comment CreateComment(BlogPost post)
        {
            return new Comment
            {
                Id = Guid.NewGuid(),
                Post = post,
                Text = commentText,
                CreateDate = DateTime.Now
            };
        }

        private BlogPost CreatePost()
        {
            return new BlogPost { Text = postText, Title = postTitle, Id = Guid.NewGuid(), CreateDate = DateTime.Now };
        }

        private void WaitUntilCommentAdded(Comment comment)
        {
            var dalc = new TestDalc(Commands.ConnectionString);
            Helpers.WaitUntil(() => dalc.GetComments().Any(x => x.Text == comment.Text), MaxTimeoutSeconds);
        }

        private void WaitUntilCommentDeleted(Comment comment)
        {
            var dalc = new TestDalc(Commands.ConnectionString);
            Helpers.WaitUntil(() => !dalc.GetComments().Any(x => x.Id == comment.Id), MaxTimeoutSeconds);
        }

        private void WaitUntilPostAdded(BlogPost post)
        {
            var dalc = new TestDalc(Commands.ConnectionString);
            Helpers.WaitUntil(() => dalc.GetPosts().Any(x => x.Text == post.Text && x.Title == post.Title), MaxTimeoutSeconds);
        }

        private void WaitUntilPostDeleted(BlogPost post)
        {
            var dalc = new TestDalc(Commands.ConnectionString);
            Helpers.WaitUntil(() => !dalc.GetPosts().Any(x => x.Id == post.Id), MaxTimeoutSeconds);
        }
    }
}
