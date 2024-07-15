using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using ProductShop.Data;
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
            string productsXml = File.ReadAllText("../../../Datasets/products.xml");
            //string categoriesXml = File.ReadAllText("../../../Datasets/categories.xml");
            //string categoriesProductsXml = File.ReadAllText("../../../Datasets/categories-products.xml");

            Console.WriteLine(ImportProducts(context, productsXml));
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
        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProductImportDTO[]),
                new XmlRootAttribute("Products"));

            using var reader = new StringReader(inputXml);
            var importDtos = (ProductImportDTO[])xmlSerializer.Deserialize(reader);

            var userIds = context.Users
                .Select(u => u.Id)
                .ToList();

            var productsWithValidBuyers = importDtos
                .Where(p => userIds.Contains(p.BuyerId))
                .ToList();

            var products = productsWithValidBuyers
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
    }
}