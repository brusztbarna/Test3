using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DynaPractice.Models
{
    public class DataObject
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Comment { get; set; }
        [Required]
        [MaxLength(50)]
        public string DeviceName { get; set; }
        [Required]
        [MaxLength(50)]
        public string DeviceId { get; set; }
        public int DataTypeId { get; set; }
        [Required]
        [MaxLength(50)]
        public string DataTypeName { get; set; }
        [MaxLength(255)]
        public string DataSample { get; set; }
        [Required]
        [MaxLength(100)]
        public string Url { get; set; }
        [Required]
        [EnumDataType(typeof(CallMethod))]
        public CallMethod CallMethod { get; set; }
        [Required]
        [EnumDataType(typeof(Type))]
        public Type Type { get; set; }

        [JsonIgnore]
        public DataStructure DataStructure { get; set; }

        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }

    public enum CallMethod
    {
        POST = 1,
        GET = 2
    }
    public enum Type
    {
        REST = 1
    }
}
