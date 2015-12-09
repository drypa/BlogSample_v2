using System;
using System.Text;
using Newtonsoft.Json;

namespace Blog.BusinessLogic.Common.RabbitMQ
{
    public class Serializer<T>
    {
        public T Desearalize(byte[] bytes)
        {
            string jsonString = Encoding.UTF8.GetString(bytes);
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public byte[] Serialize(T obj)
        {
            string jsonString = JsonConvert.SerializeObject(obj);
            return Encoding.UTF8.GetBytes(jsonString);
        }
    }
}
