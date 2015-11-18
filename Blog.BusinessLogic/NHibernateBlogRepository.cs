using System;
using System.Collections.Generic;
using System.Linq;
using Blog.BusinessEntities;
using NHibernate;

namespace Blog.BusinessLogic
{
    public class NHibernateBlogRepository : IBlogRepository
    {
        private readonly IAppSettingsHelper _appSettingsHelper;
        private NHibernateConfigurator _configurator;

        public NHibernateBlogRepository(IAppSettingsHelper appSettingsHelper)
        {
            _appSettingsHelper = appSettingsHelper;
        }

        private NHibernateConfigurator Configurator
        {
            get { return _configurator ?? (_configurator = new NHibernateConfigurator(_appSettingsHelper)); }
        }

        /// <summary>
        /// Получение списка статей с поддержкой пейджинга
        /// </summary>
        /// <param name="first"></param>
        /// <param name="count"></param>
        /// <returns>список статей</returns>
        public List<BlogPost> GetPosts(int first, int count)
        {
            using (ISession session = OpenSession())
            {
                ICriteria criteria = session.CreateCriteria<BlogPost>().SetFirstResult(first).SetMaxResults(count);
                return criteria.List<BlogPost>().ToList();
            }
        }

        public void AddComment(Comment comment)
        {
            using (ISession session = OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(comment);

                    transaction.Commit();
                }
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

        public void DeletePost(BlogPost post)
        {
            using (ISession session = OpenSession())
            {
                session.Delete(post);
                session.Flush();
            }
        }

        public BlogPost GetPost(Guid postId)
        {
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
