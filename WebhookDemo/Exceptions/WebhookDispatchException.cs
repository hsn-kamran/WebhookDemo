namespace WebhookDemo.Exceptions
{
    /// <summary>
    /// исключение при ошибке, возникшей при отправкицpost запроса на указанный эндпоинт
    /// </summary>
    internal class WebhookDispatchException : Exception
    {
        /// <summary>
        /// инициализация ошибки
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="eventUrl"></param>
        public WebhookDispatchException(string eventName, string eventUrl) 
            : base($"Error occured while sending {eventName} to an {eventUrl} url")
        {
            EventName = eventName;
            EventUrl = eventUrl;
        }

        /// <summary>
        /// наименование события
        /// </summary>
        public string EventName { get; private set; }
        
        /// <summary>
        /// url адрес, на который не дошел post запрос
        /// </summary>
        public string EventUrl { get; private set; }
    }
}
