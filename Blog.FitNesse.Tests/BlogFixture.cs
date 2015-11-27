using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Blog.BusinessEntities;
using Blog.BusinessLogic.Client;
using fitlibrary;

namespace Blog.FitNesse.Tests
{
    public class BlogFixture : DoFixture
    {
        public void AddPost()
        {
            IBlogClient client = new BlogClient(Commands.ServiceUrl);
            var postToAdd = new BlogPost { Text = "text", Title = "title" };
            client.AddPost(postToAdd);
            WaitUntilPostAdded(postToAdd);
        }

        public void AddComment()
        {
            var post = new BlogPost { Text = "text", Title = "title", Id = Guid.NewGuid(), CreateDate = DateTime.Now };
            TestDalc dalc = new TestDalc(Commands.ConnectionString);
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

        private void WaitUntilCommentAdded(Comment comment)
        {
            var dalc = new TestDalc(Commands.ConnectionString);
            Helpers.WaitUntil(() => dalc.GetComments().Any(x => x.Text == comment.Text), MaxTimeoutSeconds);
        }

        private void WaitUntilPostAdded(BlogPost post)
        {
            var dalc = new TestDalc(Commands.ConnectionString);
            Helpers.WaitUntil(()=>dalc.GetPosts().Any(x => x.Text == post.Text && x.Title == post.Title),MaxTimeoutSeconds);
        }

        private readonly int MaxTimeoutSeconds = 10;

    }
}
