namespace Cadastre.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;
    using Cadastre.Data;
    using Cadastre.Data.Enumerations;
    using Cadastre.Data.Models;
    using Cadastre.DataProcessor.ImportDtos;
    using Cadastre.Utilities;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private static XMLHelper? xmlHelper;

        private const string ErrorMessage =
            "Invalid Data!";
        private const string SuccessfullyImportedDistrict =
            "Successfully imported district - {0} with {1} properties.";
        private const string SuccessfullyImportedCitizen =
            "Succefully imported citizen - {0} {1} with {2} properties.";

        public static string ImportDistricts(CadastreContext dbContext, string xmlDocument)
        {
            StringBuilder sb = new StringBuilder();
            xmlHelper = new XMLHelper();

            ImportDistrictDto[] districtDtos = xmlHelper.Deserialize<ImportDistrictDto[]>(xmlDocument, "Districts");
            ICollection<District> validDistricts = new List<District>();

            foreach (var disDto in districtDtos)
            {
                if (!IsValid(disDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                if(dbContext.Districts.Any(d => d.Name == disDto.Name))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                District district = new District()
                {
                    Name = disDto.Name,
                    PostalCode = disDto.PostalCode,
                    Region = (Region)Enum.Parse(typeof(Region), disDto.Region)
                };

                foreach (var propDto in disDto.Properties)
                {
                    if (!IsValid(propDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime acquisitionDate = DateTime
                        .ParseExact(propDto.DateOfAcquisition, "dd/MM/yyyy", CultureInfo
                        .InvariantCulture, DateTimeStyles.None);

                    if(dbContext.Properties.Any(p => p.PropertyIdentifier == propDto.PropertyIdentifier) || district.Properties.Any(dp => dp.PropertyIdentifier == propDto.PropertyIdentifier))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    if(dbContext.Properties.Any(p => p.Address == propDto.Address) || district.Properties.Any(dp => dp.Address == propDto.Address))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Property property = new Property()
                    {
                        PropertyIdentifier = propDto.PropertyIdentifier,
                        Area = propDto.Area,
                        Details = propDto.Details,
                        Address = propDto.Address,
                        DateOfAcquisition = acquisitionDate
                    };

                    district.Properties.Add(property);
                }
                validDistricts.Add(district);
                sb.AppendLine(string.Format(SuccessfullyImportedDistrict, district.Name, district.Properties.Count));
            }
            dbContext.Districts.AddRange(validDistricts);
            dbContext.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportCitizens(CadastreContext dbContext, string jsonDocument)
        {
            StringBuilder sb = new StringBuilder();

            ImportCitizenDto[]? citizenDtos = JsonConvert.DeserializeObject<ImportCitizenDto[]>(jsonDocument);
            ICollection<Citizen> citizens = new List<Citizen>();

            foreach (var citDto in citizenDtos!)
            {
                if (!IsValid(citDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime dateOfBirth = DateTime
                    .ParseExact(citDto.BirthDate, "dd-MM-yyyy", CultureInfo
                    .InvariantCulture, DateTimeStyles.None);

                Citizen citizen = new Citizen()
                {
                    FirstName = citDto.FirstName,
                    LastName = citDto.LastName,
                    BirthDate = dateOfBirth,
                    MaritalStatus = (MaritalStatus)Enum.Parse(typeof(MaritalStatus), citDto.MaritalStatus)
                };

                foreach (var propId in citDto.Properties)
                {
                    PropertyCitizen propertyCitizen = new PropertyCitizen()
                    {
                        Citizen = citizen,
                        PropertyId = propId
                    };
                    citizen.PropertiesCitizens.Add(propertyCitizen);
                }
                citizens.Add(citizen);
                sb.AppendLine(string
                    .Format(SuccessfullyImportedCitizen, citizen.FirstName, citizen.LastName, citizen.PropertiesCitizens.Count));
            }
            dbContext.Citizens.AddRange(citizens);
            dbContext.SaveChanges();

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
