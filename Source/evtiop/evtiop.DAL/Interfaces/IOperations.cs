using System.Collections.ObjectModel;

namespace evtiop.DAL.Interfaces
{
    public interface IOperations<T> where T : class
    {
        ObservableCollection<T> GetAll();
        ObservableCollection<T> SelectAll();
        T GetByID(long id);
        void Insert(T item);
        void Update(T item);
        void Delete(long id);
        long GetScalarValue();
    }
}
