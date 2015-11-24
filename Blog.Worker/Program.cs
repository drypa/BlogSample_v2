﻿using System;
using Blog.BusinessEntities;
using Blog.BusinessEntities.Contract;
using Blog.BusinessLogic;
using Blog.BusinessLogic.RabbitMQ;

namespace Blog.Worker
{
    internal class Program
    {
        private static readonly NHibernateBlogRepository repository = new NHibernateBlogRepository(new AppSettingsHelper());

        private static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.Error.WriteLine("Необходимо запускать с параметром <routing>");
                Console.ReadLine();
                return;
            }
            ExchangeConfiguration config = new ExchangeConfigurationProvider().Configuration;
            string routingName = args[0];

            Type t = config.GetMessageTypeForRoute(routingName);
            Type consumerType = typeof(Consumer<>);
            Type[] typeArgs = { t };
            Type generic = consumerType.MakeGenericType(typeArgs);

            Action<dynamic> action = OnAction;
            object[] parameters = { config.ServerName, string.Empty, config.ExchangeType, routingName, action };
            using (dynamic consumer = Activator.CreateInstance(generic, parameters))
            {
                Console.WriteLine("Press <enter> to exit!");
                Console.ReadLine();
            }
        }

        private static void OnAction(object obj)
        {
            throw new NotSupportedException();
        }

        private static void OnAction(AddCommentRequest request)
        {
            var comment = new Comment { CreateDate = DateTime.Now, Post = new BlogPost { Id = request.PostId }, Text = request.Text };
            repository.AddComment(comment);
            Console.WriteLine("Добавлен коментарий: '{0}' от {1}", comment.Text, comment.CreateDate.ToString());
        }
    }
}
