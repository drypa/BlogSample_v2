using System;
using System.Linq;
using Blog.BusinessEntities;
using Blog.BusinessLogic.Client;
using fitlibrary;

namespace Blog.FitNesse.Tests
{
    public class BlogFixture : DoFixture
    {
        private readonly int MaxTimeoutSeconds = 10;

        public void AddComment()
        {
            var post = new BlogPost { Text = "text", Title = "title", Id = Guid.NewGuid(), CreateDate = DateTime.Now };
            var dalc = new TestDalc(Commands.ConnectionString);
            dalc.AddPost(post);

            IBlogClient client = new BlogClient(Commands.ServiceUrl);
            var comment = new Comment
            {
                Post = post,
                Text = "comment text"
            };
            client.AddComment(comment);
            WaitUntilCommentAdded(comment);
        }

        public void AddPost()
        {
            IBlogClient client = new BlogClient(Commands.ServiceUrl);
            var postToAdd = new BlogPost { Text = "text", Title = "title" };
            client.AddPost(postToAdd);
            WaitUntilPostAdded(postToAdd);
        }

        public void DeleteComment()
        {
            var post = new BlogPost { Text = "text", Title = "title", Id = Guid.NewGuid(), CreateDate = DateTime.Now };
            var dalc = new TestDalc(Commands.ConnectionString);
            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                Post = post,
                Text = "comment text",
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
            var post = new BlogPost { Text = "text", Title = "title", Id = Guid.NewGuid(), CreateDate = DateTime.Now };
            var dalc = new TestDalc(Commands.ConnectionString);
            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                Post = post,
                Text = "comment text",
                CreateDate = DateTime.Now
            };
            dalc.AddPost(post);
            dalc.AddComment(comment);

            IBlogClient client = new BlogClient(Commands.ServiceUrl);
            client.DeletePost(post);
            WaitUntilPostDeleted(post);
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
