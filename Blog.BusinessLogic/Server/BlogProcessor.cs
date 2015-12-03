using System;
using Blog.BusinessEntities.Contract;
using Blog.BusinessLogic.RabbitMQ;
using Nelibur.ServiceModel.Services.Operations;

namespace Blog.BusinessLogic.Server
{
    public class BlogProcessor :
        IPostOneWay<AddPostRequest>,
        IPostOneWay<AddCommentRequest>,
        IDeleteOneWay<DeletePostRequest>,
        IDeleteOneWay<DeleteCommentRequest>,
        IGet<GetPostRequest>,
        IGet<GetPostsRequest>
    {
        private readonly ExchangeConfiguration exchangeConfiguration;
        private readonly IBlogReader reader;

        public BlogProcessor(IBlogReader readRepository, ExchangeConfiguration configuration)
        {
            reader = readRepository;
            exchangeConfiguration = configuration;
        }

        public void DeleteOneWay(DeleteCommentRequest request)
        {
            GetProducer(request).Send(request);
        }

        public void DeleteOneWay(DeletePostRequest request)
        {
            GetProducer(request).Send(request);
        }

        public object Get(GetPostRequest request)
        {
            return reader.GetPost(request.PostId);
        }

        public object Get(GetPostsRequest request)
        {
            return new PostListResponse
            {
                Posts = reader.GetPosts()
            };
        }

        public void PostOneWay(AddCommentRequest request)
        {
            GetProducer(request).Send(request);
        }

        public void PostOneWay(AddPostRequest request)
        {
            GetProducer(request).Send(request);
        }

        private Producer<T> GetProducer<T>(T request) where T : new()
        {
            return new Producer<T>(exchangeConfiguration.ServerName, exchangeConfiguration.ExchangeType, exchangeConfiguration.GetRouting(request.GetType()));
        }
    }
}
