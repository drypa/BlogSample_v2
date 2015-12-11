using System;
using System.Collections.Generic;
using Blog.Client.Common.Model;

namespace Blog.Specflow.Tests
{
    internal class PostsComparer : IEqualityComparer<Post>
    {
        public bool Equals(Post x, Post y)
        {
            return x.Id == y.Id &&
                x.Title == y.Title;
        }

        public int GetHashCode(Post obj)
        {
            return obj.Id.GetHashCode() & obj.Title.GetHashCode();
        }
    }
}