using System.Collections.Generic;

namespace evtiop.DAL.Interfaces
{
    public interface IOperations<T> where T : class
    {
        List<T> GetAll();
        List<T> SelectAll();
        T GetByID(long id);
        void Insert(T item);
        void Update(T item);
        void Delete(long id);
        long GetScalarValue(string commandText);
    }
}
