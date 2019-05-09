using evtiop.DAL.Entities;
using evtiop.DAL.Operations;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace evtiop.BLL.ObsCollections
{
    public class CategoryRepository
    {
        public ObservableCollection<Category> categories;
        public CategoryRepository()
        {
            CategoryOperations categoryOperations = new CategoryOperations();
            List<Category> list = categoryOperations.GetAll();
            categories = new ObservableCollection<Category>(list);
        }
        public ObservableCollection<Category> GetCategories()
        {
            return categories;
        }
    }
}
