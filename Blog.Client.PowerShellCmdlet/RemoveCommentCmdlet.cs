using System;
using System.Management.Automation;
using Blog.BusinessEntities;

namespace Blog.Client.PowerShellCmdlet
{
    [Cmdlet(VerbsCommon.Remove, "Comment")]
    public sealed class RemoveCommentCmdlet : BaseBlogCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 1)]
        public Guid CommentId { get; set; }

        protected override void ProcessRecord()
        {
            GetClient().DeleteComment(new Comment { Id = CommentId });
            WriteObject("comment removed");
        }
    }
}
