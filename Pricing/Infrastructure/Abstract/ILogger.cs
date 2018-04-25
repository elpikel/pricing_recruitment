namespace Pricing.Infrastructure.Abstract
{
    public interface ILogger
    {
        void LogInfo(string message);
        void LogError(string message);
    }
}