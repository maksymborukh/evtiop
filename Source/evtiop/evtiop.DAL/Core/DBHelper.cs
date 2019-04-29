using System;
using System.Data;
using System.Data.Common;

namespace evtiop.DAL.Core
{
    public class DBHelper
    {
        #region Properties
        public string ConnectionString { get; set; }
        public ProviderManager ProviderManager { get; set; }
        #endregion

        #region Constructor
        public DBHelper()
        {
            ConnectionString = ConfigurationSetting.ConnectionString;
            ProviderManager = new ProviderManager();
        }

        public DBHelper(string connectionName)
        {
            ConnectionString = ConfigurationSetting.GetConnectionString(connectionName);
            ProviderManager = new ProviderManager(ConfigurationSetting.GetProviderName(connectionName));
        }

        public DBHelper(string connectionString, string providerName)
        {
            ConnectionString = connectionString;
            ProviderManager = new ProviderManager(providerName);
        }
        #endregion

        #region DB object
        public IDbConnection GetConnection()
        {
            try
            {
                var connection = ProviderManager.Factory.CreateConnection();
                connection.ConnectionString = ConnectionString;
                connection.Open();

                return connection;
            }
            catch (Exception)
            {
                throw new Exception("Error occured while creating connection.");
            }
        }

        public void CloseConnection(IDbConnection connection)
        {
            connection.Close();
        }

        public IDbCommand GetCommand(string commandText, IDbConnection connection, CommandType commandType)
        {
            try
            {
                IDbCommand command = ProviderManager.Factory.CreateCommand();
                command.CommandText = commandText;
                command.Connection = connection;
                command.CommandType = commandType;

                return command;
            }
            catch (Exception)
            {
                throw new Exception("Invalid parameter 'commandText'");
            }
        }

        public DbDataAdapter GetDataAdapter(IDbCommand command)
        {
            DbDataAdapter adapter = ProviderManager.Factory.CreateDataAdapter();
            adapter.SelectCommand = (DbCommand)command;
            adapter.InsertCommand = (DbCommand)command;
            adapter.UpdateCommand = (DbCommand)command;
            adapter.DeleteCommand = (DbCommand)command;

            return adapter;
        }

        public DbParameter GetParameter(string name, object value, DbType dbType)
        {
            try
            {
                DbParameter dbParameter = ProviderManager.Factory.CreateParameter();
                dbParameter.ParameterName = name;
                dbParameter.Value = value;
                dbParameter.Direction = ParameterDirection.Input;
                dbParameter.DbType = dbType;

                return dbParameter;
            }
            catch (Exception)
            {
                throw new Exception("Invalid parameter or type.");
            }
        }

        public DbParameter GetParameter(string name, object value, DbType dbType, ParameterDirection parameterDirection)
        {
            try
            {
                DbParameter dbParameter = ProviderManager.Factory.CreateParameter();
                dbParameter.ParameterName = name;
                dbParameter.Value = value;
                dbParameter.Direction = parameterDirection;
                dbParameter.DbType = dbType;

                return dbParameter;
            }
            catch (Exception)
            {
                throw new Exception("Invalid parameter or type.");
            }
        }

        public DbParameter GetParameter(string name, object value, DbType dbType, int size, ParameterDirection parameterDirection)
        {
            try
            {
                DbParameter dbParameter = ProviderManager.Factory.CreateParameter();
                dbParameter.ParameterName = name;
                dbParameter.Value = value;
                dbParameter.Size = size;
                dbParameter.Direction = parameterDirection;
                dbParameter.DbType = dbType;

                return dbParameter;
            }
            catch (Exception)
            {
                throw new Exception("Invalid parameter or type.");
            }
        }
        #endregion
    }
}
