using System;
using Blog.BusinessEntities.Contract;

namespace Blog.BusinessLogic
{
    public class ExchangeConfigurationProvider
    {
        private readonly ExchangeConfiguration configuration;

        public ExchangeConfigurationProvider()
        {
            configuration = new ExchangeConfiguration("localhost", "blog")
                .AddRouting(typeof(DeleteCommentRequest), "deleteComment")
                .AddRouting(typeof(DeletePostRequest), "deletePost")
                .AddRouting(typeof(AddCommentRequest), "addComment")
                .AddRouting(typeof(AddPostRequest), "addPost");
        }

        public ExchangeConfiguration Configuration
        {
            get { return configuration; }
        }
    }
}
