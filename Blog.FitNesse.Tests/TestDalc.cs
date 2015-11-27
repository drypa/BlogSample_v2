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

                    return ReadPosts(cmd.ExecuteReader());
                }
            }
        }

        public List<Comment> GetComments()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = @"SELECT [Id]
                                          ,[CreateDate]
                                          ,[Text]
                                      FROM [BlogServiceTest].[dbo].[Comment]";
                    cmd.CommandType = CommandType.Text;

                    return ReadComments(cmd.ExecuteReader());
                }
            }
        }

        private List<Comment> ReadComments(SqlDataReader reader)
        {
            var comments = new List<Comment>();
            while (reader.Read())
            {
                comments.Add(new Comment
                {
                    CreateDate = (DateTime)reader["CreateDate"],
                    Id = (Guid)reader["Id"],
                    Text = reader["Text"].ToString()
                });
            }
            return comments;
        }

        public void  AddPost(BlogPost post)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = @"INSERT INTO [dbo].[BlogPost]
                                           ([Id]
                                           ,[CreateDate]
                                           ,[Text]
                                           ,[Title])
                                     VALUES
                                           (@id
                                           ,@createDate
                                           ,@text
                                           ,@title)";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = post.Id;
                    cmd.Parameters.Add("@createDate", SqlDbType.DateTime).Value = post.CreateDate;
                    cmd.Parameters.Add("@text", SqlDbType.VarChar,200).Value = post.Text;
                    cmd.Parameters.Add("@title", SqlDbType.VarChar, 100).Value = post.Title;


                    cmd.ExecuteNonQuery();
                }
            }
        }

        private List<BlogPost> ReadPosts(SqlDataReader reader)
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
