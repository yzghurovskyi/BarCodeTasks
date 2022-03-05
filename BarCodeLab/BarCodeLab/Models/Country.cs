using System.Collections.Generic;
using System.ComponentModel;

namespace BarCodeLab.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Shoes> Shoes { get; set; }
    }
}
