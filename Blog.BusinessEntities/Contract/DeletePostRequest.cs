using System;
using System.Runtime.Serialization;

namespace Blog.BusinessEntities.Contract
{
    [DataContract]
    public class DeletePostRequest
    {
        [DataMember]
        public Guid PostId { get; set; }
    }
}
