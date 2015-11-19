using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Blog.BusinessLogic;
using Ninject;

namespace Blog.Service
{
    public class BlogServiceHost : ServiceHost
    {
        public BlogServiceHost(IKernel kernel, Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            foreach (ContractDescription cd in ImplementedContracts.Values)
            {
                cd.Behaviors.Add(new BlogInstanceProvider(kernel));
            }
        }
    }
}
