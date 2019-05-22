using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace evtiop.BLL.DTO
{
    public class CartProduct : INotifyPropertyChanged
    {
        public string Description { get; set; }
        public string ImageURL { get; set; }
        private int Quantity { get; set; }
        public int Price { get; set; }
        private int Total { get; set; }
        public long Id { get; set; }
        public ImageSource ImageSource { get; set; }

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
