using System;
using System.ServiceModel.Web;
using Blog.BusinessEntities.Contract;
using Nelibur.ServiceModel.Services;
using Ninject;

namespace Blog.BusinessLogic.Server
{
    public class BlogServiceProvider : IDisposable
    {
        private readonly IKernel ninjectKernel;
        private readonly WebServiceHost serviceHost;

        public BlogServiceProvider(string serviceUrl, IKernel ninject)
        {
            ninjectKernel = ninject;
            serviceHost = new WebServiceHost(typeof(BlogService), new Uri(serviceUrl));
        }

        public void Close()
        {
            Dispose();
        }

        public BlogServiceProvider Open()
        {
            var instance = new BlogProcessor(ninjectKernel.Get<IBlogReader>(), new ExchangeConfigurationProvider().Configuration);
            ConfigureRestService(instance);
            serviceHost.Open();
            return this;
        }

        public void Dispose()
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
            }
        }

        private void ConfigureRestService(BlogProcessor processor)
        {
            NeliburRestService.Configure(x =>
            {
                x.Bind<AddPostRequest, BlogProcessor>(() => processor);
                x.Bind<DeletePostRequest, BlogProcessor>(() => processor);
                x.Bind<GetPostRequest, BlogProcessor>(() => processor);
                x.Bind<GetPostsRequest, BlogProcessor>(() => processor);
                x.Bind<AddCommentRequest, BlogProcessor>(() => processor);
                x.Bind<DeleteCommentRequest, BlogProcessor>(() => processor);
            });
        }
    }
}
