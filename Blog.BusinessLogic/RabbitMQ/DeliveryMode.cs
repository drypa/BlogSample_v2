using System;

namespace Blog.BusinessLogic.RabbitMQ
{
    internal static class DeliveryMode
    {
        public static byte NonPersistent
        {
            get { return 1; }
        }

        public static byte Persistent
        {
            get { return 2; }
        }
    }
}