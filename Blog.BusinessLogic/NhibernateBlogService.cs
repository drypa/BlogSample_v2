using System;
using System.Collections.Generic;
using System.Linq;
using Blog.BusinessEntities;
using NHibernate;

namespace Blog.BusinessLogic
{
    public class NHibernateBlogService : IBlogService
    {
        private NHibernateConfigurator _configurator;

        private NHibernateConfigurator Configurator
        {
            get { return _configurator ?? (_configurator = new NHibernateConfigurator()); }
        }

        /// <summary>
        /// Получение списка статей с поддержкой пейджинга
        /// </summary>
        /// <param name="first"></param>
        /// <param name="count"></param>
        /// <returns>список статей</returns>
        public List<BlogPost> GetPosts(int first, int count)
        {
            //TODO: реализовать с помощью Dapper
            using (ISession session = OpenSession())
            {
                ICriteria criteria = session.CreateCriteria<BlogPost>().SetFirstResult(first).SetMaxResults(count);
                return criteria.List<BlogPost>().ToList();
            }
        }

        /// <summary>
        /// Добавить новую статью
        /// </summary>
        /// <param name="post">Статья для добавления</param>
        public void AddPost(BlogPost post)
        {
            using (ISession session = OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(post);

                    transaction.Commit();
                }
            }
        }

        public void DeletePost(Guid postId)
        {
            throw new NotImplementedException();
        }

        public BlogPost GetPost(Guid postId)
        {
            //TODO: реализовать с помощью Dapper
            using (ISession session = OpenSession())
            {
                return session.Get<BlogPost>(postId);
            }
        }

        /// <summary>
        /// Получение всех статей
        /// </summary>
        /// <returns>список всех статей</returns>
        public IList<BlogPost> GetPosts()
        {
            //TODO: реализовать с помощью Dapper
            using (ISession session = OpenSession())
            {
                return session.QueryOver<BlogPost>().List<BlogPost>();
            }
        }

        private ISession OpenSession()
        {
            return Configurator.GetSessionFactory().OpenSession();
        }
    }
}
