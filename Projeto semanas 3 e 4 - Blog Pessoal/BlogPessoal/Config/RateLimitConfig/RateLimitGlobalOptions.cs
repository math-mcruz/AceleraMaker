namespace BlogPessoal.Config.RateLimitConfig;

public class RateLimitGlobalOptions
{
    public const string MyRateLimit = "MyRateLimit";
    public int PermitLimit { get; set; } = 60;
    public int Window { get; set; } = 10;
    public int SegmentsPerWindow { get; set; } = 2;
    public int QueueLimit { get; set; } = 0;
}
