using System;
using System.Runtime.Serialization;

namespace Blog.Service.Contract
{
    [DataContract]
    public class GetPostRequest
    {
        [DataMember]
        public Guid PostId { get; set; }
    }
}