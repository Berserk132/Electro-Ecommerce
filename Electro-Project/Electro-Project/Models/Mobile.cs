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

        public float Width { get; set; }
        public float Height { get; set; }
        public float Thickness { get; set; }
        public float Weight { get; set; }
    }
}
