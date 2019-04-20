using evtiop.DAL.Core;
using evtiop.DAL.Entities;
using evtiop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace evtiop.DAL.Operations
{
    public class CategoryOperations : IOperations<Category>
    {
        DBManager dbManager = new DBManager("shopdb");
        IDbConnection connection = null;
        public void Delete(long id)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", id, DbType.Int64));

            string commandText = "delete from categories where Id = @Id;";
            dbManager.Delete(commandText, CommandType.Text, parameters.ToArray());
        }

        public ObservableCollection<Category> GetAll()
        {
            string commandText = "select * from categories";
            var dataReader = dbManager.GetDataReader(commandText, CommandType.Text, null, out connection);
            try
            {
                var categories = new ObservableCollection<Category>();
                while (dataReader.Read())
                {
                    var category = new Category();
                    category.ID = Convert.ToInt64(dataReader["Id"]);
                    category.Name = dataReader["Name"].ToString();
                    category.ImageURL = dataReader["ImageURL"].ToString();
                    category.SortOrder = dataReader["SortOrder"].ToString();
                    categories.Add(category);
                }

                return categories;
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

        public Category GetByID(long id)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", id, DbType.Int64));

            string commandText = "select * from categories where Id = @Id;";
            var dataReader = dbManager.GetDataReader(commandText, CommandType.Text, parameters.ToArray(), out connection);
            try
            {
                var category = new Category();
                while (dataReader.Read())
                {
                    category.ID = Convert.ToInt64(dataReader["Id"]);
                    category.Name = dataReader["Name"].ToString();
                    category.ImageURL = dataReader["ImageURL"].ToString();
                    category.SortOrder = dataReader["SortOrder"].ToString();
                }

                return category;
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

        public void Insert(Category category)
        {
            string commandText = "insert into categories (Name, ImageURL, SortOrder)" +
                 "values (@Name, @ImageURL, @SortOrder);";
            dbManager.Insert(commandText, CommandType.Text, Param(category).ToArray());
        }

        public ObservableCollection<Category> SelectAll()
        {
            string commandText = "select * from categories";

            var categoryDataTable = dbManager.GetDataTable(commandText, CommandType.Text);
            var categories = new ObservableCollection<Category>();
            foreach (DataRow row in categoryDataTable.Rows)
            {
                var category = new Category();
                category.ID = Convert.ToInt64(row["Id"]);
                category.Name = row["Name"].ToString();
                category.ImageURL = row["ImageURL"].ToString();
                category.SortOrder = row["SortOrder"].ToString();
                categories.Add(category);
            }

            return categories;
        }

        public void Update(Category category)
        {
            string commandText = "update categories set Name = @Name, ImageURL = @ImageURL, SortOrder = @SortOrder where Id = @Id;";
            dbManager.Update(commandText, CommandType.Text, Param(category).ToArray());
        }

        public List<IDbDataParameter> Param(Category category)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", category.ID, DbType.Int64));
            parameters.Add(dbManager.CreateParameter("@Name", 50, category.Name, DbType.String));
            parameters.Add(dbManager.CreateParameter("@ImageURL", category.ImageURL, DbType.String));
            parameters.Add(dbManager.CreateParameter("@SortOrder", 50, category.SortOrder, DbType.String));

            return parameters;
        }
    }
}
