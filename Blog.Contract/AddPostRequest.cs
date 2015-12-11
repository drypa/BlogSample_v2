using System;
using System.Runtime.Serialization;

namespace Blog.Contract
{
    [DataContract]
    public class AddPostRequest
    {
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Text { get; set; }
    }
}