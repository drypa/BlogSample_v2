using System;
using Blog.BusinessEntities;
using NHibernate.Mapping.ByCode.Conformist;

namespace Blog.BusinessLogic.Mappings
{
    internal class BlogPostMapping : ClassMapping<BlogPost>
    {
        public BlogPostMapping()
        {
            Id(x => x.Id);
            Property(x => x.Text);
            Property(x => x.Title);
            Property(x => x.CreateDate);
            Property(x => x.Description);
            Bag(x => x.Comments, cm => { }, cr => cr.OneToMany(y => y.Class(typeof(Comment))));
        }
    }
}
