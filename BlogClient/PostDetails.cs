using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Blog.Client
{
    public class PostDetails : Post
    {
        public ICommand AddCommentCommand
        {
            get { throw new NotImplementedException(); }
        }

        public List<PostComment> Comments { get; set; }
        public DateTime CreationDate { get; set; }

        public ICommand DeleteCommentCommand
        {
            get { throw new NotImplementedException(); }
        }

        public string Text { get; set; }
    }
}
