using System;
using System.ServiceModel.Web;
using Blog.BusinessEntities.Contract;
using Nelibur.ServiceModel.Services;
using Nelibur.ServiceModel.Services.Operations;
using Ninject;

namespace Blog.BusinessLogic.Server.Server
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
            var instance = new GetPostBlogProcessor(ninjectKernel.Get<IBlogReader>());
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

        private void ConfigureRestService(GetPostBlogProcessor processor)
        {
            var addPost = ninjectKernel.Get<IPostOneWay<AddPostRequest>>();
            var addComment = ninjectKernel.Get<IPostOneWay<AddCommentRequest>>();
            var deletePost = ninjectKernel.Get<IDeleteOneWay<DeletePostRequest>>();
            var deleteComment = ninjectKernel.Get<IDeleteOneWay<DeleteCommentRequest>>();

            NeliburRestService.Configure(x =>
            {
                x.Bind<AddPostRequest, IPostOneWay<AddPostRequest>>(() => addPost);
                x.Bind<DeletePostRequest, IDeleteOneWay<DeletePostRequest>>(() => deletePost);
                x.Bind<GetPostRequest, GetPostBlogProcessor>(() => processor);
                x.Bind<GetPostsRequest, GetPostBlogProcessor>(() => processor);
                x.Bind<AddCommentRequest, IPostOneWay<AddCommentRequest>>(() => addComment);
                x.Bind<DeleteCommentRequest, IDeleteOneWay<DeleteCommentRequest>>(() => deleteComment);
            });
        }
    }
}
