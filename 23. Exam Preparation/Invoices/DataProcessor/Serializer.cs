using System.Globalization;
using Invoices.DataProcessor.ExportDto;
using Invoices.Utility;
using Newtonsoft.Json;

namespace Invoices.DataProcessor
{
    using Invoices.Data;

    public class Serializer
    {
        public static string ExportClientsWithTheirInvoices(InvoicesContext context, DateTime date)
        {
            var xmlHelper = new XmlHelper();
            const string xmlRoot = "Clients";

            var clients = context.Clients
                .Where(c => c.Invoices.Any(i => i.IssueDate > date))
                .Select(c => new ExportClientDto
                {
                    ClientName = c.Name,
                    VatNumber = c.NumberVat,
                    InvoicesCount = c.Invoices.Count(),
                    Invoices = c.Invoices
                        .OrderBy(i => i.IssueDate)
                        .ThenByDescending(i => i.DueDate)
                        .Select(i => new ExportInvoiceDto
                        {
                            InvoiceNumber = i.Number,
                            InvoiceAmount = i.Amount,
                            Currency = i.CurrencyType.ToString(),
                            DueDate = i.DueDate.ToString("d", CultureInfo.InvariantCulture)
                        })
                        .ToArray(),
                })
                .OrderByDescending(c => c.InvoicesCount)
                .ThenBy(c => c.ClientName)
                .ToList();

            return xmlHelper.Serialize(clients, xmlRoot);
        }

        public static string ExportProductsWithMostClients(InvoicesContext context, int nameLength)
        {

            var products = context.Products
                .Where(p => p.ProductsClients.Any(pc => pc.Client.Name.Length >= nameLength))
                .Select(p => new
                {
                    p.Name,
                    p.Price,
                    Category = p.CategoryType.ToString(),
                    Clients = p.ProductsClients
                        .Where(pc => pc.Client.Name.Length >= nameLength)
                        .Select(pc => new
                        {
                            pc.Client.Name,
                            NumberVat = pc.Client.NumberVat
                        })
                        .OrderBy(c => c.Name)
                        .ToList()
                })
                .OrderByDescending(p => p.Clients.Count)
                .ThenBy(p => p.Name)
                .Take(5)
                .ToList();

            return JsonConvert.SerializeObject(products, Formatting.Indented);
        }
    }
}