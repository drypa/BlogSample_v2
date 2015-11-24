using System;
using System.Runtime.Serialization;

namespace Blog.ConsoleService.Contract
{
    [DataContract]
    public class GetPostRequest
    {
        [DataMember]
        public Guid PostId { get; set; }
    }
}