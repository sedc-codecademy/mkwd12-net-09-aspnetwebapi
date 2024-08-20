using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qinshit.DataAccess.Domain
{
	public class Meal
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
        public string Day { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
