using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Blog.BusinessEntities;
using Moq;
using NHibernate.Exceptions;
using Xunit;

namespace Blog.BusinessLogic.Tests
{
    public class NHibernateBlogRepositoryFixture
    {
        [Fact]
        public void CanAddPost()
        {
            var manager = new NHibernateBlogRepository(GetAppSettingsHelper());
            var post = new BlogPost
            {
                Title = "Title",
                Text = "Post text",
                CreateDate = DateTime.Now,
                Description = "some description"
            };
            IList<BlogPost> posts = manager.GetPosts();
            int beforePostCount = posts.Count;
            manager.AddPost(post);
            posts = manager.GetPosts();
            Assert.Equal(beforePostCount + 1, posts.Count);

            BlogPost firstPost = posts.First();
            BlogPost selectedPost = manager.GetPost(firstPost.Id);
            Assert.NotNull(selectedPost);
            Equals(firstPost, selectedPost);
        }

        [Fact]
        public void CanAddPostWithTextLengthLessThat200Characters()
        {
            var manager = new NHibernateBlogRepository(GetAppSettingsHelper());
            var lengths = new int[] { 0, 1, 5, 10, 50, 99, 100, 200 };
            var post = new BlogPost
            {
                Title = "title",
                Description = "description",
                CreateDate = DateTime.Now
            };
            foreach (int length in lengths)
            {
                post.Text = new string('a', length);
                manager.AddPost(post);
            }
        }

        [Fact]
        public void CanAddPostWithTitleLengthLessThat100Characters()
        {
            var manager = new NHibernateBlogRepository(GetAppSettingsHelper());
            var lengths = new int[] { 0, 1, 5, 10, 50, 99, 100 };
            var post = new BlogPost
            {
                Text = "text",
                Description = "description",
                CreateDate = DateTime.Now
            };
            foreach (int length in lengths)
            {
                post.Title = new string('a', length);
                manager.AddPost(post);
            }
        }

        [Fact]
        public void CantAddPostWithTitleLengthMoreThan100Characters()
        {
            const int maxLength = 100;
            var manager = new NHibernateBlogRepository(GetAppSettingsHelper());
            var post = new BlogPost
            {
                Text = "text",
                Description = "description",
                CreateDate = DateTime.Now,
                Title = new string('a', maxLength + 1)
            };
            Assert.Throws<GenericADOException>(() => manager.AddPost(post));
        }

        [Fact]
        public void CantAddPostWithTextLengthMoreThan200Characters()
        {
            const int maxLength = 200;
            var manager = new NHibernateBlogRepository(GetAppSettingsHelper());
            var post = new BlogPost
            {
                Title = "title",
                Description = "description",
                CreateDate = DateTime.Now,
                Text = new string('a', maxLength + 1)
            };
            Assert.Throws<GenericADOException>(() => manager.AddPost(post));
        }

        private void Equals<T>(T t1, T t2)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType.IsPrimitive)
                {
                    object t1val = property.GetValue(t1, null);
                    object t2val = property.GetValue(t2, null);
                    Assert.Equal(t1val, t2val);
                }
            }
        }

        private IAppSettingsHelper GetAppSettingsHelper()
        {
            var mock = new Mock<IAppSettingsHelper>();
            mock.Setup(x => x.GetConnectionString()).Returns(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=BlogService;Integrated Security=True");
            return mock.Object;
        }
    }
}
