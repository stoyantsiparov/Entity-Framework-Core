using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;
using TravelAgency.Data;
using TravelAgency.Data.Models;
using TravelAgency.DataProcessor.ImportDtos;
using TravelAgency.Utilities;

namespace TravelAgency.DataProcessor
{
    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data format!";
        private const string DuplicationDataMessage = "Error! Data duplicated.";
        private const string SuccessfullyImportedCustomer = "Successfully imported customer - {0}";
        private const string SuccessfullyImportedBooking = "Successfully imported booking. TourPackage: {0}, Date: {1}";

        public static string ImportCustomers(TravelAgencyContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRoot = "Customers";

            ICollection<Customer> customersToImport = new List<Customer>();

            var customerDtos = xmlHelper.Deserialize<ImportCustomerDTO[]>(xmlString, xmlRoot);
            foreach (var dto in customerDtos)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (customersToImport.Any(c => c.FullName == dto.FullName
                                                    || c.PhoneNumber == dto.PhoneNumber
                                                    || c.Email == dto.Email))
                {
                    sb.AppendLine(DuplicationDataMessage);
                    continue;
                }

                Customer newCustomer = new Customer
                {
                    FullName = dto.FullName,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                };

                customersToImport.Add(newCustomer);
                sb.AppendLine(string.Format(SuccessfullyImportedCustomer, newCustomer.FullName));
            }

            context.Customers.AddRange(customersToImport);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportBookings(TravelAgencyContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            ICollection<Booking> bookingsToImport = new List<Booking>();

            var bookingDtos = JsonConvert.DeserializeObject<ImportBookingDTO[]>(jsonString);
            foreach (var dto in bookingDtos)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (!DateTime.TryParseExact(dto.BookingDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None,
                            out DateTime bookingDate))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var customer = context.Customers
                    .SingleOrDefault(c => c.FullName == dto.CustomerName);

                var tourPackage = context.TourPackages
                    .SingleOrDefault(tp => tp.PackageName == dto.TourPackageName);

                Booking newBooking = new Booking
                {
                    BookingDate = bookingDate,
                    CustomerId = customer.Id,
                    TourPackageId = tourPackage.Id
                };

                bookingsToImport.Add(newBooking);
                sb.AppendLine(string.Format(SuccessfullyImportedBooking, tourPackage.PackageName, bookingDate.ToString("yyyy-MM-dd")));
            }

            context.Bookings.AddRange(bookingsToImport);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static bool IsValid(object dto)
        {
            var validateContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(dto, validateContext, validationResults, true);

            foreach (var validationResult in validationResults)
            {
                string currValidationMessage = validationResult.ErrorMessage;
            }

            return isValid;
        }
    }
}
