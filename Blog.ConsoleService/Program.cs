using System;
using System.ServiceModel.Web;
using Blog.BusinessEntities.Contract;
using Blog.BusinessLogic;
using Nelibur.ServiceModel.Services;
using Ninject;

namespace Blog.ConsoleService
{
    public class Program
    {
        private static WebServiceHost CreateService(string serviceUrl)
        {
            return new WebServiceHost(typeof(BlogService), new Uri(serviceUrl));
        }

        private static IKernel GetNinjectKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IBlogReader>()
                .To<DapperBlogRepository>();
            kernel.Bind<IAppSettingsHelper>()
                .To<AppSettingsHelper>();
            return kernel;
        }

        private static void Main(string[] args)
        {
            if (args.Length <= 0)
            {
                throw new ArgumentNullException(@"не задан URL сервиса");
            }

            using (IKernel kernel = GetNinjectKernel())
            {
                var instance = new BlogProcessor(kernel.Get<IBlogReader>(), new ExchangeConfigurationProvider().Configuration);
                NeliburRestService.Configure(x =>
                {
                    x.Bind<AddPostRequest, BlogProcessor>(() => instance);
                    x.Bind<DeletePostRequest, BlogProcessor>(() => instance);
                    x.Bind<GetPostRequest, BlogProcessor>(() => instance);
                    x.Bind<GetPostsRequest, BlogProcessor>(() => instance);
                    x.Bind<AddCommentRequest, BlogProcessor>(() => instance);
                    x.Bind<DeleteCommentRequest, BlogProcessor>(() => instance);
                });

                using (WebServiceHost service = CreateService(args[0]))
                {
                    service.Open();
                    Console.WriteLine("Press any key to exit");
                    Console.ReadKey();
                }
            }
        }
    }
}
