using CarDealer.Data;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using Newtonsoft.Json;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            CarDealerContext context = new CarDealerContext();

            //string supplierText = File.ReadAllText("../../../Datasets/suppliers.json");
            //string partText = File.ReadAllText("../../../Datasets/parts.json");
            //string carText = File.ReadAllText("../../../Datasets/cars.json");
            //string customerText = File.ReadAllText("../../../Datasets/customers.json");
            //string saleText = File.ReadAllText("../../../Datasets/sales.json");
            
            //Console.WriteLine(ImportSales(context, saleText));
        }

        //09. Import Suppliers
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var suppliers = JsonConvert.DeserializeObject<List<Supplier>>(inputJson);

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count}.";
        }

        //10. Import Parts
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            // 1. Deserialize
            var parts = JsonConvert.DeserializeObject<List<Part>>(inputJson);

            // 2. Get valid Ids
            var validIds = context.Suppliers
                .Select(x => x.Id)
                .ToList();

            // 3. Filter parts based on the valid Ids
            var partsWithValidSupplierId = parts
                .Where(p => validIds.Contains(p.SupplierId))
                .ToList();

            context.Parts.AddRange(partsWithValidSupplierId);
            context.SaveChanges();

            return $"Successfully imported {partsWithValidSupplierId.Count}.";
        }

        //11. Import Cars
        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var carsDTOs = JsonConvert.DeserializeObject<List<ImportCarDTO>>(inputJson);

            var cars = new HashSet<Car>();
            var partsCars = new HashSet<PartCar>();

            foreach (var carDtO in carsDTOs)
            {
                var newCar = new Car()
                {
                    Make = carDtO.Make,
                    Model = carDtO.Model,
                    TraveledDistance = carDtO.TraveledDistance
                };
                
                cars.Add(newCar);
                foreach (var partId in carDtO.PartsId.Distinct())
                {
                    partsCars.Add(new PartCar
                    {
                        Car = newCar,
                        PartId = partId

                    });
                }
            }

            context.Cars.AddRange(cars);
            context.PartsCars.AddRange(partsCars);

            context.SaveChanges();

            return $"Successfully imported {cars.Count}.";
        }

        //12. Import Customers
        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var customers = JsonConvert.DeserializeObject<List<Customer>>(inputJson);

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Count}.";
        }

        //13. Import Sales
        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var sales = JsonConvert.DeserializeObject<List<Sale>>(inputJson);

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Count}.";
        }

        //14. Export Ordered Customers
        public static string GetOrderedCustomers(CarDealerContext context)
        {



            return "";
        }
    }
}