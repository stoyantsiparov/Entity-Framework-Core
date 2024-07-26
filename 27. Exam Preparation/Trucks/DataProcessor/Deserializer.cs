using System.Text;
using Newtonsoft.Json;
using Trucks.Data.Models;
using Trucks.Data.Models.Enums;
using Trucks.DataProcessor.ImportDto;
using Trucks.Utilities;

namespace Trucks.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using Data;


    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedDespatcher
            = "Successfully imported despatcher - {0} with {1} trucks.";

        private const string SuccessfullyImportedClient
            = "Successfully imported client - {0} with {1} trucks.";

        public static string ImportDespatcher(TrucksContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRoot = "Despatchers";

            ICollection<Despatcher> despatchersToImport = new List<Despatcher>();

            var despatcherDtos = xmlHelper.Deserialize<ImportDespatcherDto[]>(xmlString, xmlRoot);
            foreach (var despatcherDto in despatcherDtos)
            {
                if (!IsValid(despatcherDto) || string.IsNullOrEmpty(despatcherDto.Position))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                ICollection<Truck> trucksToImport = new List<Truck>();
                foreach (var truckDto in despatcherDto.Trucks)
                {
                    if (!IsValid(truckDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Truck newTruck = new Truck
                    {
                        RegistrationNumber = truckDto.RegistrationNumber,
                        VinNumber = truckDto.VinNumber,
                        TankCapacity = truckDto.TankCapacity,
                        CargoCapacity = truckDto.CargoCapacity,
                        CategoryType = (CategoryType)truckDto.CategoryType,
                        MakeType = (MakeType)truckDto.MakeType,
                    };

                    trucksToImport.Add(newTruck);
                }

                Despatcher newDespatcher = new Despatcher
                {
                    Name = despatcherDto.Name,
                    Position = despatcherDto.Position,
                    Trucks = trucksToImport
                };

                despatchersToImport.Add(newDespatcher);
                sb.AppendLine(string.Format(SuccessfullyImportedDespatcher, newDespatcher.Name, newDespatcher.Trucks.Count));
            }

            context.Despatchers.AddRange(despatchersToImport);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }
        public static string ImportClient(TrucksContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            ICollection<Client> clientsToImport = new List<Client>();

            var clientDtos = JsonConvert.DeserializeObject<ImportClientDto[]>(jsonString);

            var validTruckIds = context.Trucks
                .Select(t => t.Id)
                .ToList();

            foreach (var clientDto in clientDtos)
            {
                if (!IsValid(clientDto) || clientDto.Type.ToLower() == "usual")
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Client newClient = new Client
                {
                    Name = clientDto.Name,
                    Nationality = clientDto.Nationality,
                    Type = clientDto.Type
                };

                foreach (var id in clientDto.TrucksIds.Distinct())
                {
                    if (!validTruckIds.Contains(id))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    ClientTruck newClientTruck = new ClientTruck
                    {
                        TruckId = id,
                        Client = newClient
                    };

                    newClient.ClientsTrucks.Add(newClientTruck);
                }

                if (!newClient.ClientsTrucks.Any())
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                clientsToImport.Add(newClient);
                sb.AppendLine(string.Format(SuccessfullyImportedClient, newClient.Name, newClient.ClientsTrucks.Count));
            }

            context.Clients.AddRange(clientsToImport);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}