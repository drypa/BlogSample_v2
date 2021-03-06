﻿using System;
using System.Management.Automation;
using Blog.Client.Common.Model;

namespace Blog.Client.PowerShellCmdlet
{
    [Cmdlet(VerbsCommon.New, "Comment")]
    public sealed class NewCommentCmdlet : BaseBlogCmdlet
    {
        [Parameter(Mandatory = true, HelpMessage = "Не задан идентификатор сообщения", ValueFromPipelineByPropertyName = true)]
        public Guid PostId { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "Не задан текст коментария", ValueFromPipelineByPropertyName = true)]
        public string Text { get; set; }

        protected override void ProcessRecord()
        {
            GetClient().AddComment(new PostComment { Post = new Post { Id = PostId }, Text = Text });
            WriteObject("comment added");
        }
    }
}
