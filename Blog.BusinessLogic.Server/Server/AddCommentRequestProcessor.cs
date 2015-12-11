using System;
using Blog.Contract;
using Nelibur.ServiceModel.Services.Operations;

namespace Blog.BusinessLogic.Server.Server
{
    public class AddCommentRequestProcessor : BlogUpdateProcessor, IPostOneWay<AddCommentRequest>
    {
        public AddCommentRequestProcessor(ExchangeConfiguration configuration)
            : base(configuration)
        {
        }

        public void PostOneWay(AddCommentRequest request)
        {
            GetProducer(request).Send(request);
        }
    }
}
