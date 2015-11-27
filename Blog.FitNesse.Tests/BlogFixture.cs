using System;
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
            client.AddPost(new BlogPost { Text = "text", Title = "title" });
            Thread.Sleep(5000);
        }
    }
}
