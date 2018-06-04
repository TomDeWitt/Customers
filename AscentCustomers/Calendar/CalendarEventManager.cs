namespace AscentCustomers.Calendar
{
    using System;
    using System.Data;
    using System.Data.Common;

    public class CalendarEventManager
    {
        public DataTable FilteredData(DateTime start, DateTime end)
        {
            DbDataAdapter adapter = DataAccess.CreateDataAdapter("SELECT * FROM [CalendarEvents] WHERE NOT (([Ending] <= @start) OR ([Starting] >= @end))");
            DataAccess.AddParameterWithValue(adapter.SelectCommand, "start", start);
            DataAccess.AddParameterWithValue(adapter.SelectCommand, "end", end);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        public void EditCalenderEvent(string id, string description, DateTime start, DateTime end)
        {
            int i;
            if (int.TryParse(id, out i))
            {
                EditCalenderEvent(i, description, start, end);
            }
        }
        public void EditCalenderEvent(int id, string description, DateTime start, DateTime end)
        {
            using (var connection = DataAccess.CreateConnection())
            {
                connection.Open();
                var command = DataAccess.CreateCommand("UPDATE [CalendarEvents] SET [Description] = @description, [Starting] = @start, [Ending] = @end WHERE [Id] = @id", connection);
                DataAccess.AddParameterWithValue(command, "id", id);
                DataAccess.AddParameterWithValue(command, "start", start);
                DataAccess.AddParameterWithValue(command, "end", end);
                DataAccess.AddParameterWithValue(command, "description", description);
                command.ExecuteNonQuery();
            }
        }

        public void MoveCalendarEvent(string id, DateTime start, DateTime end)
        {
            int i;
            if (int.TryParse(id, out i))
            {
                MoveCalendarEvent(i, start, end);
            }
        }
        public void MoveCalendarEvent(int id, DateTime start, DateTime end)
        {
            using (var connection = DataAccess.CreateConnection())
            {
                connection.Open();
                var command = DataAccess.CreateCommand("UPDATE [CalendarEvents] SET [Starting] = @start, [Ending] = @end WHERE [Id] = @id", connection);
                DataAccess.AddParameterWithValue(command, "id", id);
                DataAccess.AddParameterWithValue(command, "start", start);
                DataAccess.AddParameterWithValue(command, "end", end);
                command.ExecuteNonQuery();
            }
        }

        public CalendarEvent Get(string id)
        {
            int i;
            if (int.TryParse(id, out i))
            {
                return Get(i);
            }
            return null;
        }
        public CalendarEvent Get(int id)
        {
            var adapter = DataAccess.CreateDataAdapter("SELECT * FROM [CalendarEvents] WHERE Id = @id");
            DataAccess.AddParameterWithValue(adapter.SelectCommand, "Id", id);
            DataTable table = new DataTable();
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                return new CalendarEvent
                {
                    Id = id,
                    Description = (string)row["Description"],
                    Starting = (DateTime)row["Starting"],
                    Ending = (DateTime)row["Ending"]
                };
            }
            return null;
        }

        public void CreateCalendarEvent(string description, DateTime start, DateTime end)
        {
            using (var connection = DataAccess.CreateConnection())
            {
                connection.Open();
                var command = DataAccess.CreateCommand("INSERT INTO  [CalendarEvents] (Starting, Ending, [Description]) VALUES (@start, @end, @description)", connection);
                DataAccess.AddParameterWithValue(command, "start", start);
                DataAccess.AddParameterWithValue(command, "end", end);
                DataAccess.AddParameterWithValue(command, "description", description);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteCalendarEvent(string id)
        {
            int i;
            if (int.TryParse(id, out i))
            {
                DeleteCalendarEvent(i);
            }
        }
        public void DeleteCalendarEvent(int id)
        {
            using (var connection = DataAccess.CreateConnection())
            {
                connection.Open();
                var command = DataAccess.CreateCommand("DELETE FROM [CalendarEvents] WHERE Id = @id", connection);
                DataAccess.AddParameterWithValue(command, "id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}