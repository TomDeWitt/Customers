namespace AscentCustomers.Calendar
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.Common;

    public class DataAccess
    {
        #region Private methods and properties

        //private static string ConnectionString => ConfigurationManager.ConnectionStrings["CalendarEvents"].ConnectionString;

        //private static DbProviderFactory Factory => DbProviderFactories.GetFactory(FactoryName());
        private static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["CalendarEvents"].ConnectionString; }
        }

        private static DbProviderFactory Factory
        {
            get { return DbProviderFactories.GetFactory(FactoryName()); }
        }

        private static string FactoryName()
        {
            return "System.Data.SqlClient";
        }

        private static string IdentityCommand()
        {
            switch (FactoryName())
            {
                case "System.Data.SqlClient":
                    return "select @@identity;";
                default:
                    throw new NotSupportedException("Unsupported Database Factory in DataAccess class.");
            }
        }

        #endregion //Private methods and properties

        #region Public methods

        public static DbDataAdapter CreateDataAdapter(string command)
        {
            DbDataAdapter adapter = Factory.CreateDataAdapter();
            adapter.SelectCommand = CreateCommand(command);
            return adapter;
        }

        public static DbConnection CreateConnection()
        {
            DbConnection connection = Factory.CreateConnection();
            connection.ConnectionString = ConnectionString;
            return connection;
        }

        public static DbCommand CreateCommand(string commandText)
        {
            DbCommand command = Factory.CreateCommand();
            command.CommandText = commandText;
            command.Connection = CreateConnection();
            return command;
        }

        public static DbCommand CreateCommand(string commandText, DbConnection connection)
        {
            DbCommand command = Factory.CreateCommand();
            command.CommandText = commandText;
            command.Connection = connection;
            return command;
        }

        public static void AddParameterWithValue(DbCommand command, string parameterName, object parameterValue)
        {
            var parameter = Factory.CreateParameter();
            parameter.Direction = ParameterDirection.Input;
            parameter.ParameterName = parameterName;
            parameter.Value = parameterValue;
            command.Parameters.Add(parameter);
        }

        public static int GetIdentity(DbConnection connection)
        {
            var command = CreateCommand(IdentityCommand(), connection);
            return Convert.ToInt32(command.ExecuteScalar());
        }

        #endregion //Public methods
    }
}