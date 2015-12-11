using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Blog.BusinessEntities;
using Blog.Client.Common.Model;

namespace Blog.Test.Common
{
    public class TestRepository
    {
        private readonly string connectionString;

        public TestRepository(string connectionStr)
        {
            connectionString = connectionStr;
        }

        public void AddComment(PostComment comment)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = @"INSERT INTO [dbo].[Comment]
                                           ([Id]
                                           ,[CreateDate]
                                           ,[Post]
                                           ,[Text])
                                     VALUES
                                           (@id
                                           ,@date
                                           ,@postId
                                           ,@text)";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = comment.Id;
                    cmd.Parameters.Add("@date", SqlDbType.DateTime).Value = comment.CreationDate;
                    cmd.Parameters.Add("@text", SqlDbType.VarChar, 200).Value = comment.Text;
                    cmd.Parameters.Add("@postId", SqlDbType.UniqueIdentifier).Value = comment.Post.Id;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CleanDatabase()
        {
            CleanComments();
            CleanPosts();
        }

        public void CleanComments()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "truncate table [dbo].[Comment];";
                    command.ExecuteNonQuery();
                }
            }
        }

        public void CleanPosts()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "delete from [dbo].[BlogPost];";
                    command.ExecuteNonQuery();
                }
            }
        }


        public void AddPost(PostDetails post)
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
                    cmd.Parameters.Add("@createDate", SqlDbType.DateTime).Value = post.CreationDate;
                    cmd.Parameters.Add("@text", SqlDbType.VarChar, 200).Value = post.Text;
                    cmd.Parameters.Add("@title", SqlDbType.VarChar, 100).Value = post.Title;

                    cmd.ExecuteNonQuery();
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
