using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Blog.BusinessEntities;
using Xunit;

namespace Blog.BusinessLogic.Tests
{
    public class NHibernateBlogServiceFixture
    {
        [Fact]
        public void CanAddPost()
        {
            var service = new NHibernateBlogService();
            var post = new BlogPost
            {
                Title = "Title",
                Text = "Post text",
                CreateDate = DateTime.Now,
                Description = "some description"
            };
            IList<BlogPost> posts = service.GetPosts();
            int beforePostCount = posts.Count;
            service.AddPost(post);
            posts = service.GetPosts();
            Assert.Equal(beforePostCount + 1, posts.Count);

            var firstPost = posts.First();
            var selectedPost = service.GetPost(firstPost.Id);
            Assert.NotNull(selectedPost);
            Equals(firstPost, selectedPost);
        }


        private void Equals<T>(T t1, T t2)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                if (property.PropertyType.IsPrimitive)
                {
                    var t1val = property.GetValue(t1, null);
                    var t2val = property.GetValue(t2, null);
                    Assert.Equal(t1val, t2val);
                }
                
            }
        }
    }
}
