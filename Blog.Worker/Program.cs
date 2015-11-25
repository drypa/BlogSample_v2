using System;
using System.Collections.Generic;
using Blog.BusinessEntities;
using Blog.BusinessEntities.Contract;
using Blog.BusinessLogic;
using Blog.BusinessLogic.RabbitMQ;

namespace Blog.Worker
{
    internal class Program
    {
        private static readonly IBlogWriter repository = new NHibernateBlogRepository(new AppSettingsHelper());

        private static void Main(string[] args)
        {
            ExchangeConfiguration config = new ExchangeConfigurationProvider().Configuration;
            List<dynamic> consumers = new List<dynamic>(config.Routes.Count);
            try
            {
                foreach (var route in config.Routes)
                {
                    Type consumerType = typeof(Consumer<>);

                    Type generic = consumerType.MakeGenericType(route.Key);

                    Action<dynamic> action = (x) => OnAction(x);
                    object[] parameters = { config.ServerName, config.ExchangeType, route.Value, action };
                    dynamic consumer = Activator.CreateInstance(generic, parameters);
                    consumers.Add(consumer);
                    consumer.Open();

                }
                Console.WriteLine("Press <enter> to exit!");
                Console.ReadLine();
            }
            finally 
            {
                foreach (var consumer in consumers)
                {
                    consumer.Close();
                }
            }

        }

        private static void OnAction(AddCommentRequest request)
        {
            var comment = new Comment { CreateDate = DateTime.Now, Post = new BlogPost { Id = request.PostId }, Text = request.Text };
            repository.AddComment(comment);
            Console.WriteLine("Добавлен коментарий: '{0}' от {1}", comment.Text, comment.CreateDate.ToString());
        }

        private static void OnAction(DeleteCommentRequest request)
        {
            var comment = new Comment { Id = request.CommentId };
            repository.DeleteComment(comment);
            Console.WriteLine("Удалён коментарий: '{0}'", comment.Id);
        }

        private static void OnAction(AddPostRequest request)
        {
            var post = new BlogPost { Text = request.Text, Title = request.Title, CreateDate = DateTime.Now };
            repository.AddPost(post);
            Console.WriteLine("Добавлена статья: '{0}' от {1}", post.Title, post.CreateDate);
        }

        private static void OnAction(DeletePostRequest request)
        {
            var post = new BlogPost { Id = request.PostId };
            repository.DeletePost(post);
            Console.WriteLine("Удалёна статья: '{0}'", post.Id);
        }
    }
}
