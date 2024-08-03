using System.Globalization;
using Newtonsoft.Json;
using TravelAgency.Data;
using TravelAgency.Data.Models;
using TravelAgency.Data.Models.Enums;
using TravelAgency.DataProcessor.ExportDtos;
using TravelAgency.Utilities;

namespace TravelAgency.DataProcessor
{
    public class Serializer
    {
        public static string ExportGuidesWithSpanishLanguageWithAllTheirTourPackages(TravelAgencyContext context)
        {
            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRoot = "Guides";

            var guides = context.Guides
                .Where(g => g.Language == Language.Spanish)
                .Select(g => new ExportGuideDTO
                {
                    FullName = g.FullName,
                    TourPackages = g.TourPackagesGuides
                        .Select(tp => new ExportTourPackageDTO
                        {
                            Name = tp.TourPackage.PackageName,
                            Description = tp.TourPackage.Description,
                            Price = tp.TourPackage.Price,
                        })
                        .OrderByDescending(tp => tp.Price)
                        .ThenBy(tp => tp.Name)
                        .ToArray()
                })
                .OrderByDescending(g => g.TourPackages.Length)
                .ThenBy(g => g.FullName)
                .ToList();

            return xmlHelper.Serialize(guides, xmlRoot);
        }

        public static string ExportCustomersThatHaveBookedHorseRidingTourPackage(TravelAgencyContext context)
        {
            var customers = context.Customers
                .Where(c => c.Bookings.Any(
                    b => b.CustomerId == c.Id && b.TourPackage.PackageName == "Horse Riding Tour"))
                .Select(c => new
                {
                    c.FullName,
                    c.PhoneNumber,
                    Bookings = c.Bookings
                        .Where(b => b.CustomerId == c.Id && b.TourPackage.PackageName == "Horse Riding Tour")
                        .Select(b => new
                        {
                            TourPackageName = b.TourPackage.PackageName,
                            Date = b.BookingDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                        })
                        .OrderBy(b => b.Date)
                        .ToList()
                })
                .OrderByDescending(c => c.Bookings.Count)
                .ThenBy(c => c.FullName)
                .ToList();

            return JsonConvert.SerializeObject(customers, Formatting.Indented);
        }
    }
}
