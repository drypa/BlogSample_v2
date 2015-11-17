using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Blog.BusinessEntities;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Blog.BusinessLogic
{
    public class DapperBlogManager : IBlogManager
    {
        private readonly string selectPostsQuery = @"SELECT [Id]
      ,[CreateDate]
      ,[Description]
      ,[Text]
      ,[Title]
  FROM [BlogService].[dbo].[BlogPost]
";
        private readonly string selectPostWithCommentsQuery = @"select [Id]
      ,[CreateDate]
      ,[Description]
      ,[Text]
      ,[Title] 
  from [dbo].[BlogPost] where Id =  @postId;
  
  select [Id]
      ,[CreateDate]
      ,[Text]
  from [dbo].[Comment] where Post = @postId;
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
                var data = connection.QueryMultiple(selectPostWithCommentsQuery, new { postId });
                BlogPost resultPost = data.Read<BlogPost>().SingleOrDefault();
                if (resultPost != null)
                {
                    resultPost.Comments = data.Read<Comment>().ToList();
                }
                return resultPost;
            }
        }

        public IList<BlogPost> GetPosts()
        {
            using (SqlConnection connection = GetReadyConnection())
            {
                return connection.Query<BlogPost>(selectPostsQuery).AsList();
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
