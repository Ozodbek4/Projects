namespace BootcampResult.Infrastructure.Common.Settings;

public class CasheSettings
{
    public int AbsoluteExpirationTimeInSeconds { get; set; }
    
    public int SlidingExpirationTimeInSeconds { get; set; }
}