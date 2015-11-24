using System;
using System.ServiceModel.Web;
using Blog.ConsoleService.Contract;
using Nelibur.ServiceModel.Services;

namespace Blog.ConsoleService
{
    public class Program
    {
        static void Main(string[] args)
        {
            NeliburRestService.Configure(x =>
            {
                x.Bind<AddPostRequest, BlogProcessor>();
                x.Bind<DeletePostRequest, BlogProcessor>();
                x.Bind<GetPostRequest, BlogProcessor>();
                x.Bind<GetPostsRequest, BlogProcessor>();
                x.Bind<AddCommentRequest, BlogProcessor>();
                x.Bind<DeleteCommentRequest, BlogProcessor>();
            });
            using (WebServiceHost service = new WebServiceHost(typeof(BlogService)))
            {
                service.Open();
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
                service.Close();
            }
        }
    }
}
