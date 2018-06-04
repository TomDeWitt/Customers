namespace AscentCustomers.Controllers
{
    using System;
    using System.Data;
    using System.Web.Mvc;
    using Calendar;
    using DayPilot.Web.Mvc;
    using DayPilot.Web.Mvc.Data;
    using DayPilot.Web.Mvc.Enums;
    using DayPilot.Web.Mvc.Events.Calendar;

    public class CalendarController : Controller
    {
        public ActionResult Day()
        {
            return new PilotCalender().CallBack(this);
        }

        public ActionResult Week()
        {
            return new PilotCalender().CallBack(this);
        }

        public ActionResult Month()
        {
            return new PilotMonth().CallBack(this);
        }

        private class PilotCalender : DayPilotCalendar
        {
            protected override void OnInit(InitArgs e)
            {
                Update(CallBackUpdateType.Full);
            }

            protected override void OnEventResize(EventResizeArgs e)
            {
                new CalendarEventManager().MoveCalendarEvent(e.Id, e.NewStart, e.NewEnd);
                Update();
            }

            protected override void OnEventMove(EventMoveArgs e)
            {
                new CalendarEventManager().MoveCalendarEvent(e.Id, e.NewStart, e.NewEnd);
                Update();
            }

            protected override void OnTimeRangeSelected(TimeRangeSelectedArgs e)
            {
                new CalendarEventManager().CreateCalendarEvent("New event", e.Start, e.End);
                Update();
            }

            protected override void OnBeforeEventRender(BeforeEventRenderArgs e)
            {
                e.Areas.Add(new Area().Right(3).Top(3).Width(15).Height(15).CssClass("event_action_delete").JavaScript("switcher.active.control.commandCallBack('delete', {'e': e});"));
            }

            protected override void OnCommand(CommandArgs e)
            {
                switch (e.Command)
                {
                    case "navigate":
                        StartDate = (DateTime)e.Data["day"];
                        Update(CallBackUpdateType.Full);
                        break;
                    case "refresh":
                        Update(CallBackUpdateType.EventsOnly);
                        break;
                    case "delete":
                        new CalendarEventManager().DeleteCalendarEvent((string)e.Data["e"]["id"]);
                        Update(CallBackUpdateType.EventsOnly);
                        break;
                }
            }

            protected override void OnFinish()
            {
                if (UpdateType == CallBackUpdateType.None)
                {
                    return;
                }

                Events = new CalendarEventManager().FilteredData(StartDate, StartDate.AddDays(Days)).AsEnumerable();

                DataIdField = "Id";
                DataTextField = "Description";
                DataStartField = "Starting";
                DataEndField = "Ending";
            }
        }

        private class PilotMonth : DayPilotMonth
        {
            protected override void OnInit(DayPilot.Web.Mvc.Events.Month.InitArgs e)
            {
                Update();
            }

            protected override void OnEventResize(DayPilot.Web.Mvc.Events.Month.EventResizeArgs e)
            {
                new CalendarEventManager().MoveCalendarEvent(e.Id, e.NewStart, e.NewEnd);
                Update();
            }

            protected override void OnEventMove(DayPilot.Web.Mvc.Events.Month.EventMoveArgs e)
            {
                new CalendarEventManager().MoveCalendarEvent(e.Id , e.NewStart, e.NewEnd);
                Update();
            }

            protected override void OnTimeRangeSelected(DayPilot.Web.Mvc.Events.Month.TimeRangeSelectedArgs e)
            {
                new CalendarEventManager().CreateCalendarEvent("New event", e.Start, e.End);
                Update();
            }

            protected override void OnBeforeEventRender(DayPilot.Web.Mvc.Events.Month.BeforeEventRenderArgs e)
            {
                e.Areas.Add(new Area().Right(3).Top(3).Width(15).Height(15).CssClass("event_action_delete").JavaScript("switcher.active.control.commandCallBack('delete', {'e': e});"));
            }

            protected override void OnCommand(DayPilot.Web.Mvc.Events.Month.CommandArgs e)
            {
                switch (e.Command)
                {
                    case "navigate":
                        StartDate = (DateTime)e.Data["day"];
                        Update(CallBackUpdateType.Full);
                        break;
                    case "refresh":
                        Update(CallBackUpdateType.EventsOnly);
                        break;
                    case "delete":
                        new CalendarEventManager().DeleteCalendarEvent((string)e.Data["e"]["id"]);
                        Update(CallBackUpdateType.EventsOnly);
                        break;
                }
            }

            protected override void OnFinish()
            {
                if (UpdateType == CallBackUpdateType.None)
                {
                    return;
                }

                Events = new CalendarEventManager().FilteredData(VisibleStart, VisibleEnd).AsEnumerable();

                DataIdField = "Id";
                DataTextField = "Description";
                DataStartField = "Starting";
                DataEndField = "Ending";
            }
        }

    }
}
