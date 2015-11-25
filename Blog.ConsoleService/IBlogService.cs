using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using Nelibur.ServiceModel.Contracts;
using Nelibur.ServiceModel.Services.Operations;

namespace Blog.ConsoleService
{
    [ServiceContract]
    public interface IBlogService
    {
        [OperationContract]
        [WebInvoke(Method = OperationType.Delete,
            UriTemplate = RestServiceMetadata.Path.DeleteOneWay,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        void Delete(Message message);

        [OperationContract]
        [WebInvoke(Method = OperationType.Get,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = RestServiceMetadata.Path.Get,
            ResponseFormat = WebMessageFormat.Json)]
        Message Get(Message message);

        [OperationContract]
        [WebInvoke(Method = OperationType.Post,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = RestServiceMetadata.Path.PostOneWay,
            ResponseFormat = WebMessageFormat.Json)]
        void Post(Message message);
    }
}
