using DynaPractice.Models;
using System.ComponentModel.DataAnnotations;

namespace DynaPractice.Dtos.DataStructureDtos
{
    public class DataStructureRequestDto
    {
        [EnumDataType(typeof(Models.DataType))]
        public Models.DataType DataType { get; set; }
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
    }
}
