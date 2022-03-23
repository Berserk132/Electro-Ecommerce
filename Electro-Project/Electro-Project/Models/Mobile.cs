using Electro_Project.Models.Enums;
using Electro_Project.Models.Enums.Mobile;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Electro_Project.Models
{
    public class Mobile:Product
    {

        [EnumDataType(typeof(MobileRam))]
        public MobileRam Ram { get; set; }
        public string? GPU { get; set; }
        [EnumDataType(typeof(MobileOS))]

        public MobileOS OS { get; set; }
        [EnumDataType(typeof(Color))]

        public Color Color { get; set; }
        public string? CPU { get; set; }

        public Dimension? Dimension { get; set;}
        [ForeignKey("Dimension")]
        public int DimensionID { get; set; }
        public float Weight { get; set; }
    }
}
