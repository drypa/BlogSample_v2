using System;
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

        private void WaitUntilPostAdded(BlogPost post)
        {
            var dalc = new TestDalc(Commands.ConnectionString);
            while (!dalc.GetPosts().Any(x => x.Text == post.Text && x.Title == post.Title))
            {
                Thread.Sleep(100);
            }
        }
    }
}
