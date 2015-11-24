using System;
using System.Runtime.Serialization;

namespace Blog.Service.Contract
{
    [DataContract]
    public class DeletePostRequest
    {
        [DataMember]
        public Guid PostId { get; set; }
    }
}
