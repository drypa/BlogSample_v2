using System;
using System.Collections.Generic;
using Blog.BusinessEntities;

namespace Blog.SpecflowTests
{
    internal class PostsComparer : IEqualityComparer<BlogPost>
    {
        public bool Equals(BlogPost x, BlogPost y)
        {
            return x.Id == y.Id &&
                x.Text == y.Text &&
                x.Title == y.Title;
        }

        public int GetHashCode(BlogPost obj)
        {
            return obj.Id.GetHashCode() & obj.Text.GetHashCode() & obj.Title.GetHashCode();
        }
    }
}