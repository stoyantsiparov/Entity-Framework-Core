using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using ProductShop.Data;
using ProductShop.DTOs.Export;
using ProductShop.DTOs.Import;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            ProductShopContext context = new ProductShopContext();
            //string usersXml = File.ReadAllText("../../../Datasets/users.xml");
            //string productsXml = File.ReadAllText("../../../Datasets/products.xml");
            //string categoriesXml = File.ReadAllText("../../../Datasets/categories.xml");
            //string categoriesProductsXml = File.ReadAllText("../../../Datasets/categories-products.xml");

            //Console.WriteLine(ImportCategoryProducts(context, categoriesProductsXml));

            Console.WriteLine(GetUsersWithProducts(context));
        }

        //01. Import Users
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(UserImportDTO[])
                , new XmlRootAttribute("Users"));

            using var reader = new StringReader(inputXml);
            var importDtos = (UserImportDTO[])xmlSerializer.Deserialize(reader);

            var users = importDtos
                .Select(dto => new User
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Age = dto.Age,
                })
                .ToList();

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Count}";
        }

        //02. Import Products
        ////TODO: WRONG
        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProductImportDTO[]),
                new XmlRootAttribute("Products"));

            using var reader = new StringReader(inputXml);
            var importDtos = (ProductImportDTO[])xmlSerializer.Deserialize(reader);

            var products = importDtos
                .Select(dto => new Product
                {
                    Name = dto.Name,
                    Price = dto.Price,
                    SellerId = dto.SellerId,
                    BuyerId = dto.BuyerId
                })
                .ToList();

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Count}";
        }

        //03. Import Categories
        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CategoriesImportDTO[]),
                new XmlRootAttribute("Categories"));

            using var reader = new StringReader(inputXml);
            var importDtos = (CategoriesImportDTO[])xmlSerializer.Deserialize(reader);

            var categories = importDtos
                .Where(dto => !string.IsNullOrEmpty(dto.Name))
                .Select(dto => new Category
                {
                    Name = dto.Name
                })
                .ToList();

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Count}";
        }

        //04. Import Categories and Products
        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CategoriesProductsImportDTO[]),
                new XmlRootAttribute("CategoryProducts"));

            using var reader = new StringReader(inputXml);
            var importDtos = (CategoriesProductsImportDTO[])xmlSerializer.Deserialize(reader);

            var categoryIds = context.Categories
                .Select(c => c.Id)
                .ToList();

            var productIds = context.Products
                .Select(p => p.Id)
                .ToList();

            var validCategoryProducts = new List<CategoryProduct>();

            foreach (var dto in importDtos)
            {
                if (categoryIds.Contains(dto.CategoryId) && productIds.Contains(dto.ProductId))
                {
                    validCategoryProducts.Add(new CategoryProduct
                    {
                        CategoryId = dto.CategoryId,
                        ProductId = dto.ProductId
                    });
                }
            }

            context.CategoryProducts.AddRange(validCategoryProducts);
            context.SaveChanges();

            return $"Successfully imported {validCategoryProducts.Count}";
        }

        //05. Export Products In Range
        ////TODO: WRONG
        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Select(p => new ProductInRangeExportDTO
                {
                    Name = p.Name,
                    Price = p.Price,
                    Buyer = $"{p.Buyer.FirstName} {p.Buyer.LastName}"
                })
                .Take(10)
                .ToArray();

            return SerializeToXml(products, "Products");
        }

        //06. Export Sold Products
        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Any())
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new UserExportDTO
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    SoldProducts = u.ProductsSold
                        .Select(p => new SoldProductsExportDTO
                        {
                            Name = p.Name,
                            Price = p.Price
                        }).ToArray()
                })
                .Take(5)
                .ToArray();

            return SerializeToXml(users, "Users");
        }

        //07. Export Categories By Products Count
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                .Select(c => new CategoryExportDTO
                {
                    Name = c.Name,
                    ProductCount = c.CategoryProducts.Count,
                    AveragePrice = c.CategoryProducts.Average(cp => (decimal?)cp.Product.Price) ?? 0m,
                    TotalRevenue = c.CategoryProducts.Sum(cp => (decimal?)cp.Product.Price) ?? 0m
                })
                .OrderByDescending(c => c.ProductCount)
                .ThenBy(c => c.TotalRevenue)
                .ToArray();

            return SerializeToXml(categories, "Categories");
        }

        //08. Export Users and Products
        //TODO: WRONG
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))
                .OrderByDescending(u => u.ProductsSold.Count(p => p.BuyerId != null))
                .ThenBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Take(10)
                .Select(u => new UserWithProductsExportDTO
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age,
                    SoldProducts = new SoldProductsDTO
                    {
                        Count = u.ProductsSold.Count(p => p.BuyerId != null),
                        Products = u.ProductsSold
                            .Where(p => p.BuyerId != null)
                            .OrderByDescending(p => p.Price)
                            .Select(p => new ProductExportDTO
                            {
                                Name = p.Name,
                                Price = p.Price
                            })
                            .ToArray()
                    }
                })
                .ToArray();

            var result = new UsersWithProductsExportDTO
            {
                Count = users.Length,
                Users = users
            };

            return SerializeToXml(result, "Users");
        }

        private static string SerializeToXml<T>(T dto, string xmlRootAttribute, bool omitDeclaration = false)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T), new XmlRootAttribute(xmlRootAttribute));
            StringBuilder stringBuilder = new StringBuilder();

            XmlWriterSettings settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = omitDeclaration,
                Encoding = new UTF8Encoding(false),
                Indent = true
            };

            using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture))
            using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings))
            {
                XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
                xmlSerializerNamespaces.Add(string.Empty, string.Empty);

                try
                {
                    xmlSerializer.Serialize(xmlWriter, dto, xmlSerializerNamespaces);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return stringBuilder.ToString();
        }
    }
}