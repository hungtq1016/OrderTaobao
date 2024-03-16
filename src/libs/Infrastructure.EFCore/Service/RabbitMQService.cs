namespace Infrastructure.EFCore.Service
{
    public interface IRabbitMQService
    {
        void SendMessage<T>(T message);
        void ConsumeMessage(Action<string> messageHandler,string queue);
    }

    public class RabbitMQService : IRabbitMQService
    {
        private readonly ConnectionFactory _factory;

        public RabbitMQService()
        {
            _factory = new ConnectionFactory()
            {
                HostName = "rabbitmq",
                Port = 5672,
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/"
            };
        }

        public void SendMessage<T>(T message)
        {
            var connection = _factory.CreateConnection();
            var channel = connection.CreateModel();
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.QueueDeclare(typeof(T).Name, durable: false, exclusive: false, autoDelete: false, arguments: null);

            channel.BasicPublish(exchange: "", routingKey: typeof(T).Name, body: body);
        }

        public void ConsumeMessage(Action<string> messageHandler,string queue)
        {
            var connection = _factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: queue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                messageHandler(message);

                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            channel.BasicConsume(queue: queue, autoAck: false, consumer: consumer);
        }
    }
}
