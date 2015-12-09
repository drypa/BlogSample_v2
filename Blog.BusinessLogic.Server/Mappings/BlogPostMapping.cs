using System;
using Blog.BusinessEntities;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Blog.BusinessLogic.Server.Mappings
{
    internal class BlogPostMapping : ClassMapping<BlogPost>
    {
        public BlogPostMapping()
        {
            Id(x => x.Id, m => m.Generator(Generators.Guid));
            Property(x => x.Text, mapper => mapper.Column(c => c.Length(200)));
            Property(x => x.Title, mapper => mapper.Column(c => c.Length(100)));
            Property(x => x.CreateDate);
            Property(x => x.Description, mapper => mapper.Column(c => c.Length(50)));
            Bag(x => x.Comments, cm => { }, cr => cr.OneToMany(y => y.Class(typeof(Comment))));
        }
    }
}
