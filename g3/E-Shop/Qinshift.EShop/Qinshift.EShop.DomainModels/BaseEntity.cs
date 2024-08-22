using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qinshift.EShop.DomainModels
{
    public abstract class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int CreatedBy { get; set; } = 1;
        public DateTime ModifiedOn { get; set; } = DateTime.Now;
        public int ModifiedBy { get; set; } = 1;
    }
}
