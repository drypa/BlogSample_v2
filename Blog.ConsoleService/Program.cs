using System;
using Blog.BusinessLogic;
using Blog.BusinessLogic.Server;
using Ninject;

namespace Blog.ConsoleService
{
    public class Program
    {
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
            using (BlogServiceProvider serviceProvider = new BlogServiceProvider(args[0], kernel).Open())
            {
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
                serviceProvider.Close();
            }
        }
    }
}
