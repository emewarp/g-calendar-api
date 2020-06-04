using MyCalendarApi.Models;

namespace MyCalendarApi.Abstractions
{
    public interface IMyCalendarService
    {
        CalendarEvents GetAllEvents(string calendarId);
        CalendarEvent GetEvent(string calendarId, string eventId);
        CalendarEvent CreateEvent(CalendarEvent myEvent, string calendarId);
        CalendarEvent UpdateEvent(CalendarEvent update, string calendarId, string eventId);
        bool DeleteEvent(string calendarId, string eventId);
    }
}
