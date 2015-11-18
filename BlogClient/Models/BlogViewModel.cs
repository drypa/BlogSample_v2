using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
        private ICommand addPostCommand;
        private PostDetails currentPost;
        private bool hasNewPost;
        private ICommand newPostCommand;
        private List<Post> posts;
        private ICommand selectPostCommand;
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand AddPostCommand
        {
            get
            {
                if (addPostCommand == null)
                {
                    addPostCommand = new RelayCommand(x => AddNewPost(), x => HasNewPost);
                }
                return addPostCommand;
            }
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

        public ICommand DeletePostCommand
        {
            get { throw new NotImplementedException(); }
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
            get
            {
                if (newPostCommand == null)
                {
                    newPostCommand = new RelayCommand(x => CreateNewPost(), x => true);
                }
                return newPostCommand;
            }
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

        public ICommand SelectPostCommand
        {
            get
            {
                if (selectPostCommand == null)
                {
                    selectPostCommand = new RelayCommand(LoadPostDetails, x => true);
                }
                return selectPostCommand;
            }

        }

        private void LoadPostDetails(object post)
        {
            var postId = (post as Post).Id;
            using (ChannelFactory<IBlogService> factory = CreateChanelFactory())
            {
                CurrentPost = factory.CreateChannel().GetPost(postId).ToPostDetails();
            }
        }

        private void AddNewPost()
        {
            BlogPost model = CurrentPost.ToModel();
            model.CreateDate = DateTime.Now;
            using (ChannelFactory<IBlogService> factory = CreateChanelFactory())
            {
                factory.CreateChannel().AddPost(model);
            }

            HasNewPost = false;
        }

        private ChannelFactory<IBlogService> CreateChanelFactory()
        {
            var factory = new ChannelFactory<IBlogService>(
                new WebHttpBinding(), ConfigurationManager.AppSettings["SeviceUrl"]);
            factory.Endpoint.Behaviors.Add(new WebHttpBehavior());
            return factory;
        }

        private void CreateNewPost()
        {
            HasNewPost = true;
            CurrentPost = new PostDetails();
        }

        private List<Post> GetPosts()
        {
            using (ChannelFactory<IBlogService> factory = CreateChanelFactory())
            {
                return factory.CreateChannel().GetPosts().ToViewModel();
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
