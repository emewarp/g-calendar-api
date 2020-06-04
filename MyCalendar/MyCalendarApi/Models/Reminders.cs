using System.Collections.Generic;

namespace MyCalendarApi.Models
{
    public class Reminders
    {
        public List<Reminder> ReminderList { get; set; }
        public bool? UseDefault { get; set; }
    }
}
