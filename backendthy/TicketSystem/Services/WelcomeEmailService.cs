using Microsoft.Extensions.Options;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using TicketSystem.Models;
using System.Text;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using System.Threading;

namespace TicketSystem.Services
{
    public class WelcomeEmailService : BackgroundService
    {
        private readonly RabbitMQConfiguration _rabbitMQConfig;
        private readonly EmailSettings _emailSettings;
        private IConnection _connection;
        private IModel _channel;

        public WelcomeEmailService(IOptions<RabbitMQConfiguration> rabbitMQConfig, IOptions<EmailSettings> emailSettings)
        {
            _rabbitMQConfig = rabbitMQConfig.Value;
            _emailSettings = emailSettings.Value;
            InitializeRabbitMQListener();
        }

        private void InitializeRabbitMQListener()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMQConfig.Hostname,
                UserName = _rabbitMQConfig.UserName,
                Password = _rabbitMQConfig.Password
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _rabbitMQConfig.QueueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                SendWelcomeEmail(message);

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(_rabbitMQConfig.QueueName, false, consumer);

            return Task.CompletedTask;
        }

        private void SendWelcomeEmail(string message)
        {
            try
            {

                var parts = message.Split(';');
                var email = parts[0].Replace("Email:", "");
                var emailMessageContent = parts[1];

                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
                emailMessage.To.Add(new MailboxAddress("", email)); 
                emailMessage.Subject = "Welcome to MilesSmiles!";
                emailMessage.Body = new TextPart("plain")
                {
                    Text = emailMessageContent
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(_emailSettings.SmtpServer, _emailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate(_emailSettings.Username, _emailSettings.Password);
                    client.Send(emailMessage);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
