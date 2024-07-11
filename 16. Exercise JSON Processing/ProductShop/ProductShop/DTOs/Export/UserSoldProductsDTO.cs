using Newtonsoft.Json;

namespace ProductShop.DTOs.Export;

public class UserSoldProductsDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public IEnumerable<SoldProductDTO> SoldProducts { get; set; }
}