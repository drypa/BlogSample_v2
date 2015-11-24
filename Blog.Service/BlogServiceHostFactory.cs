using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Blog.BusinessLogic;
using Ninject;

namespace Blog.Service
{
    public class BlogServiceHostFactory : ServiceHostFactory
    {

        private readonly IKernel kernel;
        public BlogServiceHostFactory()
        {
            kernel = new StandardKernel();
            kernel.Bind<IAppSettingsHelper>()
                .To<AppSettingsHelper>();
            kernel.Bind<IBlogRepository>()
                .To<DapperBlogRepository>()
                .Named("read");
            kernel.Bind<IBlogRepository>()
                .To<NHibernateBlogRepository>()
                .Named("write");

            kernel.Bind<IBlogService>()
                .To<BlogService>();
            //                .WithConstructorArgument("readRepository", kernel.Get<IBlogRepository>("read"))
            //                .WithConstructorArgument("writeRepository", kernel.Get<IBlogRepository>("write"));

        }

        protected override ServiceHost CreateServiceHost(Type serviceType,
            Uri[] baseAddresses)
        {
            return new BlogServiceHost(kernel, serviceType, baseAddresses);
        }
    }
}
