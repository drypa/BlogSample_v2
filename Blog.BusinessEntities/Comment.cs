using System;

namespace Blog.BusinessEntities
{
    public class Comment
    {
        public virtual DateTime CreateDate { get; set; }

        public virtual Guid Id { get; set; }

        public virtual BlogPost Post { get; set; }

        public virtual string Text { get; set; }
    }
}
