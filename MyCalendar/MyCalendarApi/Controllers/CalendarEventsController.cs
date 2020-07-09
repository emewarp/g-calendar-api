using Microsoft.AspNetCore.Mvc;
using MyCalendarContracts.Contracts;
using MyCalendarModels.Models;
using MyCalendarContracts;
using System;
using System.Net;

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
        public IActionResult GetAllEvents(string calendarId)
        {
            try
            {
                CalendarEvents events = service.GetAllEvents(calendarId);
                return events != null ? (IActionResult)Ok(events) : (IActionResult)BadRequest();
            }
            catch (Google.GoogleApiException e)
            {
                return e.HttpStatusCode.Equals(HttpStatusCode.NotFound) ? (IActionResult)NotFound(e.Message) : e.HttpStatusCode.Equals(HttpStatusCode.InternalServerError) ? StatusCode(500, e.Message) : (IActionResult)BadRequest(e.Message);
            }

        }

        [HttpGet("{calendarId}/events/{eventId}")]
        public IActionResult GetEvent(string calendarId, string eventId)
        {
            try
            {
                CalendarEvent calendarEvent = service.GetEvent(calendarId, eventId);
                return calendarEvent != null ? (IActionResult)Ok(calendarEvent) : (IActionResult)BadRequest();
            }
            catch (Google.GoogleApiException e)
            {
                return e.HttpStatusCode.Equals(HttpStatusCode.NotFound) ? (IActionResult)NotFound() : (IActionResult)BadRequest(e.Message);
            }

        }

        [HttpPost("{calendarId}/events")]
        public IActionResult CreateEvent(CalendarEvent myEvent, string calendarId)
        {
            try
            {
                CalendarEvent newEvent = service.CreateEvent(myEvent, calendarId);
                return newEvent != null ? StatusCode(201, newEvent) : (IActionResult)BadRequest();
            }
            catch (Google.GoogleApiException e)
            {
                Console.WriteLine(e);
                return e.HttpStatusCode.Equals(HttpStatusCode.Conflict) ? (IActionResult)Conflict(e.Message) : (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpPut("{calendarId}/events/{eventId}")]
        public IActionResult UpdateEvent(CalendarEvent update, string calendarId, string eventId)
        {
            try
            {
                CalendarEvent updatedEvent = service.UpdateEvent(update, calendarId, eventId);
                return updatedEvent != null ? (IActionResult)Ok(updatedEvent) : (IActionResult)BadRequest();
            }
            catch (Google.GoogleApiException e)
            {
                return e.HttpStatusCode.Equals(HttpStatusCode.NotFound) ? (IActionResult)NotFound(e.Message) : (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpDelete("{calendarId}/events/{eventId}")]
        public IActionResult DeleteEvent(string calendarId, string eventId)
        {
            try
            {
                return service.DeleteEvent(calendarId, eventId) ? (IActionResult)Ok(eventId) : (IActionResult)BadRequest();
            }
            catch (Google.GoogleApiException e)
            {
                return e.HttpStatusCode.Equals(HttpStatusCode.NotFound) ? (IActionResult)NotFound(e.Message) : (IActionResult)BadRequest(e.Message);
            }
        }
    }
}
