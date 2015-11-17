using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Blog.BusinessEntities;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Blog.BusinessLogic
{
    public class DapperBlogManager : IBlogManager
    {
        private readonly string selectPostQuery = @"SELECT [Id]
      ,[CreateDate]
      ,[Description]
      ,[Text]
      ,[Title]
  FROM [BlogService].[dbo].[BlogPost]
";

        public void AddComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public void AddPost(BlogPost post)
        {
            throw new NotImplementedException();
        }

        public void DeletePost(BlogPost post)
        {
            throw new NotImplementedException();
        }

        public BlogPost GetPost(Guid postId)
        {
            using (SqlConnection connection = GetReadyConnection())
            {
                return connection.Get<BlogPost>(postId);
            }
        }

        public IList<BlogPost> GetPosts()
        {
            using (SqlConnection connection = GetReadyConnection())
            {
                return connection.Query<BlogPost>(selectPostQuery).AsList();
            }
        }

        private SqlConnection GetReadyConnection()
        {
            var connection = new SqlConnection(new AppSettingsHelper().GetConnectionString("BlogDb"));
            connection.Open();
            return connection;
        }
    }
}
