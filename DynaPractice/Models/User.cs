using System.ComponentModel.DataAnnotations;

namespace DynaPractice.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public List<Blueprint>? Blueprints { get; set; }

        public List<DataObject>? DataObjects { get; set; }
    }
}
