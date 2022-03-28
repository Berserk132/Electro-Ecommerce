using Electro_Project.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Electro_Project.Models
{
    public class Laptop : Product
    {

        [EnumDataType(typeof(LaptopRam))]

        public LaptopRam Ram { get; set;}
        [EnumDataType(typeof(LaptopRamType))]

        public LaptopRamType RamType { get; set; }
        [EnumDataType(typeof(LaptopGPU))]

        public LaptopGPU GPU { get; set; }
        [EnumDataType(typeof(LaptopOS))]

        public LaptopOS OS { get; set; }
        [EnumDataType(typeof(Color))]

        public Color Color { get; set; }
        [EnumDataType(typeof(LaptopScreen))]

        public LaptopScreen ScreenSize { get; set; }
        public string? CPU { get; set; }

        public float Width { get; set; }
        public float Height { get; set; }
        public float Thickness { get; set; }

        public float Weight { get; set; }



    }
}
