using evtiop.DAL.Core;
using evtiop.DAL.Entities;
using evtiop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;

namespace evtiop.DAL.Operations
{
    public class HelpOperations : IOperations<Help>
    {
        DBManager dbManager = new DBManager("shopdb");
        IDbConnection connection = null;
        public void Delete(long id)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", id, DbType.Int64));

            string commandText = "delete from helps where Id = @Id;";
            dbManager.Delete(commandText, CommandType.Text, parameters.ToArray());
        }

        public List<Help> GetAll()
        {
            string commandText = "select * from helps";
            var dataReader = dbManager.GetDataReader(commandText, CommandType.Text, null, out connection);
            try
            {
                var helps = new List<Help>();
                while (dataReader.Read())
                {
                    var help = new Help();
                    help.ID = Convert.ToInt64(dataReader["Id"]);
                    help.FirstName = dataReader["FirstName"].ToString();
                    help.LastName = dataReader["LastName"].ToString();
                    help.EmailAddress = dataReader["EmailAddress"].ToString();
                    help.Message = dataReader["Message"].ToString();
                    help.CreatedAt = Convert.ToDateTime(dataReader["CreatedAt"]);
                    help.ID = Convert.ToInt64(dataReader["Id"]);
                    helps.Add(help);
                }

                return helps;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataReader.Close();
                dbManager.CloseConnection(connection);
            }
        }

        public Help GetByID(long id)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", id, DbType.Int64));

            string commandText = "select * from helps where Id = @Id;";
            var dataReader = dbManager.GetDataReader(commandText, CommandType.Text, parameters.ToArray(), out connection);
            try
            {
                var help = new Help();
                while (dataReader.Read())
                {
                    help.ID = Convert.ToInt64(dataReader["Id"]);
                    help.FirstName = dataReader["FirstName"].ToString();
                    help.LastName = dataReader["LastName"].ToString();
                    help.EmailAddress = dataReader["EmailAddress"].ToString();
                    help.Message = dataReader["Message"].ToString();
                    help.CreatedAt = Convert.ToDateTime(dataReader["CreatedAt"]);
                    help.ID = Convert.ToInt64(dataReader["Id"]);
                }

                return help;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataReader.Close();
                dbManager.CloseConnection(connection);
            }
        }

        public long GetScalarValue(string commandText)
        {
            object scalarValue = dbManager.GetScalarValue(commandText, CommandType.Text);
            return Convert.ToInt64(scalarValue);
        }

        public void Insert(Help help)
        {
            string commandText = "insert into helps (FirstName, LastName, EmailAddress, Message, CreatedAt)" +
                 "values (@FirstName, @LastName, @EmailAddress, @Message, @CreatedAt);";
            dbManager.Insert(commandText, CommandType.Text, Param(help).ToArray());
        }

        public List<Help> SelectAll()
        {
            string commandText = "select * from helps";

            var helpDataTable = dbManager.GetDataTable(commandText, CommandType.Text);
            var helps = new List<Help>();
            foreach (DataRow row in helpDataTable.Rows)
            {
                var help = new Help();
                help.ID = Convert.ToInt64(row["Id"]);
                help.FirstName = row["FirstName"].ToString();
                help.LastName = row["LastName"].ToString();
                help.EmailAddress = row["EmailAddress"].ToString();
                help.Message = row["Message"].ToString();
                help.CreatedAt = Convert.ToDateTime(row["CreatedAt"]);
                help.ID = Convert.ToInt64(row["Id"]);
                helps.Add(help);
            }

            return helps;
        }

        public void Update(Help help)
        {
            string commandText = "update helps set FirstName = @FirstName, LastName = @LastName, " +
                "EmailAddress = @EmailAddress, Message = @Message, CreatedAt = @CreatedAt where Id = @Id;";
            dbManager.Update(commandText, CommandType.Text, Param(help).ToArray());
        }

        public List<IDbDataParameter> Param(Help help)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", help.ID, DbType.Int64));
            parameters.Add(dbManager.CreateParameter("@FirstName", 50, help.FirstName, DbType.String));
            parameters.Add(dbManager.CreateParameter("@LastName", help.LastName, DbType.String));
            parameters.Add(dbManager.CreateParameter("@EmailAddress", help.EmailAddress, DbType.String));
            parameters.Add(dbManager.CreateParameter("@Message", help.Message, DbType.String));
            parameters.Add(dbManager.CreateParameter("@CreatedAt", help.CreatedAt, DbType.DateTime));

            return parameters;
        }
    }
}
