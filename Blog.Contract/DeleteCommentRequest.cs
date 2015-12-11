using System;
using System.Runtime.Serialization;

namespace Blog.Contract
{
    [DataContract]
    public class DeleteCommentRequest
    {
        [DataMember]
        public Guid CommentId { get; set; }
    }
}