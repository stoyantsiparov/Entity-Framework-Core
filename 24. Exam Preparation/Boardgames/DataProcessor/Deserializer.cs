using System.Text;
using Boardgames.Data.Models;
using Boardgames.Data.Models.Enums;
using Boardgames.DataProcessor.ImportDto;
using Boardgames.Utilities;
using Newtonsoft.Json;

namespace Boardgames.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Boardgames.Data;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCreator
            = "Successfully imported creator – {0} {1} with {2} boardgames.";

        private const string SuccessfullyImportedSeller
            = "Successfully imported seller - {0} with {1} boardgames.";

        public static string ImportCreators(BoardgamesContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRoot = "Creators";

            ICollection<Creator> creatorsToImport = new List<Creator>();

            var creatorsDtos = xmlHelper.Deserialize<ImportCreatorDto[]>(xmlString, xmlRoot);
            foreach (var creatorDto in creatorsDtos)
            {
                if (!IsValid(creatorDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                ICollection<Boardgame> boardgamesToImport = new List<Boardgame>();
                foreach (var boardgamesDto in creatorDto.Boardgames)
                {
                    if (!IsValid(boardgamesDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Boardgame newBoardgame = new Boardgame
                    {
                        Name = boardgamesDto.Name,
                        Rating = boardgamesDto.Rating,
                        YearPublished = boardgamesDto.YearPublished,
                        CategoryType = (CategoryType)boardgamesDto.CategoryType,
                        Mechanics = boardgamesDto.Mechanics,
                    };

                    boardgamesToImport.Add(newBoardgame);
                }

                Creator newCreator = new Creator
                {
                    FirstName = creatorDto.FirstName,
                    LastName = creatorDto.LastName,
                    Boardgames = boardgamesToImport
                };

                creatorsToImport.Add(newCreator);
                sb.AppendLine(string.Format(SuccessfullyImportedCreator, newCreator.FirstName, newCreator.LastName, newCreator.Boardgames.Count));
            }

            context.Creators.AddRange(creatorsToImport);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportSellers(BoardgamesContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            ICollection<Seller> sellersToImport = new List<Seller>();

            var sellerDtos = JsonConvert.DeserializeObject<ImportSellerDto[]>(jsonString);

            var validBoardgames = context.Boardgames
                .Select(b => b.Id)
                .ToList();

            foreach (var sellerDto in sellerDtos)
            {
                if (!IsValid(sellerDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Seller newSeller = new Seller
                {
                    Name = sellerDto.Name,
                    Address = sellerDto.Address,
                    Country = sellerDto.Country,
                    Website = sellerDto.Website,
                };

                foreach (var id in sellerDto.BoardgamesIds.Distinct())
                {
                    if (!validBoardgames.Contains(id))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    BoardgameSeller newBoardgameSeller = new BoardgameSeller
                    {
                        BoardgameId = id,
                        Seller = newSeller
                    };

                    newSeller.BoardgamesSellers.Add(newBoardgameSeller);
                }

                sellersToImport.Add(newSeller);
                sb.AppendLine(string.Format(SuccessfullyImportedSeller, newSeller.Name, newSeller.BoardgamesSellers.Count));
            }

            context.Sellers.AddRange(sellersToImport);
            context.SaveChanges();

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
