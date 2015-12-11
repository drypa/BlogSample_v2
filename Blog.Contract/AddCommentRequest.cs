using System;
using System.Runtime.Serialization;

namespace Blog.Contract
{
    [DataContract]
    public class AddCommentRequest
    {
        [DataMember]
        public Guid PostId { get; set; }
        [DataMember]
        public string Text { get; set; }
    }
}