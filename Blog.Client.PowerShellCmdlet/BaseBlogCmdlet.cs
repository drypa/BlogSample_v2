using System;
using System.Management.Automation;
using Blog.BusinessLogic.Client;

namespace Blog.Client.PowerShellCmdlet
{
    public abstract class BaseBlogCmdlet : Cmdlet
    {
        protected IBlogClient GetClient()
        {
            IBlogClient blogClient = new BlogClient(CmdletSettingsHelper.SeviceUrl());
            return blogClient;
        }
    }
}
