using Ivoluntia.BackgroudServices.Common.Dtos;

namespace Ivoluntia.BackgroudServices.Services.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmail(EmailParams model);
    }
}
