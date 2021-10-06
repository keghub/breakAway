using System;

namespace Domain
{
    public class TimeReport
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int EmployeeId { get; set; }

        public TimeReportType Type { get; set; }

        public int? WorkingHours { get; set; }
    }

    public enum TimeReportType
    {
        WorkingHours = 1,
        Vacation = 2,
        SickDay = 3
    }
}