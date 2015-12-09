using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Blog.BusinessEntities;
using Blog.BusinessLogic.Common;
using Blog.BusinessLogic.Server;
using Moq;
using NHibernate.Exceptions;
using Xunit;
using Xunit.Extensions;

namespace Blog.BusinessLogic.Tests
{
    public class NHibernateBlogRepositoryFixture : IDisposable
    {
        private const string postText = "text";
        private const string postTitle = "title";

        public NHibernateBlogRepositoryFixture()
        {
            CreateDbSchema();
            Cleanup();
        }

        [Fact]
        public void CanAddPost()
        {
            var manager = new NHibernateBlogRepository(GetAppSettingsHelper());
            BlogPost post = CreatePost(postTitle, postText);
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

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(99)]
        [InlineData(199)]
        [InlineData(200)]
        public void CanAddPostWithTextLengthLessThat200Characters(int textLength)
        {
            var manager = new NHibernateBlogRepository(GetAppSettingsHelper());
            BlogPost post = CreatePost(postTitle, GetString(textLength));
            manager.AddPost(post);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(99)]
        [InlineData(100)]
        public void CanAddPostWithTitleLengthLessThat100Characters(int titleLength)
        {
            var manager = new NHibernateBlogRepository(GetAppSettingsHelper());
            BlogPost post = CreatePost(GetString(titleLength), postText);

            manager.AddPost(post);
        }

        [Fact]
        public void CantAddPostWithTextLengthMoreThan200Characters()
        {
            const int maxLength = 200;
            var manager = new NHibernateBlogRepository(GetAppSettingsHelper());
            BlogPost post = CreatePost(postTitle, GetString(maxLength + 1));
            Assert.Throws<GenericADOException>(() => manager.AddPost(post));
        }

        [Fact]
        public void CantAddPostWithTitleLengthMoreThan100Characters()
        {
            const int maxLength = 100;
            var manager = new NHibernateBlogRepository(GetAppSettingsHelper());
            BlogPost post = CreatePost(GetString(maxLength + 1),postText);
            Assert.Throws<GenericADOException>(() => manager.AddPost(post));
        }

        public void Dispose()
        {
            Cleanup();
        }

        private void Cleanup()
        {
            using (var connection = new SqlConnection(GetAppSettingsHelper().GetConnectionString()))
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = @"truncate table [dbo].[Comment]; delete from [dbo].[BlogPost]";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void CreateDbSchema()
        {
            NHibernateConfigurator.BuildConfiguration(GetAppSettingsHelper().GetConnectionString());
        }

        private BlogPost CreatePost(string title, string text)
        {
            return new BlogPost
            {
                Title = title,
                CreateDate = DateTime.Now,
                Text = text
            };
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
            mock.Setup(x => x.GetConnectionString()).Returns(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=BlogServiceTest;Integrated Security=False;User ID=tests_user;Password=tests_user");
            return mock.Object;
        }

        private string GetString(int stringLength)
        {
            return new string('a', stringLength);
        }
    }
}
