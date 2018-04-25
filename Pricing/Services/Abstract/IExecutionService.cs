namespace Pricing.Services.Abstract
{
    public interface IExecutionService
    {
        bool AlreadyDoneThisMonth();
        void MarkAsDoneThisMonth();
    }
}
