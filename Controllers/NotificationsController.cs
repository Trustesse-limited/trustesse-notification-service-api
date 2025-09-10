using Ivoluntia.BackgroudServices.Common.Dtos;

namespace Ivoluntia.BackgroudServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IEmailJobService _emailJobService;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public NotificationsController(IEmailJobService emailJobService, IBackgroundJobClient backgroundJobClient)
        {
            _emailJobService = emailJobService;
            _backgroundJobClient = backgroundJobClient;
        }

        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailRequest request)
        {
            try
            {
                var response = await _emailJobService.SendEmailAsync(request);

                if (response.Successs)
                    return Ok(response);

                if (response.StatusCode == StatusCodes.Status400BadRequest)
                    return BadRequest(response);

                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ApiResponse<string>.Failure(
                        StatusCodes.Status500InternalServerError,
                        $"Error: {ex.Message}"
                    ));
            }
        }

        [HttpPost("enqueue-email")]
        public IActionResult EnqueueEmail([FromBody] SendEmailRequest request)
        {
            try
            {
                string jobId = _backgroundJobClient.Enqueue(() => _emailJobService.SendEmailAsync(request));

                return Ok(ApiResponse<string>.Success(
                    $"Email job enqueued successfully with JobId: {jobId}.",
                    jobId
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ApiResponse<string>.Failure(
                        StatusCodes.Status500InternalServerError,
                        $"Error: {ex.Message}"
                    ));
            }
        }


        [HttpPost("send-unsent-notifications")]
        public async Task<IActionResult> SendEmail()
        {
            try
            {
                int processedCount = await _emailJobService.SendNotificationEmailAsync();

                return Ok(ApiResponse<int>.Success(
                    processedCount > 0
                        ? $"Processed {processedCount} notification(s)."
                        : "No unsent notifications found.",
                    processedCount
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ApiResponse<string>.Failure(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}"));
            }
        }

        [HttpPost("enqueue-unsent-notifications")]
        public IActionResult Enqueue()
        {
            try
            {
                string jobId = _backgroundJobClient.Enqueue(() => _emailJobService.SendNotificationEmailAsync());

                return Ok(ApiResponse<string>.Success(
                    $"Job enqueued successfully with JobId: {jobId}.",
                    jobId
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ApiResponse<string>.Failure(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}"));
            }
        }
    }
}
