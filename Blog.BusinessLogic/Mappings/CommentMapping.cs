using System;
using Blog.BusinessEntities;
using NHibernate.Mapping.ByCode.Conformist;

namespace Blog.BusinessLogic.Mappings
{
    internal class CommentMapping : ClassMapping<Comment>
    {
        public CommentMapping()
        {
            Id(x=>x.Id);
            Property(x=>x.Text);
            Property(x=>x.CreateDate);
            ManyToOne(x=>x.Post);
        }
    }
}
