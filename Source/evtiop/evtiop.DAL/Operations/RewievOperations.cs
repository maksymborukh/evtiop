using evtiop.DAL.Core;
using evtiop.DAL.Entities;
using evtiop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;

namespace evtiop.DAL.Operations
{
    public class RewievOperations : IOperations<Rewiev>
    {
        DBManager dbManager = new DBManager("shopdb");
        IDbConnection connection = null;
        public void Delete(long id)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", id, DbType.Int64));

            string commandText = "delete from rewievs where Id = @Id;";
            dbManager.Delete(commandText, CommandType.Text, parameters.ToArray());
        }

        public List<Rewiev> GetAll()
        {
            string commandText = "select * from rewievs";
            var dataReader = dbManager.GetDataReader(commandText, CommandType.Text, null, out connection);
            try
            {
                var rewievs = new List<Rewiev>();
                while (dataReader.Read())
                {
                    var rewiev = new Rewiev();
                    rewiev.ID = Convert.ToInt64(dataReader["Id"]);
                    rewiev.Rating = Convert.ToInt32(dataReader["Rating"]);
                    rewiev.Text = dataReader["Text"].ToString();
                    rewiev.RewieverID = Convert.ToInt64(dataReader["RewieverId"]);
                    rewiev.ProductID = Convert.ToInt64(dataReader["ProductId"]);
                    rewievs.Add(rewiev);
                }

                return rewievs;
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

        public Rewiev GetByID(long id)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", id, DbType.Int64));

            string commandText = "select * from rewievs where Id = @Id;";
            var dataReader = dbManager.GetDataReader(commandText, CommandType.Text, parameters.ToArray(), out connection);
            try
            {
                var rewiev = new Rewiev();
                while (dataReader.Read())
                {
                    rewiev.ID = Convert.ToInt64(dataReader["Id"]);
                    rewiev.Rating = Convert.ToInt32(dataReader["Rating"]);
                    rewiev.Text = dataReader["Text"].ToString();
                    rewiev.RewieverID = Convert.ToInt64(dataReader["RewieverId"]);
                    rewiev.ProductID = Convert.ToInt64(dataReader["ProductId"]);
                }

                return rewiev;
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

        public void Insert(Rewiev rewiev)
        {
            string commandText = "insert into rewievs (Rating, Text, RewieverId, ProductId)" +
                 "values (@Rating, @Text, @RewieverId, @ProductId);";
            dbManager.Insert(commandText, CommandType.Text, Param(rewiev).ToArray());
        }

        public List<Rewiev> SelectAll()
        {
            string commandText = "select * from rewievs";

            var rewievDataTable = dbManager.GetDataTable(commandText, CommandType.Text);
            var rewievs = new List<Rewiev>();
            foreach (DataRow row in rewievDataTable.Rows)
            {
                var rewiev = new Rewiev();
                rewiev.ID = Convert.ToInt64(row["Id"]);
                rewiev.Rating = Convert.ToInt32(row["Rating"]);
                rewiev.Text = row["Text"].ToString();
                rewiev.RewieverID = Convert.ToInt64(row["RewieverId"]);
                rewiev.ProductID = Convert.ToInt64(row["ProductId"]);
                rewievs.Add(rewiev);
            }

            return rewievs;
        }

        public void Update(Rewiev rewiev)
        {
            string commandText = "update rewievs set Rating = @Rating, Text = @Text, " +
                "RewieverId = @RewieverId, ProductId = @ProductId where Id = @Id;";
            dbManager.Update(commandText, CommandType.Text, Param(rewiev).ToArray());
        }

        public List<IDbDataParameter> Param(Rewiev rewiev)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", rewiev.ID, DbType.Int64));
            parameters.Add(dbManager.CreateParameter("@Rating", rewiev.Rating, DbType.String));
            parameters.Add(dbManager.CreateParameter("@Text", rewiev.Text, DbType.String));
            parameters.Add(dbManager.CreateParameter("@RewieverId", rewiev.RewieverID, DbType.Int64));
            parameters.Add(dbManager.CreateParameter("@ProductId", rewiev.ProductID, DbType.Int64));

            return parameters;
        }
    }
}
