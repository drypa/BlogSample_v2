using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Windows.Input;
using Blog.BusinessEntities;
using Blog.Client.Annotations;

namespace Blog.Client.Models
{
    public sealed class BlogViewModel : INotifyPropertyChanged
    {
        private readonly Action<string> notification;
        private readonly string serviceUrl;

        private PostDetails currentPost;
        private bool hasNewPost;
        private List<Post> posts;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="service">Адрес сервиса для получения данных</param>
        /// <param name="alert">Делегат, который будет использоваться для отображения сообщений пользователю</param>
        public BlogViewModel(string service, Action<string> alert)
        {
            serviceUrl = service;
            notification = alert;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand AddCommentCommand
        {
            get { return new RelayCommand(AddComment, x => true); }
        }

        public ICommand AddPostCommand
        {
            get { return new RelayCommand(x => AddNewPost(), x => HasNewPost); }
        }

        public PostDetails CurrentPost
        {
            get { return currentPost; }
            set
            {
                if (currentPost != value)
                {
                    currentPost = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand DeleteCommentCommand
        {
            get { return new RelayCommand(DeleteComment, x => true); }
        }

        public ICommand DeletePostCommand
        {
            get { return new RelayCommand(DeletePost, x => true); }
        }

        public bool HasNewPost
        {
            get { return hasNewPost; }
            set
            {
                if (hasNewPost != value)
                {
                    hasNewPost = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand NewPostCommand
        {
            get { return new RelayCommand(x => CreateNewPost(), x => true); }
        }

        public List<Post> Posts
        {
            get
            {
                if (posts == null)
                {
                    posts = GetPosts();
                    OnPropertyChanged();
                }
                return posts;
            }
            set
            {
                if (posts != value)
                {
                    posts = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SelectEmptyPostCommand
        {
            get { return new RelayCommand(x => LoadPostDetails(new Post()), x => true); }
        }

        public ICommand SelectPostCommand
        {
            get { return new RelayCommand(LoadPostDetails, x => true); }
        }

        private void AddComment(object obj)
        {
        }

        private void AddNewPost()
        {
            BlogPost model = CurrentPost.ToModel();
            model.CreateDate = DateTime.Now;
            using (ChannelFactory<IBlogService> factory = CreateChanelFactory())
            {
                factory.CreateChannel().AddPost(model);
            }
            Posts = GetPosts();
            HasNewPost = false;
        }

        private ChannelFactory<IBlogService> CreateChanelFactory()
        {
            var factory = new ChannelFactory<IBlogService>(
                new WebHttpBinding(), serviceUrl);
            factory.Endpoint.Behaviors.Add(new WebHttpBehavior());
            return factory;
        }

        private void CreateNewPost()
        {
            HasNewPost = true;
            CurrentPost = new PostDetails();
        }

        private void DeleteComment(object obj)
        {
        }

        private void DeletePost(object obj)
        {
            var post = (obj as Post);
            using (ChannelFactory<IBlogService> factory = CreateChanelFactory())
            {
                factory.CreateChannel().DeletePost(post.ToModel());
                Posts = GetPosts();
            }
        }

        private List<Post> GetPosts()
        {
            using (ChannelFactory<IBlogService> factory = CreateChanelFactory())
            {
                return factory.CreateChannel().GetPosts().ToViewModel();
            }
        }

        private void LoadPostDetails(object post)
        {
            Guid postId = (post as Post).Id;
            using (ChannelFactory<IBlogService> factory = CreateChanelFactory())
            {
                BlogPost blogPost = factory.CreateChannel().GetPost(postId);
                if (blogPost != null)
                {
                    CurrentPost = blogPost.ToPostDetails();
                }
                else
                {
                    notification("Статья не найдена");
                }
            }
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
