using System;
using System.Collections.Generic;

namespace Blog.Client.Common.Model
{
    public class PostDetails : Post
    {
        public List<PostComment> Comments { get; set; }

        public DateTime CreationDate { get; set; }

        public string Text { get; set; }
    }
}
