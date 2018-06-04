namespace AscentCustomers.Controllers
{
    using System;
    using System.Web.Mvc;
    using Calendar;
    using DayPilot.Web.Mvc.Json;

    public class CalendarEventController : Controller
    {
        public ActionResult Edit(string id)
        {
            var manager = new CalendarEventManager().Get(id) ?? new CalendarEvent();
            return View(manager);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(FormCollection form)
        {
            DateTime start = Convert.ToDateTime(form["Starting"]);
            DateTime end = Convert.ToDateTime(form["Ending"]);
            new CalendarEventManager().EditCalenderEvent(form["Id"], form["Description"], start, end);
            return JavaScript(SimpleJsonSerializer.Serialize("OK"));
        }

        public ActionResult Create()
        {
            return View(
                new CalendarEvent
                {
                    Starting = Convert.ToDateTime(Request.QueryString["start"]),
                    Ending = Convert.ToDateTime(Request.QueryString["end"]),
                    Description = "New Event"
                });
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(FormCollection form)
        {
            DateTime start = Convert.ToDateTime(form["Starting"]);
            DateTime end = Convert.ToDateTime(form["Ending"]);
            new CalendarEventManager().CreateCalendarEvent(form["Description"], start, end);
            return JavaScript(SimpleJsonSerializer.Serialize("OK"));
        }
    }
}
