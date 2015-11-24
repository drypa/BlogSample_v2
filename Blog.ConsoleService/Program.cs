using System;
using System.ServiceModel.Web;
using Blog.BusinessLogic;
using Blog.ConsoleService.Contract;
using Nelibur.ServiceModel.Services;
using Ninject;

namespace Blog.ConsoleService
{
    public class Program
    {
        private static IKernel GetNinjectKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IBlogRepository>()
                .To<DapperBlogRepository>()
                .Named("read");
            kernel.Bind<IAppSettingsHelper>()
               .To<AppSettingsHelper>();
            return kernel;
        }

        private static void Main(string[] args)
        {
            using (var kernel = GetNinjectKernel())
            {
                var instance = new BlogProcessor(kernel.Get<IBlogRepository>());
                NeliburRestService.Configure(x =>
                {
                    x.Bind<AddPostRequest, BlogProcessor>(() => instance);
                    x.Bind<DeletePostRequest, BlogProcessor>(() => instance);
                    x.Bind<GetPostRequest, BlogProcessor>(() => instance);
                    x.Bind<GetPostsRequest, BlogProcessor>(() => instance);
                    x.Bind<AddCommentRequest, BlogProcessor>(() => instance);
                    x.Bind<DeleteCommentRequest, BlogProcessor>(() => instance);
                });


                using (var service = new WebServiceHost(typeof(BlogService)))
                {
                    service.Open();
                    Console.WriteLine("Press any key to exit");
                    Console.ReadKey();
                    service.Close();
                }
            }
        }
    }
}
