using System;

namespace Pricing.Models
{
    public class DateRange
    {
        private DateRange(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public static DateRange LastMonth()
        {
            var now = DateTime.UtcNow;

            return new DateRange(now.AddMonths(-1), now);
        }

        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
    }
}
