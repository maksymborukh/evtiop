using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace evtiop.BLL.DTO
{
    public class CartProduct : INotifyPropertyChanged
    {
        public string Description { get; set; }
        private int Quantity { get; set; }
        public int Price { get; set; }
        private int Total { get; set; }
        public long Id { get; set; }

        public int quantity
        {
            get { return Quantity; }
            set
            {
                Quantity = value;
                OnPropertyChanged("quantity");
            }
        }

        public int total
        {
            get { return Total; }
            set
            {
                Total = value;
                OnPropertyChanged("total");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
