using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DynaPractice.Models
{
    public class DataStructure
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [EnumDataType(typeof(DataType))]
        public DataType DataType { get; set; }
        [Required]
        public string DataValue { get; set; }
        public string DataMask { get; set; }
        public string DataUnit { get; set; }
        [EnumDataType(typeof(DataUnitPosition))]
        public DataUnitPosition DataUnitPosition { get; set; }
        [Required]
        [EnumDataType(typeof(DataLevel))]
        public DataLevel DataLevel { get; set; }

        public int DataObjectId { get; set; }

        [JsonIgnore]
        public DataObject DataObject { get; set; }
    }

    public enum DataType
    {
        NUMBER = 1,
        STRING = 2,
        DATE = 3,
        DATE_TIME = 4
    }
    public enum DataUnitPosition
    {
        BEFORE = 1,
        AFTER = 2
    }
    public enum DataLevel
    {
        NORMAL = 1,
        WARNING = 2,
        ERROR = 3
    }
}