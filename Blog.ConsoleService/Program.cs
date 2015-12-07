using System;
using Blog.BusinessEntities.Contract;
using Blog.BusinessLogic;
using Blog.BusinessLogic.Server;
using Nelibur.ServiceModel.Services.Operations;
using Ninject;

namespace Blog.ConsoleService
{
    public class Program
    {
        private static IKernel GetNinjectKernel()
        {
            var kernel = new StandardKernel();
            ExchangeConfiguration config = new ExchangeConfigurationProvider().Configuration;
            kernel.Bind<IBlogReader>()
                .To<DapperBlogRepository>();

            kernel.Bind<IAppSettingsHelper>()
                .To<AppSettingsHelper>();

            kernel.Bind<IPostOneWay<AddPostRequest>>()
                .To<AddPostRequestProcessor>()
                .WithConstructorArgument(config);

            kernel.Bind<IPostOneWay<AddCommentRequest>>()
                .To<AddCommentRequestProcessor>()
                .WithConstructorArgument(config);

            kernel.Bind<IDeleteOneWay<DeleteCommentRequest>>()
                .To<DeleteCommentRequestProcessor>()
                .WithConstructorArgument(config);

            kernel.Bind<IDeleteOneWay<DeletePostRequest>>()
                .To<DeletePostRequestProcessor>()
                .WithConstructorArgument(config);

            return kernel;
        }

        private static void Main(string[] args)
        {
            if (args.Length <= 0)
            {
                throw new ArgumentNullException("args", @"не задан URL сервиса");
            }
            using (IKernel kernel = GetNinjectKernel())
            {
                using (BlogServiceProvider serviceProvider = new BlogServiceProvider(args[0], kernel).Open())
                {
                    Console.WriteLine("Press any key to exit");
                    Console.ReadKey();
                    serviceProvider.Close();
                }
            }
        }
    }
}
