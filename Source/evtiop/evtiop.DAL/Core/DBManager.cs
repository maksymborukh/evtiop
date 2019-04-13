using System;
using System.Data;

namespace evtiop.DAL.Core
{
    public class DBManager //IDispodable
    {
        private DBHelper database;

        public DBManager(string connectionStringName)
        {
            database = new DBHelper(connectionStringName);
        }

        public IDbConnection GetDbConnection()
        {
            return database.GetConnection();
        }
        
        public void CloseConnection(IDbConnection connection)
        {
            database.CloseConnection(connection);
        }

        public IDbDataParameter CreateParameter(string name, object value, DbType dbType)
        {
            return database.GetParameter(name, value, dbType, ParameterDirection.Input);
        }

        public IDbDataParameter CreateParameter(string name, int size, object value, DbType dbType)
        {
            return database.GetParameter(name, value, dbType, size, ParameterDirection.Input);
        }

        public IDbDataParameter CreateParameter(string name, int size, object value, DbType dbType, ParameterDirection parameterDirection)
        {
            return database.GetParameter(name, value, dbType, size, parameterDirection);
        }

        public DataTable GetDataTable(string commandText, CommandType commandType, IDbDataParameter[] parameters = null)
        {
            using (var connection = database.GetConnection())
            {
                using (var command = database.GetCommand(commandText, connection, commandType))
                {
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    var dataset = new DataSet();
                    var dataAdapter = database.GetDataAdapter(command);
                    dataAdapter.Fill(dataset);
                    return dataset.Tables[0];
                }
            }
        }

        public DataSet GetDataSet(string commandText, CommandType commandType, IDbDataParameter[] parameters = null)
        {
            using (var connection = database.GetConnection())
            {
                using (var command = database.GetCommand(commandText, connection, commandType))
                {
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    var dataset = new DataSet();
                    var dataAdapter = database.GetDataAdapter(command);
                    dataAdapter.Fill(dataset);
                    return dataset;
                }
            }
        }

        public IDataReader GetDataReader(string commandText, CommandType commandType, IDbDataParameter[] parameters, out IDbConnection connection)
        {
            IDataReader reader = null;
            connection = database.GetConnection();

            var command = database.GetCommand(commandText, connection, commandType);
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }

            reader = command.ExecuteReader();
            return reader;
        }

        public void Delete(string commandText, CommandType commandType, IDbDataParameter[] parameters = null)
        {
            using (var connection = database.GetConnection())
            {
                using (var command = database.GetCommand(commandText, connection, commandType))
                {
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Insert(string commandText, CommandType commandType, IDbDataParameter[] parameters)
        {
            using (var connection = database.GetConnection())
            {
                using (var command = database.GetCommand(commandText, connection, commandType))
                {
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    command.ExecuteNonQuery();
                }
            }
        }

        public long Insert(string commandText, CommandType commandType, IDbDataParameter[] parameters, out long lastID)
        {
            lastID = 0;
            using (var connection = database.GetConnection())
            {
                using (var command = database.GetCommand(commandText, connection, commandType))
                {
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    object newID = command.ExecuteScalar();
                    lastID = Convert.ToInt64(newID);
                }
            }

            return lastID;
        }

        public void InsertWithTransaction(string commandText, CommandType commandType, IDbDataParameter[] parameters)
        {
            IDbTransaction transaction = null;

            using (var connection = database.GetConnection())
            {
                transaction = connection.BeginTransaction();
                using (var command = database.GetCommand(commandText, connection, commandType))
                {
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    try
                    {
                        command.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public void Update(string commandText, CommandType commandType, IDbDataParameter[] parameters)
        {
            using (var connection = database.GetConnection())
            {
                using (var command = database.GetCommand(commandText, connection, commandType))
                {
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateWithTransaction(string commandText, CommandType commandType, IDbDataParameter[] parameters)
        {
            IDbTransaction transaction = null;

            using (var connection = database.GetConnection())
            {
                transaction = connection.BeginTransaction();
                using (var command = database.GetCommand(commandText, connection, commandType))
                {
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    try
                    {
                        command.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public object GetScalarValue(string commandText, CommandType commandType, IDbDataParameter[] parameters = null)
        {
            using (var connection = database.GetConnection())
            {
                using (var command = database.GetCommand(commandText, connection, commandType))
                {
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    return command.ExecuteScalar();
                }
            }
        }
    }
}
