namespace BlogPessoal.Config.RateLimitConfig;

public class RateLimitOptions
{
    public const string MyRateLimit = "MyRateLimit";
    public int PermitLimit { get; set; } = 5;
    public int Window { get; set; } = 1;
    public int SegmentsPerWindow { get; set; } = 2;
    public int QueueLimit { get; set; } = 2;
}
