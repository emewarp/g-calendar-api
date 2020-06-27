using MyCalendarContracts.Contracts;
using MyCalendarModels.Models;
using MyCalendarModels.Mappers;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Threading;
using System.IO;
using System;

namespace MyCalendarContracts
{
    public class MyCalendarService : IMyCalendarService
    {
        public CalendarEvents GetAllEvents(string calendarId)
        {          
            var service = GetGoogleService();           

            // Define parameters of request
            EventsResource.ListRequest request = service.Events.List(calendarId);
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 1000;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            Events events = request.Execute();               
            
            return events.ToMyCalendarEvents();
        }

        public CalendarEvent GetEvent(string calendarId, string eventId)
        {
            var service = GetGoogleService();
                        
            return service.Events.Get(calendarId, eventId).Execute()
                    .ToMyCalendarEvent();
        }

        public CalendarEvent CreateEvent(CalendarEvent myEvent, string calendarId) 
        {
            var service = GetGoogleService();

            return service.Events.Insert(myEvent.ToGoogleEvent(), calendarId).Execute()
                    .ToMyCalendarEvent();            
        }

        public CalendarEvent UpdateEvent(CalendarEvent update, string calendarId, string eventId)
        {
            var service = GetGoogleService();

            return service.Events.Update(update.ToGoogleEvent(), calendarId, eventId).Execute()
                    .ToMyCalendarEvent();            
        }

        public bool DeleteEvent(string calendarId, string eventId)
        {
            var service = GetGoogleService();
            bool deleted = false;

            var removed = service.Events.Delete(calendarId, eventId).Execute();
            if (removed != null)
                deleted = true;

            return deleted;
        }
                
        private CalendarService GetGoogleService()
        {
            /* From
             * https://developers.google.com/calendar/quickstart/dotnet
             */

            string[] Scopes = { CalendarService.Scope.Calendar };

            UserCredential credential;
            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                // Remove and run app when Scopes changes
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Google Calendar API .NET Quickstart",
            });

            return service;
        }
    }
}
