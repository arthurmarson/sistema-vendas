using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class EntityBase
    {
        [Key]
        public int Codigo { get; set; }
    }
}
