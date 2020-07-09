using Microsoft.AspNetCore.Mvc;
using MyCalendarContracts;
using MyCalendarContracts.Contracts;
using MyCalendarModels.Models;

namespace MyCalendarApi.Controllers
{
    [ApiController]
    [Route("calendars")]
    public class CalendarEventsController : ControllerBase
    {
        private IMyCalendarService service;

        public CalendarEventsController()
        {
            service = new MyCalendarService();
        }

        [HttpGet("{calendarId}/events")]
        public CalendarEvents GetAllEvents(string calendarId)
        {
            return service.GetAllEvents(calendarId);
        }

        [HttpGet("{calendarId}/events/{eventId}")]
        public CalendarEvent GetEvent(string calendarId, string eventId)
        {
            return service.GetEvent(calendarId, eventId);
        }

        [HttpPost("{calendarId}/events")]
        public CalendarEvent CreateEvent(CalendarEvent myEvent, string calendarId)
        {
            return service.CreateEvent(myEvent, calendarId);
        }

        [HttpPut("{calendarId}/events/{eventId}")]
        public CalendarEvent UpdateEvent(CalendarEvent update, string calendarId, string eventId)
        {
            return service.UpdateEvent(update, calendarId, eventId);
        }

        [HttpDelete("{calendarId}/events/{eventId}")]
        public bool DeleteEvent(string calendarId, string eventId)
        {
            return service.DeleteEvent(calendarId, eventId);
        }
    }
}
