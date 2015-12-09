using System;
using Blog.BusinessEntities.Contract;
using Nelibur.ServiceModel.Services.Operations;

namespace Blog.BusinessLogic.Server.Server
{
    public class DeletePostRequestProcessor : BlogUpdateProcessor, IDeleteOneWay<DeletePostRequest>
    {
        public DeletePostRequestProcessor(ExchangeConfiguration configuration)
            : base(configuration)
        {
        }

        public void DeleteOneWay(DeletePostRequest request)
        {
            GetProducer(request).Send(request);
        }
    }
}
