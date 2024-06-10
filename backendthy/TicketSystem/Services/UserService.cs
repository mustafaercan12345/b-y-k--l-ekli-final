using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RabbitMQ.Client;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TicketSystem.Models;
using TicketSystem.Repositories;

namespace TicketSystem.Services
{
    public class UserService : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        IConfiguration _configuration;
        private readonly RabbitMQConfiguration _rabbitMQConfig;
        public UserService(ApplicationDbContext context, IConfiguration configuration, IOptions<RabbitMQConfiguration> rabbitMQConfig)
        {
            _context = context;
            _configuration = configuration;
            _rabbitMQConfig = rabbitMQConfig.Value;
        }

        public User AddUser(User user)
        {
            user.Password = HashPassword(user.Password);
            _context.Users.Add(user);
            _context.SaveChanges();
            PublishWelcomeEmailMessage(user);
            return user;
        }

        public string HashPassword(string password)
        {

            string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return hashedPassword;
        }
        private User GetAuthenticatedUser(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return user;
            }

            return null;
        }

        public string GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], null,
                expires: DateTime.UtcNow.AddYears(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public User? Login(string email, string password)
        {
            var user = GetAuthenticatedUser(email, password);

            if (user == null) { 
            return null;
            }
            return user;
        }

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public bool UserExists(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public void PublishWelcomeEmailMessage(User user)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMQConfig.Hostname,
                UserName = _rabbitMQConfig.UserName,
                Password = _rabbitMQConfig.Password
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _rabbitMQConfig.QueueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = $"Email:{user.Email};Welcome, {user.FullName}";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: _rabbitMQConfig.QueueName,
                                     basicProperties: null,
                                     body: body);
            }
        }

      
    }


}
