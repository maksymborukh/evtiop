using evtiop.DAL.Core;
using evtiop.DAL.Entities;
using evtiop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
namespace evtiop.DAL.Operations
{
    public class ManufacturerOperations : IOperations<Manufacturer>
    {
        DBManager dbManager = new DBManager("shopdb");
        IDbConnection connection = null;
        public void Delete(long id)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", id, DbType.Int64));

            string commandText = "delete from manufacturers where Id = @Id;";
            dbManager.Delete(commandText, CommandType.Text, parameters.ToArray());
        }

        public List<Manufacturer> GetAll()
        {
            string commandText = "select * from manufacturers";
            var dataReader = dbManager.GetDataReader(commandText, CommandType.Text, null, out connection);
            try
            {
                var manufacturers = new List<Manufacturer>();
                while (dataReader.Read())
                {
                    var manufacturer = new Manufacturer();
                    manufacturer.ID = Convert.ToInt64(dataReader["Id"]);
                    manufacturer.Name = dataReader["Name"].ToString();
                    manufacturer.ImageURL = dataReader["ImageURL"].ToString();
                    manufacturers.Add(manufacturer);
                }

                return manufacturers;
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

        public Manufacturer GetByID(long id)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", id, DbType.Int64));

            string commandText = "select * from manufacturers where Id = @Id;";
            var dataReader = dbManager.GetDataReader(commandText, CommandType.Text, parameters.ToArray(), out connection);
            try
            {
                var manufacturer = new Manufacturer();
                while (dataReader.Read())
                {
                    manufacturer.ID = Convert.ToInt64(dataReader["Id"]);
                    manufacturer.Name = dataReader["Name"].ToString();
                    manufacturer.ImageURL = dataReader["ImageURL"].ToString();
                }

                return manufacturer;
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

        public void Insert(Manufacturer manufacturer)
        {
            string commandText = "insert into customers (Name, ImageURL)" +
                 "values (@Name, @ImageURL);";
            dbManager.Insert(commandText, CommandType.Text, Param(manufacturer).ToArray());
        }

        public List<Manufacturer> SelectAll()
        {
            string commandText = "select * from manufacturers";

            var manufacturerDataTable = dbManager.GetDataTable(commandText, CommandType.Text);
            var manufacturers = new List<Manufacturer>();
            foreach (DataRow row in manufacturerDataTable.Rows)
            {
                var manufacturer = new Manufacturer();
                manufacturer.ID = Convert.ToInt64(row["Id"]);
                manufacturer.Name = row["Name"].ToString();
                manufacturer.ImageURL = row["ImageURL"].ToString();
                manufacturers.Add(manufacturer);
            }

            return manufacturers;
        }

        public void Update(Manufacturer manufacturer)
        {
            string commandText = "update manufacturers set Name = @Name, ImageURL = @ImageURL where Id = @Id;";
            dbManager.Update(commandText, CommandType.Text, Param(manufacturer).ToArray());
        }

        public List<IDbDataParameter> Param(Manufacturer manufacturer)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", manufacturer.ID, DbType.Int64));
            parameters.Add(dbManager.CreateParameter("@Name", 50, manufacturer.Name, DbType.String));
            parameters.Add(dbManager.CreateParameter("@ImageURL", manufacturer.ImageURL, DbType.String));

            return parameters;
        }
    }
}
