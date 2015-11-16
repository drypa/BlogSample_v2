using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Blog.BusinessEntities
{
    [DataContract]
    public class BlogPost
    {
        [DataMember]
        public virtual IList<Comment> Comments { get; set; }

        [DataMember]
        public virtual DateTime CreateDate { get; set; }

        [DataMember]
        public virtual string Description { get; set; }

        [DataMember]
        public virtual Guid Id { get; set; }

        [DataMember]
        public virtual string Text { get; set; }

        [DataMember]
        public virtual string Title { get; set; }
    }
}
