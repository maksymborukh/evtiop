using evtiop.DAL.Core;
using evtiop.DAL.Entities;
using evtiop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace evtiop.DAL.Operations
{
    public class CategoryProductOperations : IOperations<CategoryProduct>
    {
        DBManager dbManager = new DBManager("shopdb");
        IDbConnection connection = null;
        public void Delete(long id) //need to rewrite. id does not exist in table.
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", id, DbType.Int64));

            string commandText = "delete from categoryProducts where Id = @Id;";
            dbManager.Delete(commandText, CommandType.Text, parameters.ToArray());
        }

        public ObservableCollection<CategoryProduct> GetAll()
        {
            string commandText = "select * from categoryProducts";
            var dataReader = dbManager.GetDataReader(commandText, CommandType.Text, null, out connection);
            try
            {
                var categoryProducts = new ObservableCollection<CategoryProduct>();
                while (dataReader.Read())
                {
                    var categoryProduct = new CategoryProduct();
                    categoryProduct.CategoryID = Convert.ToInt64(dataReader["CategoryID"]);
                    categoryProduct.ProductID = Convert.ToInt64(dataReader["ProductID"]);
                    categoryProducts.Add(categoryProduct);
                }

                return categoryProducts;
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

        public CategoryProduct GetByID(long id) //can not get by id
        {
            throw new Exception("is not write");
        }

        public long GetScalarValue(string commandText)
        {
            object scalarValue = dbManager.GetScalarValue(commandText, CommandType.Text);
            return Convert.ToInt64(scalarValue);
        }

        public void Insert(CategoryProduct categoryProduct)
        {
            string commandText = "insert into categoryProducts (ProductId, CategoryId)" +
                 "values (@ProductId, @CategoryId);";
            dbManager.Insert(commandText, CommandType.Text, Param(categoryProduct).ToArray());
        }

        public ObservableCollection<CategoryProduct> SelectAll() //write new select
        {
            throw new Exception("is not write");
        }

        public void Update(CategoryProduct categoryProduct) //write new update
        {
            throw new Exception("is not write");
        }

        public List<IDbDataParameter> Param(CategoryProduct categoryProduct)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@ProductId", categoryProduct.ProductID, DbType.Int64));
            parameters.Add(dbManager.CreateParameter("@CategoryId", categoryProduct.CategoryID, DbType.Int64));

            return parameters;
        }
    }
}
