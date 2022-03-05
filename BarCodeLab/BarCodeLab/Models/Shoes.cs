using System;
using System.ComponentModel.DataAnnotations;

namespace BarCodeLab.Models
{
    public enum Season
    {
        Winter, Summer, Mid
    }

    public enum Gender
    {
        Male, Female
    }

    public class Shoes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int ManufacturerId { get; set; }
        public Country Manufacturer { get; set; }

        [DataType(DataType.Date)] 
        public DateTime DeliveryDate { get; set; }
        [EnumDataType(typeof(Season))]
        public Season Season { get; set; }
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }
    }
}
