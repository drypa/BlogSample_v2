using System;
using System.Management.Automation;
using Blog.Client.Common;

namespace Blog.Client.PowerShellCmdlet
{
    public abstract class BaseBlogCmdlet : Cmdlet
    {
        protected BlogClientController GetClient()
        {
            var blogClientController = new BlogClientController(CmdletSettingsHelper.SeviceUrl());
            return blogClientController;
        }
    }
}
