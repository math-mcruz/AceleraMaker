namespace BlogPessoal.Config.RateLimitConfig;

public class RateLimitGlobalOptions
{
    public const string RateLimitGlobal = "RateLimitGlobal";
    public int PermitLimit { get; set; } = 100;
    public int Window { get; set; } = 1;
    public int SegmentsPerWindow { get; set; } = 6;
    public int QueueLimit { get; set; } = 10;
}
