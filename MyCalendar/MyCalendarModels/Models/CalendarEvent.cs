using System;
using System.Collections.Generic;

namespace MyCalendarModels.Models
{
    public class CalendarEvent
    {
        public string Id { get; set; } //if not set, google serve creates an event id by default
        public string EventName { get; set; }
        public List<Attendee> Attendees { get; set; }
        public string Description { get; set; }       
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool? IsAllDay { get; set; }
        public Organizer Organizer { get; set; }
        public Location Location { get; set; }
        public string Status { get; set; }
        public Reminders Reminders { get; set; }       
        public IList<string> Recurrence { get; set; }
    }
}
