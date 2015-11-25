using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Blog.BusinessEntities;
using Dapper;
using Ninject;

namespace Blog.BusinessLogic
{
    public class DapperBlogRepository : IBlogReader
    {
        private readonly IAppSettingsHelper appSettings;

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
        private readonly string selectPostsQuery = @"SELECT [Id]
      ,[CreateDate]
      ,[Description]
      ,[Text]
      ,[Title]
  FROM [dbo].[BlogPost]
";

        [Inject]
        public DapperBlogRepository(IAppSettingsHelper appSettingsHelper)
        {
            appSettings = appSettingsHelper;
        }

        public BlogPost GetPost(Guid postId)
        {
            using (SqlConnection connection = GetOpenConnection())
            {
                SqlMapper.GridReader data = connection.QueryMultiple(selectPostWithCommentsQuery, new { postId });
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
            using (SqlConnection connection = GetOpenConnection())
            {
                return connection.Query<BlogPost>(selectPostsQuery).AsList();
            }
        }

        private SqlConnection GetOpenConnection()
        {
            var connection = new SqlConnection(appSettings.GetConnectionString());
            connection.Open();
            return connection;
        }
    }
}
