using MyCalendarModels.Models;
using System.Collections.Generic;
using Google.Apis.Calendar.v3.Data;
using static Google.Apis.Calendar.v3.Data.Event;
using System;

namespace MyCalendarModels.Mappers
{
    public static class GoogleEntityMapper
    {
        public static CalendarEvents ToMyCalendarEvents(this Events googleEvents)
        {
            return new CalendarEvents 
            { 
                Title = googleEvents.Summary,
                Events = GetEvents(googleEvents),
                DefaultReminders = GetDefaultReminders(googleEvents)
            };
        }

        public static CalendarEvent ToMyCalendarEvent(this Event googleEvent)
        {
            return new CalendarEvent
            {
                Id = googleEvent.Id,
                EventName = googleEvent.Summary,
                Attendees = GetAttendees(googleEvent),
                Description = googleEvent.Description,
                StartTime = googleEvent.Start.DateTime != null ? googleEvent.Start.DateTime : DateTime.Parse(googleEvent.Start.Date),
                EndTime = googleEvent.End.DateTime != null ? googleEvent.End.DateTime : DateTime.Parse(googleEvent.End.Date),
                IsAllDay = googleEvent.Start.Date != null ? true : false,
                Location = new Location 
                {
                    Name = googleEvent.Location,
                    TimeZone = googleEvent.Start.TimeZone
                },
                Reminders = GetReminders(googleEvent),
                Organizer = GetOrganizer(googleEvent)
            };
        }

        public static Event ToGoogleEvent(this CalendarEvent calendarEvent)
        {
            return new Event
            {
                Id = calendarEvent.Id,
                Summary = calendarEvent.EventName,
                Start = new EventDateTime
                {
                    DateTime = calendarEvent.StartTime,
                    TimeZone = calendarEvent.Location.TimeZone
                },
                End = new EventDateTime
                {
                    DateTime = calendarEvent.EndTime,
                    TimeZone = calendarEvent.Location.TimeZone
                },
                Attendees = GetAttendees(calendarEvent),
                Description = calendarEvent.Description,
                Location = calendarEvent.Location.Name,
                Reminders = GetReminders(calendarEvent),
                Organizer = GetOrganizer(calendarEvent),
                Status = calendarEvent.Status             
            };

        }

        #region Private Methods
        private static List<CalendarEvent> GetEvents(Events googleEvents)
        {
            if (googleEvents.Items == null)
                return new List<CalendarEvent>();

            List<CalendarEvent> events = new List<CalendarEvent>();
            foreach(Event e in googleEvents.Items)
                events.Add(e.ToMyCalendarEvent());
            return events;

        }
        private static List<Attendee> GetAttendees(Event googleEvent)
        {
            if (googleEvent.Attendees == null)
                return new List<Attendee>();

            List<Attendee> attendees = new List<Attendee>();
            foreach (EventAttendee a in googleEvent.Attendees)
            {
                attendees.Add(new Attendee
                {
                    Name = a.DisplayName,
                    Email = a.Email,
                    Organizer = a.Organizer,
                    ResponseStatus = a.ResponseStatus
                });
            };
            return attendees;
        }

        private static List<EventAttendee> GetAttendees(CalendarEvent calendarEvent)
        {
            if (calendarEvent.Attendees == null)
                return new List<EventAttendee>();

            List<EventAttendee> attendees = new List<EventAttendee>();
            foreach(Attendee a in calendarEvent.Attendees)
            {
                attendees.Add(new EventAttendee
                {
                    DisplayName = a.Name,
                    Email = a.Email,
                    Organizer = a.Organizer,
                    ResponseStatus = a.ResponseStatus
                });
            }
            return attendees;
        }

        private static List<Reminder> GetDefaultReminders(Events googleEvents)
        {
            if (googleEvents.DefaultReminders == null)
                return new List<Reminder>();

            List<Reminder> reminders = new List<Reminder>();
            foreach(EventReminder r in googleEvents.DefaultReminders)
            {
                reminders.Add(new Reminder
                {
                    Method = r.Method,
                    Minutes = r.Minutes
                });
            }
            return reminders;
        }
        private static Reminders GetReminders(Event googleEvent)
        {
            if (googleEvent.Reminders.Overrides == null)
                return new Reminders();

            List<Reminder> reminders = new List<Reminder>();
            foreach (EventReminder r in googleEvent.Reminders.Overrides)
            {
                reminders.Add(new Reminder
                {
                    Method = r.Method,
                    Minutes = r.Minutes
                });
            }
           
            return new Reminders { ReminderList = reminders, UseDefault = googleEvent.Reminders.UseDefault};
        }

        private static RemindersData GetReminders(CalendarEvent calendarEvent)
        {
            if (calendarEvent.Reminders.ReminderList == null)
                return new RemindersData();

            List<EventReminder> reminders = new List<EventReminder>();
            foreach (Reminder r in calendarEvent.Reminders.ReminderList)
            {
                reminders.Add(new EventReminder
                {
                    Method = r.Method,
                    Minutes = r.Minutes
                });
            }

            return new RemindersData { Overrides = reminders, UseDefault = calendarEvent.Reminders.UseDefault };
        }

        private static Organizer GetOrganizer(Event googleEvent)
        {
            if (googleEvent.Organizer == null)
                return new Organizer();

            return new Organizer
            {
                Email = googleEvent.Organizer.Email,
                Name = googleEvent.Organizer.DisplayName,
                Self = googleEvent.Organizer.Self
            };
        }

        private static OrganizerData GetOrganizer(CalendarEvent calendarEvent)
        {
            if (calendarEvent.Organizer == null)
                return new OrganizerData();

            return new OrganizerData
            {
                DisplayName = calendarEvent.Organizer.Name,
                Email = calendarEvent.Organizer.Email,
                Self = calendarEvent.Organizer.Self
            };
        }
        #endregion
    }
}
