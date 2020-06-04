using System.Collections.Generic;

namespace MyCalendarApi.Models
{
    public class CalendarEvents
    {
        public string Title { get; set; }
        public List<CalendarEvent> Events { get; set; }
        public List<Reminder> DefaultReminders { get; set; }
    }
}
