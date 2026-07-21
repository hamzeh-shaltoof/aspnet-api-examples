using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicCommerce.Entities.Base
{
    [NotMapped]
    public abstract class BaseEntity 
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }


}
