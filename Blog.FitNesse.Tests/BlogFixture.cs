using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using Blog.BusinessEntities;
using Blog.BusinessLogic.Client;
using fitlibrary;

namespace Blog.FitNesse.Tests
{
    public class BlogFixture : DoFixture
    {


        public void AddPost()
        {
            IBlogClient client = new BlogClient(Commands.ServiceUrl);
            var postToAdd = new BlogPost { Text = "text", Title = "title" };
            client.AddPost(postToAdd);
            WaitUntilPostAdded(postToAdd);
        }

        private void WaitUntilPostAdded(BlogPost post)
        {
            while (!GetPosts().Any(x => x.Text == post.Text && x.Title == post.Title))
            {
                Thread.Sleep(100);
            }
        }


        private List<BlogPost> GetPosts()
        {

            using (var connection = new SqlConnection(Commands.ConnectionString))
            {
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = @"SELECT [Id]
                                          ,[CreateDate]
                                          ,[Description]
                                          ,[Text]
                                          ,[Title]
                                      FROM [BlogServiceTest].[dbo].[BlogPost]";
                    cmd.CommandType = CommandType.Text;
                    
                    return Read(cmd.ExecuteReader());
                }
            }
        }

        private List<BlogPost> Read(SqlDataReader reader)
        {
            List<BlogPost> posts = new List<BlogPost>();
            while (reader.Read())
            {
                posts.Add(new BlogPost
                {
                    CreateDate = (DateTime)reader["CreateDate"],
                    Id = (Guid)reader["Id"],
                    Text = reader["Text"].ToString(),
                    Title = reader["Title"].ToString()
                });
                
            }
            return posts;
        }

    }
}
