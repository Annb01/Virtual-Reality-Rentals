using System.Security.Cryptography.X509Certificates;

namespace VR_Rentals.Models
{
    public enum TypeEnum
    {
        VRHeadset,
        FullSet,
        Controllers
    }
    public enum Status
    {
        Rented,
        Available
    }
    public class VrEquipment
    {
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public TypeEnum TypeName { get; set; }
        public string Manufacturer { get; set; }
        public double PricePerDay { get; set; }
        public Status EquipmentStatus { get; set; }
        public bool NeedController { get; set; }
        public byte[] Image { get; set; }
        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    }
}
