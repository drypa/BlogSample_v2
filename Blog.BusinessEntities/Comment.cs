using System;
using System.Runtime.Serialization;

namespace Blog.BusinessEntities
{
    [DataContract]
    public class Comment
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public DateTime CreateDate { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public BlogPost Post { get; set; }
    }
}