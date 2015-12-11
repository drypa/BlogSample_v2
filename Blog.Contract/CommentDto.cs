using System;
using System.Runtime.Serialization;

namespace Blog.Contract
{
    [DataContract]
    public class CommentDto
    {
        [DataMember]
        public DateTime CreateDate { get; set; }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public BlogPostDto Post { get; set; }

        [DataMember]
        public string Text { get; set; }
    }
}
