using System;
using System.Runtime.Serialization;

namespace Blog.BusinessEntities
{
    [DataContract]
    public class Comment
    {
        [DataMember]
        public virtual DateTime CreateDate { get; set; }

        [DataMember]
        public virtual Guid Id { get; set; }

        [DataMember]
        public virtual BlogPost Post { get; set; }

        [DataMember]
        public virtual string Text { get; set; }
    }
}
