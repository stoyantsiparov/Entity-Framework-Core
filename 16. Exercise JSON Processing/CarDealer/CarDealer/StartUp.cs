using System.Diagnostics;
using CarDealer.Data;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using Castle.Core.Resource;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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

            Console.WriteLine(GetSalesWithAppliedDiscount(context));
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
            var customers = context.Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver)
                .ToList()
                .Select(c => new
                {
                    c.Name,
                    BirthDate = c.BirthDate.ToString("dd/MM/yyyy"),
                    c.IsYoungDriver
                })
                .ToList();

            return SerializeObjectWithJsonSettings(customers);
        }

        //15. Export Cars From Make Toyota
        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var toyotaCars = context.Cars
                .Select(tc => new
                {
                    tc.Id,
                    tc.Make,
                    tc.Model,
                    tc.TraveledDistance
                })
                .Where(tc => tc.Make == "Toyota")
                .OrderBy(tc => tc.Model)
                .ThenByDescending(tc => tc.TraveledDistance)
                .ToList();

            return SerializeObjectWithJsonSettings(toyotaCars);
        }

        //16. Export Local Suppliers
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var localSuppliers = context.Suppliers
                .Where(s => !s.IsImporter)
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    PartsCount = s.Parts.Count
                })
                .ToList();

            return SerializeObjectWithJsonSettings(localSuppliers);
        }

        //17. Export Cars With Their List Of Parts
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars
                .Select(c => new
                {
                    car = new
                    {
                        c.Make,
                        c.Model,
                        c.TraveledDistance,

                    },
                    parts = c.PartsCars
                        .Select(pc => new
                        {
                            pc.Part.Name,
                            Price = pc.Part.Price.ToString("F2")
                        })
                        .ToList()
                })
                .ToList();

            return SerializeObjectWithJsonSettings(cars);
        }

        //18. Export Total Sales By Customer
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Where(c => c.Sales.Any())
                .Select(c => new
                {
                    FullName = c.Name,
                    BoughtCars = c.Sales.Count,
                    // SelectMany() -> Flattens the nested collection {PartsCars} associated with cars sold to the customer.
                    SpentMoney = c.Sales.SelectMany(s => s.Car.PartsCars)
                                                        .Sum(pc => pc.Part.Price)
                })
                .OrderByDescending(c => c.SpentMoney)
                .ThenByDescending(c => c.BoughtCars)
                .ToList();

            return SerializeObjectWithJsonSettingsWithCamelCase(customers);
        }

        //19. Export Sales With Applied Discount
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var salesWithDiscount = context.Sales
                .Take(10)
                .Select(s => new
                {
                    car = new
                    {
                        s.Car.Make,
                        s.Car.Model,
                        s.Car.TraveledDistance
                    },
                    CustomerName = s.Customer.Name,
                    Discount = s.Discount.ToString("F2"),
                    Price = s.Car.PartsCars.Sum(pc => pc.Part.Price).ToString("F2"),
                    PriceWithDiscount = (s.Car.PartsCars.Sum(pc => pc.Part.Price) * (1 - s.Discount / 100)).ToString("F2")
                })
                .ToList();


            return SerializeObjectWithJsonSettingsWithCamelCase(salesWithDiscount);
        }

        private static string SerializeObjectWithJsonSettings(object obj)
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            };

            return JsonConvert.SerializeObject(obj, settings);
        }

        private static string SerializeObjectWithJsonSettingsWithCamelCase(object obj)
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            };

            return JsonConvert.SerializeObject(obj, settings);
        }
    }
}