using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Blog.BusinessEntities.Contract
{
    [DataContract]
    public class PostListResponse
    {
        [DataMember]
        public IList<BlogPost> Posts { get; set; }
    }
}
