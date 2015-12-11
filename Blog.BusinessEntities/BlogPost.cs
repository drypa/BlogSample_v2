using System;
using System.Collections.Generic;

namespace Blog.BusinessEntities
{
    public class BlogPost
    {
        public virtual IList<Comment> Comments { get; set; }

        public virtual DateTime CreateDate { get; set; }

        public virtual string Description { get; set; }

        public virtual Guid Id { get; set; }

        public virtual string Text { get; set; }

        public virtual string Title { get; set; }
    }
}
