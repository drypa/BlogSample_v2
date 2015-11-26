using System;
using System.Management.Automation;
using Blog.BusinessEntities;

namespace Blog.Client.PowerShellCmdlet
{
    [Cmdlet(VerbsCommon.New, "Post")]
    public sealed class NewPostCmdlet : BaseBlogCmdlet
    {
        [Parameter(Mandatory = true, HelpMessage = "Не задан текст сообщения", ValueFromPipelineByPropertyName = true)]
        public string Text { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "Не задан заголовок сообщения", ValueFromPipelineByPropertyName = true)]
        public string Title { get; set; }

        protected override void ProcessRecord()
        {
            GetClient().AddPost(new BlogPost { Text = Text, Title = Title });
            WriteObject("post added");
        }
    }
}
