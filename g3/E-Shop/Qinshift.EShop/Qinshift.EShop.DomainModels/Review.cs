namespace Qinshift.EShop.DomainModels
{
    public class Review : BaseEntity
    {
        public string ReviewerName { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public string ImageUrl { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
