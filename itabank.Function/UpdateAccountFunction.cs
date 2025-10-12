using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace itabank.Function;

public class UpdateAccountFunction
{
    private readonly ILogger<UpdateAccountFunction> _logger;

    private const string UpdateAccountTimer = "0 */5 * * * *";

    public UpdateAccountFunction(ILogger<UpdateAccountFunction> logger)
    {
        _logger = logger;
    }

    [Function("UpdateAccount")]
    public void UpdateAccount([TimerTrigger(UpdateAccountTimer)] TimerInfo timer)
    {
        _logger.LogInformation($"Running UpdateAccount... {DateTimeOffset.UtcNow} {timer.IsPastDue}");
    }
}