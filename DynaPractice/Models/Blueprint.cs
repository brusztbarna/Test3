using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DynaPractice.Models
{
    public class Blueprint
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [MaxLength(150)]
        public string Description { get; set; }
        [Required]
        [MaxLength(80)]
        public string ImgUrl { get; set; }
        [Required]
        [MaxLength(80)]
        public string AssetUrl { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
