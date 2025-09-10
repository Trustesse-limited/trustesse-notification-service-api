using Ivoluntia.BackgroudServices.Data.Enums;

namespace Ivoluntia.BackgroudServices.Services.Implementations
{
    public class EmailServiceFactory : IEmailServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public EmailServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IEmailService GetEmailService()
        {
            var provider = EmailServiceProvider.Gmail.ToString();

            return provider switch
            {
                "Gmail" => _serviceProvider.GetRequiredService<GmailService>(),
            };
        }
    }
}
