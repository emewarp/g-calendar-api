using Microsoft.AspNetCore.Mvc;
using MyCalendarContracts;
using MyCalendarModels.Models;
using MyCalendarContracts.Contracts;


namespace MyCalendarApi.Controllers
{
    [ApiController]
    [Route("mycalendar")]
    public class CalendarEventsController : ControllerBase
    {
        private IMyCalendarService service;

        public CalendarEventsController()
        {
            service = new MyCalendarService();
        }

        [HttpGet("get")]
        public CalendarEvents GetAllEvents(string calendarId)
        {
            return service.GetAllEvents(calendarId);
        }

        [HttpGet("get/{eventId}")]
        public CalendarEvent GetEvent(string calendarId, string eventId)
        {
            return service.GetEvent(calendarId, eventId);
        }

        [HttpPost]
        public CalendarEvent CreateEvent(CalendarEvent myEvent, string calendarId)
        {
            return service.CreateEvent(myEvent, calendarId);
        }

        [HttpPut("update")]
        public CalendarEvent UpdateEvent(CalendarEvent update, string calendarId, string eventId)
        {
            return service.UpdateEvent(update, calendarId, eventId);
        }

        [HttpDelete("delete")]
        public bool DeleteEvent(string calendarId, string eventId)
        {
            return service.DeleteEvent(calendarId, eventId);
        }
    }
}
