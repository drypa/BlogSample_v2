using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Blog.BusinessLogic;
using Blog.ConsoleService.Contract;
using Nelibur.ServiceModel.Services;
using Ninject;

namespace Blog.ConsoleService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class BlogService : IBlogService
    {

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
