using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Blog.Client.Annotations;

namespace Blog.Client.Models
{
    public class PostComment : INotifyPropertyChanged
    {
        private DateTime creationDate;
        private Guid id;
        private string text;
        private Post post;
        public event PropertyChangedEventHandler PropertyChanged;
        public DateTime CreationDate
        {
            get { return creationDate; }
            set
            {
                if (creationDate != value)
                {
                    creationDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public Guid Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Text
        {
            get { return text; }
            set
            {
                if (text != value)
                {
                    text = value;
                    OnPropertyChanged();
                }
            }
        }

        public Post Post
        {
            get { return post; }
            set
            {
                if (post != value)
                {
                    post = value;
                    OnPropertyChanged();
                }
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
