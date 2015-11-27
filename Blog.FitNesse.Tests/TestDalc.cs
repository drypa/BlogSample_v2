using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Blog.BusinessEntities;

namespace Blog.FitNesse.Tests
{
    internal class TestDalc
    {
        private readonly string connectionString;

        public TestDalc(string connectionStr)
        {
            connectionString = connectionStr;
        }

        public List<BlogPost> GetPosts()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = connection.CreateCommand())
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
            var posts = new List<BlogPost>();
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
