using System;
using Blog.BusinessEntities.Contract;
using Nelibur.ServiceModel.Services.Operations;

namespace Blog.BusinessLogic.Server.Server
{
    public class DeleteCommentRequestProcessor : BlogUpdateProcessor, IDeleteOneWay<DeleteCommentRequest>
    {
        public DeleteCommentRequestProcessor(ExchangeConfiguration configuration)
            : base(configuration)
        {
        }

        public void DeleteOneWay(DeleteCommentRequest request)
        {
            GetProducer(request).Send(request);
        }
    }
}
