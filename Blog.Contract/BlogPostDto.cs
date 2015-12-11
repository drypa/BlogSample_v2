using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Blog.Contract
{
    [DataContract]
    public class BlogPostDto
    {
        [DataMember]
        public IList<CommentDto> Comments { get; set; }

        [DataMember]
        public DateTime CreateDate { get; set; }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public string Title { get; set; }
    }
}
