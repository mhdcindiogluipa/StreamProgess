using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using StreamProgess.Shared;

namespace StreamProgess.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProgressController : ControllerBase
{
    [HttpGet("stream")]
    public async IAsyncEnumerable<ProgressUpdate> StreamProgress([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        for (int i = 0; i <= 100; i += 5)
        {
            if (cancellationToken.IsCancellationRequested)
                yield break;

            yield return new ProgressUpdate
            {
                Percentage = i,
                Message = $"Processing step {i / 5 + 1} of 21...",
                Timestamp = DateTime.UtcNow
            };

            await Task.Delay(500, cancellationToken);
        }
    }

    [HttpGet("long-operation")]
    public async IAsyncEnumerable<ProgressUpdate> LongOperation([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var steps = new[]
        {
            "Initializing...",
            "Loading data...",
            "Processing records...",
            "Validating results...",
            "Generating report...",
            "Finalizing..."
        };

        for (int i = 0; i < steps.Length; i++)
        {
            if (cancellationToken.IsCancellationRequested)
                yield break;

            yield return new ProgressUpdate
            {
                Percentage = (i + 1) * 100 / steps.Length,
                Message = steps[i],
                Timestamp = DateTime.UtcNow
            };

            await Task.Delay(Random.Shared.Next(800, 2000), cancellationToken);
        }
    }
}
