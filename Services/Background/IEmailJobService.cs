

using Ivoluntia.BackgroudServices.Common.Dtos;

namespace Ivoluntia.BackgroudServices.Services.Background
{
    public interface IEmailJobService
    {
        Task<ApiResponse<object>> SendEmailAsync(SendEmailRequest request);
        Task<int> SendNotificationEmailAsync();
    }
}
