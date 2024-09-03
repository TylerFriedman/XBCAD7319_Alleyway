using System.Collections.Generic;

namespace XBCAD.ViewModels
{
    public class AvailabilityViewModel
    {
        public List<DayAvailability> Days { get; set; } = new List<DayAvailability>
        {
            new DayAvailability { Day = "Monday" },
            new DayAvailability { Day = "Tuesday" },
            new DayAvailability { Day = "Wednesday" },
            new DayAvailability { Day = "Thursday" },
            new DayAvailability { Day = "Friday" },
            new DayAvailability { Day = "Saturday" },
            new DayAvailability { Day = "Sunday" }
        };
    }

    public class DayAvailability
    {
        public string Day { get; set; }
        public List<TimeSlot> TimeSlots { get; set; } = new List<TimeSlot>();
    }

    public class TimeSlot
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
