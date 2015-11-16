using System;
using System.Collections.Generic;
using Blog.BusinessEntities;
using NHibernate;

namespace Blog.BusinessLogic
{
    public class NHibernateBlogService : IBlogService
    {
        private static ISessionFactory sessionFactory;


        private static ISessionFactory SessionFactory
        {
            get { return sessionFactory; }
            set { sessionFactory = value; }
        }

        private NHibernateConfigurator _configurator;
        private NHibernateConfigurator Configurator
        {
            get { return _configurator ?? (_configurator = new NHibernateConfigurator()); }
        }




        public void AddPost(BlogPost post)
        {
            throw new NotImplementedException();
        }

        public List<BlogPost> GetPosts()
        {
            throw new NotImplementedException();
        }

        public BlogPost GetPost(Guid postId)
        {
            throw new NotImplementedException();
        }

        public void DeletePost(Guid postId)
        {
            throw new NotImplementedException();
        }
    }
}
