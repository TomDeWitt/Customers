namespace AscentCustomers.Calendar
{
    using System;

    public class CalendarEvent
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Starting { get; set; }
        public DateTime Ending { get; set; }
    }
}