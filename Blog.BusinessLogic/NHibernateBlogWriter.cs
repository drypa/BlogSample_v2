using System;
using Blog.BusinessEntities;
using NHibernate;

namespace Blog.BusinessLogic
{
    public class NHibernateBlogWriter : IBlogWriter
    {
        private readonly IAppSettingsHelper appSettings;
        private NHibernateConfigurator configurator;


        public NHibernateBlogWriter(IAppSettingsHelper appSettingsHelper)
        {
            appSettings = appSettingsHelper;
        }

        private NHibernateConfigurator Configurator
        {
            get { return configurator ?? (configurator = new NHibernateConfigurator(appSettings)); }
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

        public void DeleteComment(Comment comment)
        {
            using (ISession session = OpenSession())
            {
                session.Delete(comment);
                session.Flush();
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

        private ISession OpenSession()
        {
            return Configurator.GetSessionFactory().OpenSession();
        }
    }
}
