using Pricing.Models;
using System;
using Xunit;

namespace Pricing.Tests.Models
{
    public class DateRangeTests
    {
        [Fact]
        public void CreatedDateRangeForLastMont()
        {
            var lastMonth = DateRange.LastMonth();
            var previousMonth = DateTime.UtcNow.AddMonths(-1).Month;

            Assert.Equal(previousMonth, lastMonth.StartDate.Month);
        }
    }
}
