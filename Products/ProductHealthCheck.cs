using Microsoft.Extensions.Diagnostics.HealthChecks;

/// <summary>
/// I have created a generic healthcheck that could be used for anything.
/// </summary>
public class ProductHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        /// TODO 
        /// This is just a random example, normally here we will test connection with other system, databases, etc.

        var allocated = GC.GetTotalMemory(forceFullCollection: false);

        var status = (allocated > 1000) ? HealthStatus.Healthy : HealthStatus.Degraded;


        return Task.FromResult(new HealthCheckResult(
            status,
            description: " Message to display ...",
            exception: null,
            data: null));
    }

}