using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Blog.Client.Common.Model
{
    public class PostComment : INotifyPropertyChanged
    {
        private DateTime creationDate;
        private Guid id;
        private Post post;
        private string text;
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

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
