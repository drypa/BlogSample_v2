using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Blog.Contract
{
    [DataContract]
    public class PostListResponse
    {
        [DataMember]
        public IList<BlogPostDto> Posts { get; set; }
    }
}
