using System;
using System.Management.Automation;

namespace Blog.Client.PowerShellCmdlet
{
    [Cmdlet(VerbsCommon.Get, "Posts")]
    public sealed class GetPostsCmdlet : BaseBlogCmdlet
    {
        protected override void ProcessRecord()
        {
            WriteObject(GetClient().GetPosts()); 
        }
    }
}
