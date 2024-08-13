namespace Qinshift.EShop.API.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int CreatedBy { get; set; } = 1;
        public DateTime ModifiedOn { get; set; } = DateTime.Now;
        public int ModifiedBy { get; set; } = 1;
    }
}
