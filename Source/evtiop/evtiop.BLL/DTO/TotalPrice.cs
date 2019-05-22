using evtiop.BLL.Static;
using evtiop.DAL.Operations;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace evtiop.BLL.DTO
{
    public class TotalPrice : INotifyPropertyChanged
    {
        public TotalPrice()
        {
            sumUp = StaticBasketInfo.SumUp;

            BasketOperations basketOperations = new BasketOperations();
            long basketId = basketOperations.GetByCustomerID(StaticUserInfo.CustomerId);

            BasketProductOperations basketProductOperations = new BasketProductOperations();
            long a = basketProductOperations.GetScalarValue($"select count(ProductId) from basketProducts where BasketId = {basketId};");

            StaticBasketInfo.ProductsInBasket = Convert.ToInt32(a);
            amount = StaticBasketInfo.ProductsInBasket;
        }
        private int SumUp { get; set; }
        private int Amount { get; set; }

        public int sumUp
        {
            get { return SumUp; }
            set
            {
                SumUp = value;
                OnPropertyChanged("sumUp");
            }
        }
        public int amount
        {
            get { return Amount; }
            set
            {
                Amount = value;
                OnPropertyChanged("amount");
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
