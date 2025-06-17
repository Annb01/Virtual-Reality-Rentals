using Bogus;
using VR_Rentals.Models;
using BCrypt.Net;


namespace VR_Rentals.Data
{
    public class DbInitializer
    {

        public static void SeedData(RentalContext context)
        {
            if (!context.Customers.Any())
            {
                var customers = GenerateFakeCustomers();
                context.Customers.AddRange(customers);
                context.SaveChanges();
            }

            if (!context.VrEquipments.Any())
            {
                var equipments = InitialEquipment();
                context.VrEquipments.AddRange(equipments);
                context.SaveChanges();
            }

            if (!context.Rentals.Any())
            {
                var rentals = InitialRentals(context);
                context.Rentals.AddRange(rentals);
                context.SaveChanges();
            }
        }
        public static List<Customer> GenerateFakeCustomers()
        {
            var customerFaker = new Faker<Customer>()
                .RuleFor(c => c.CustomerName, f => f.Name.FirstName())
                .RuleFor(c => c.CustomerSurname, f => f.Name.LastName())
                .RuleFor(c => c.Email, f => f.Internet.Email())
                .RuleFor(c => c.PasswordHash, f => BCrypt.Net.BCrypt.HashPassword(f.Internet.Password()));

            return customerFaker.Generate(3);
        }


        public static List<VrEquipment> InitialEquipment()
        {
            var vrEquipments = new List<VrEquipment>
            {
                new VrEquipment
                {
                    EquipmentName = "Oculus Quest 2 Full Set",
                    TypeName = TypeEnum.FullSet,
                    Manufacturer = "Meta",
                    PricePerDay = 150,
                    EquipmentStatus = Status.Rented,
                    NeedController = false,
                    Description = "Kompletne urządzenie VR z goglami i kontrolerami, idealne do gier i doświadczeń VR.",
                    Image = GetImageBytes(@"DbInitial\Image\Oculus Quest 2 Full Set.jpg")
                },
                new VrEquipment
                {
                    EquipmentName = "HTC Vive Pro Full Set",
                    TypeName = TypeEnum.FullSet,
                    Manufacturer = "HTC",
                    PricePerDay = 170,
                    EquipmentStatus = Status.Available,
                    NeedController = false,
                    Description = "Profesjonalny zestaw VR z goglami, kontrolerami i pełnym śledzeniem ruchu.",
                    Image = GetImageBytes(@"DbInitial\Image\HTC Vive Pro Full Set.jpg")
                },
                new VrEquipment
                {
                    EquipmentName = "Oculus Rift S",
                    TypeName = TypeEnum.VRHeadset,
                    Manufacturer = "Meta",
                    PricePerDay = 50,
                    EquipmentStatus = Status.Rented,
                    NeedController = true,
                    Description = "Wysokiej jakości gogle VR kompatybilne z PC, wymagające kontrolerów do interakcji.",
                    Image = GetImageBytes(@"DbInitial\Image\Oculus Rift S.png")
                },
                new VrEquipment
                {
                    EquipmentName = "Oculus Rift S Touch Controllers",
                    TypeName = TypeEnum.Controllers,
                    Manufacturer = "Meta",
                    PricePerDay = 15,
                    EquipmentStatus = Status.Rented,
                    NeedController = false,
                    Description = "Oficjalne kontrolery do Oculus Rift S, zapewniające precyzyjne śledzenie ruchów w VR.",
                    Image = GetImageBytes(@"DbInitial\Image\Oculus Rift S Touch Controllers.png")
                },
                new VrEquipment
                {
                    EquipmentName = "Valve Index",
                    TypeName = TypeEnum.VRHeadset,
                    Manufacturer = "Valve",
                    PricePerDay = 80,
                    EquipmentStatus = Status.Available,
                    NeedController = true,
                    Description = "Najwyższej klasy gogle VR z precyzyjnym śledzeniem ruchów i obsługą kontrolerów Index.",
                    Image = GetImageBytes(@"DbInitial\Image\Valve Index.jpg")
                },
                new VrEquipment
                {
                    EquipmentName = "Valve Index Controllers",
                    TypeName = TypeEnum.Controllers,
                    Manufacturer = "Valve",
                    PricePerDay = 25,
                    EquipmentStatus = Status.Available,
                    NeedController = false,
                    Description = "Zaawansowane kontrolery do Valve Index, pozwalające na śledzenie ruchów palców.",
                    Image = GetImageBytes(@"DbInitial\Image\Valve Index Controllers.jpg")
                },
                new VrEquipment
                {
                    EquipmentName = "Pimax 8K X",
                    TypeName = TypeEnum.VRHeadset,
                    Manufacturer = "Pimax",
                    PricePerDay = 100,
                    EquipmentStatus = Status.Available,
                    NeedController = true,
                    Description = "Najwyższej rozdzielczości gogle VR z szerokim polem widzenia, kompatybilne z kontrolerami Pimax Sword.",
                    Image = GetImageBytes(@"DbInitial\Image\Pimax 8K X.jpg")
                },
                new VrEquipment
                {
                    EquipmentName = "Pimax Sword Controllers",
                    TypeName = TypeEnum.Controllers,
                    Manufacturer = "Pimax",
                    PricePerDay = 30,
                    EquipmentStatus = Status.Rented,
                    NeedController = false,
                    Description = "Precyzyjne kontrolery do gogli Pimax, zapewniające pełne śledzenie ruchów w przestrzeni VR.",
                    Image = GetImageBytes(@"DbInitial\Image\Pimax Sword Controllers.jpg")
                }
            };

            return vrEquipments;
        }

        public static byte[] GetImageBytes(string imagePath)
        {
            if (File.Exists(imagePath))
            {
                return File.ReadAllBytes(imagePath);
            }
            else
            {
                throw new FileNotFoundException($"The image file at path {imagePath} was not found.");
            }
        }

        public static List<Rental> InitialRentals(RentalContext context)
        {

            var customers = context.Customers.ToList();
            var equipments = context.VrEquipments.ToList();

            if (customers.Count < 2 || equipments.Count < 2)
                return new List<Rental>();


            return new List<Rental>
            {
                new Rental
                {
                    CustomerId = customers[0].CustomerId,
                    EquipmentId = equipments[0].EquipmentId, 
                    RentalDate = DateTime.Now.AddDays(-5),
                    PlannedReturnDate = DateTime.Now.AddDays(5),
                    ReturnDate = null,
                    TotalRentalCost = 10 * equipments[0].PricePerDay
                },

                new Rental
                {
                    CustomerId = customers[1].CustomerId,
                    EquipmentId = equipments[2].EquipmentId,
                    RentalDate = DateTime.Now.AddDays(-1),
                    PlannedReturnDate = DateTime.Now.AddDays(10),
                    ReturnDate = null,
                    TotalRentalCost = 11 * equipments[2].PricePerDay
                },

                new Rental
                {
                    CustomerId = customers[1].CustomerId,
                    EquipmentId = equipments[3].EquipmentId,
                    RentalDate = DateTime.Now.AddDays(-1),
                    PlannedReturnDate = DateTime.Now.AddDays(10),
                    ReturnDate = null,
                    TotalRentalCost = 11 * equipments[3].PricePerDay
                },

                new Rental
                {
                    CustomerId = customers[2].CustomerId,
                    EquipmentId = equipments[7].EquipmentId,
                    RentalDate = DateTime.Now.AddDays(-12),
                    PlannedReturnDate = DateTime.Now.AddDays(-4),
                    ReturnDate = DateTime.Now,
                    TotalRentalCost = 12 * equipments[7].PricePerDay
                },
            };

        }

    }
}
