using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Bookstore.EmailSending;

public class EmailSendingBackgroundService : BackgroundService
{
    private readonly ILogger<EmailSendingBackgroundService> _logger;

    public EmailSendingBackgroundService(ILogger<EmailSendingBackgroundService> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        int delayMilliseconds = 10_000; // 10 seconds

        _logger.LogInformation("{serviceName} starting...", nameof(EmailSendingBackgroundService));

        while(!stoppingToken.IsCancellationRequested)
        {
            try
            {

            }
            catch(Exception ex) 
            {
                _logger.LogError("Error processing outbox: {message}", ex.Message);
            }
            finally
            {
                await Task.Delay(delayMilliseconds);
            }
        }

        _logger.LogInformation("{serviceName} stopping.", nameof(EmailSendingBackgroundService));
    }
}
