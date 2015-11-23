using System;

namespace Blog.Client
{
    public class ClientNotificator : IClientNotificator
    {
        private readonly Action<string> showMessageAction;

        public ClientNotificator(Action<string> showMessage)
        {
            showMessageAction = showMessage;
        }

        public void ShowMessage(string message)
        {
            showMessageAction(message);
        }
    }
}