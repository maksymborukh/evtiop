namespace evtiop.DAL.Entities
{
    public class Rewiev
    {
        public ulong ID { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }
        public ulong RewieverID { get; set; }   
        public ulong ProductID { get; set; }
    }
}
