using Pricing.Models;
using Pricing.Repositories.Abstract;
using Pricing.Services.Abstract;
using System;
using System.Linq;

namespace Pricing.Services
{
    public class ExecutionService : IExecutionService
    {
        private readonly IRepository<Execution> _executionRepository;

        public ExecutionService(IRepository<Execution> executionRepository)
        {
            _executionRepository = executionRepository;
        }

        public void MarkAsDoneThisMonth()
        {
            _executionRepository.Create(new Execution { DateOfExecution = DateTime.UtcNow });
        }

        public bool AlreadyDoneThisMonth()
        {
            var lastExecution = _executionRepository.GetAll().OrderByDescending(e => e.DateOfExecution).FirstOrDefault();

            if (lastExecution == null)
            {
                return false;
            }

            var thisMonth = DateTime.UtcNow.Month;

            return lastExecution.DateOfExecution.Month == thisMonth;
        }
    }
}
