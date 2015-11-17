using System;
using Blog.BusinessEntities;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Blog.BusinessLogic.Mappings
{
    internal class CommentMapping : ClassMapping<Comment>
    {
        public CommentMapping()
        {
            Id(x => x.Id, m => m.Generator(Generators.Guid));
            Property(x => x.Text, mapper => mapper.Column(c=>c.Length(50)));
            Property(x => x.CreateDate);
            ManyToOne(x => x.Post);
        }
    }
}
