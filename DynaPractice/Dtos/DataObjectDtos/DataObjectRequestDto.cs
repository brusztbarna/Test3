using DynaPractice.Models;
using System.ComponentModel.DataAnnotations;

namespace DynaPractice.Dtos.DataObjectDtos
{
    public class DataObjectRequestDto
    {
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
        [EnumDataType(typeof(Models.Type))]
        public Models.Type Type { get; set; }
    }
}
