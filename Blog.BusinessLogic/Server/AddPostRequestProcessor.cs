using System;
using Blog.BusinessEntities.Contract;
using Nelibur.ServiceModel.Services.Operations;

namespace Blog.BusinessLogic.Server
{
    public class AddPostRequestProcessor : BlogUpdateProcessor, IPostOneWay<AddPostRequest>
    {
        public AddPostRequestProcessor(ExchangeConfiguration configuration)
            : base(configuration)
        {
        }

        public void PostOneWay(AddPostRequest request)
        {
            GetProducer(request).Send(request);
        }
    }
}
