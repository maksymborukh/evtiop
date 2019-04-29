namespace evtiop.DAL.Entities
{
    public class Rewiev
    {
        public long ID { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }
        public long RewieverID { get; set; }   
        public long ProductID { get; set; }
    }
}
