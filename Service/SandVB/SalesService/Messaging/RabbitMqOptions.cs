namespace SalesService.Messaging
{
    public class RabbitMqOptions
    {
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public int RequestedConnectionTimeout { get; set; }
        public Boolean SslEnabled { get; set; }
        public string SslServerName { get; set; }
    }
}
