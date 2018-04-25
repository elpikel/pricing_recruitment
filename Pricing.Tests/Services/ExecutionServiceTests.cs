using Pricing.Models;
using Pricing.Repositories;
using Pricing.Services;
using System;
using Xunit;

namespace Pricing.Tests.Services
{
    public class ExecutionServiceTests
    {
        [Fact]
        public void NotAlreadyDoneWhenThereIsNothingInDb()
        {
            var executionRepository = new Repository<Execution>();
            var executionService = new ExecutionService(executionRepository);

            var alreadyDoneThisMonth = executionService.AlreadyDoneThisMonth();

            Assert.False(alreadyDoneThisMonth);
        }

        [Fact]
        public void NotAlreadyDoneWhenNoExecutionsThisMonth()
        {
            var executionRepository = new Repository<Execution>();
            var executionService = new ExecutionService(executionRepository);

            executionRepository.Create(new Execution { DateOfExecution = DateTime.UtcNow.AddMonths(-1) });

            var alreadyDoneThisMonth = executionService.AlreadyDoneThisMonth();

            Assert.False(alreadyDoneThisMonth);
        }

        [Fact]
        public void AlreadyDoneWhenThereIsExecutionForThisMonth()
        {
            var executionRepository = new Repository<Execution>();
            var executionService = new ExecutionService(executionRepository);

            executionRepository.Create(new Execution { DateOfExecution = DateTime.UtcNow });

            var alreadyDoneThisMonth = executionService.AlreadyDoneThisMonth();

            Assert.True(alreadyDoneThisMonth);
        }

        [Fact]
        public void MarkAsDoneThisMonth()
        {
            var executionRepository = new Repository<Execution>();
            var executionService = new ExecutionService(executionRepository);

            executionService.MarkAsDoneThisMonth();

            var alreadyDoneThisMonth = executionService.AlreadyDoneThisMonth();

            Assert.True(alreadyDoneThisMonth);
        }
    }
}
