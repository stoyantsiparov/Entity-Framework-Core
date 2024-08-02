using System.Text;
using Artillery.Data.Models;
using Artillery.Data.Models.Enums;
using Artillery.DataProcessor.ImportDto;
using Artillery.Utilities;
using Newtonsoft.Json;

namespace Artillery.DataProcessor
{
    using Artillery.Data;
    using System.ComponentModel.DataAnnotations;

    public class Deserializer
    {
        private const string ErrorMessage =
            "Invalid data.";
        private const string SuccessfulImportCountry =
            "Successfully import {0} with {1} army personnel.";
        private const string SuccessfulImportManufacturer =
            "Successfully import manufacturer {0} founded in {1}.";
        private const string SuccessfulImportShell =
            "Successfully import shell caliber #{0} weight {1} kg.";
        private const string SuccessfulImportGun =
            "Successfully import gun {0} with a total weight of {1} kg. and barrel length of {2} m.";

        public static string ImportCountries(ArtilleryContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRoot = "Countries";

            ICollection<Country> countriesToImport = new List<Country>();

            var countryDtos = xmlHelper.Deserialize<ImportContryDTO[]>(xmlString, xmlRoot);
            foreach (var countryDto in countryDtos)
            {
                if (!IsValid(countryDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Country newCountry = new Country
                {
                    CountryName = countryDto.CountryName,
                    ArmySize = countryDto.ArmySize
                };

                countriesToImport.Add(newCountry);
                sb.AppendLine(string.Format(SuccessfulImportCountry, newCountry.CountryName, newCountry.ArmySize));
            }

            context.Countries.AddRange(countriesToImport);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportManufacturers(ArtilleryContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRoot = "Manufacturers";

            ICollection<Manufacturer> manufacturerToImport = new List<Manufacturer>();

            var manufacturerDtos = xmlHelper.Deserialize<ImportManufacturerDTO[]>(xmlString, xmlRoot);
            foreach (var dto in manufacturerDtos)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var foundedParts = dto.Founded.Split(", ");
                if (foundedParts.Length < 2)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var contryName = foundedParts.Last();
                var townName = string.Join(", ", foundedParts.Skip(foundedParts.Length - 2).Take(1));

                Manufacturer newManufacturer = new Manufacturer
                {
                    ManufacturerName = dto.ManufacturerName,
                    Founded = dto.Founded
                };

                if (manufacturerToImport.Any(m => m.ManufacturerName == newManufacturer.ManufacturerName))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                manufacturerToImport.Add(newManufacturer);
                sb.AppendLine(string.Format(SuccessfulImportManufacturer, newManufacturer.ManufacturerName, $"{townName}, {contryName}"));
            }

            context.Manufacturers.AddRange(manufacturerToImport);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportShells(ArtilleryContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRoot = "Shells";

            ICollection<Shell> shellsToImport = new List<Shell>();

            var shellDtos = xmlHelper.Deserialize<ImportShellDTO[]>(xmlString, xmlRoot);
            foreach (var dto in shellDtos)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Shell newShell = new Shell
                {
                    ShellWeight = dto.ShellWeight,
                    Caliber = dto.Caliber
                };

                shellsToImport.Add(newShell);
                sb.AppendLine(string.Format(SuccessfulImportShell, newShell.Caliber, newShell.ShellWeight));
            }

            context.Shells.AddRange(shellsToImport);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportGuns(ArtilleryContext context, string jsonString)
        {
            var validGunTypes = new[] { "Howitzer", "Mortar", "FieldGun", "AntiAircraftGun", "MountainGun", "AntiTankGun" };
            var gunsJson = JsonConvert.DeserializeObject<ImportGunDTO[]>(jsonString);
            var guns = new List<Gun>();
            var sb = new StringBuilder();

            foreach (var dto in gunsJson)
            {
                if (!IsValid(dto) || !validGunTypes.Contains(dto.GunType))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var gun = new Gun
                {
                    ManufacturerId = dto.ManufacturerId,
                    GunWeight = dto.GunWeight,
                    BarrelLength = dto.BarrelLength,
                    NumberBuild = dto.NumberBuild,
                    Range = dto.Range,
                    GunType = (GunType)Enum.Parse(typeof(GunType), dto.GunType),
                    ShellId = dto.ShellId
                };

                foreach (var countryDto in dto.Countries)
                {
                    gun.CountriesGuns.Add(new CountryGun
                    {
                        CountryId = countryDto.Id,
                        Gun = gun
                    });
                }

                guns.Add(gun);
                sb.AppendLine(string.Format(SuccessfulImportGun, dto.GunType, dto.GunWeight, dto.BarrelLength));
            }

            context.Guns.AddRange(guns);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }
        private static bool IsValid(object obj)
        {
            var validator = new ValidationContext(obj);
            var validationRes = new List<ValidationResult>();

            var result = Validator.TryValidateObject(obj, validator, validationRes, true);
            return result;
        }
    }
}