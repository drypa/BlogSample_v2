using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Blog.ConsoleService.Contract;
using Nelibur.ServiceModel.Services;

namespace Blog.ConsoleService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class BlogService : IBlogService
    {
        public BlogService()
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
        }

        public void Delete(Message message)
        {
            NeliburRestService.ProcessOneWay(message);
        }

        public Message Get(Message message)
        {
            return NeliburRestService.Process(message);
        }

        public void Post(Message message)
        {
            NeliburRestService.ProcessOneWay(message);
        }
    }
}
