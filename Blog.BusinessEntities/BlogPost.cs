using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Blog.BusinessEntities
{
    [DataContract]
    public class BlogPost
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public DateTime CreateDate { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public List<Comment> Comments { get; set; }
    }
}
