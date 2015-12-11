using System;
using System.Management.Automation;
using Blog.Client.Common.Model;

namespace Blog.Client.PowerShellCmdlet
{
    [Cmdlet(VerbsCommon.Remove, "Post")]
    public sealed class RemovePostCmdlet : BaseBlogCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 1)]
        public Guid PostId { get; set; }

        protected override void ProcessRecord()
        {
            GetClient().DeletePost(new Post { Id = PostId });
            WriteObject("post removed");
        }
    }
}
