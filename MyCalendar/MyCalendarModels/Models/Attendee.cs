namespace MyCalendarModels.Models
{
    public class Attendee
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool? Organizer {get;set;}
        public string ResponseStatus { get; set; }

    }
}
