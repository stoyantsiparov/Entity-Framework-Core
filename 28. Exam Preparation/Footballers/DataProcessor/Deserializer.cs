using System.Globalization;
using System.Text;
using Footballers.Data.Models;
using Footballers.Data.Models.Enums;
using Footballers.DataProcessor.ImportDto;
using Footballers.Utilities;
using Newtonsoft.Json;

namespace Footballers.DataProcessor
{
    using Footballers.Data;
    using System.ComponentModel.DataAnnotations;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCoach
            = "Successfully imported coach - {0} with {1} footballers.";

        private const string SuccessfullyImportedTeam
            = "Successfully imported team - {0} with {1} footballers.";

        public static string ImportCoaches(FootballersContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRoot = "Coaches";

            ICollection<Coach> coachesToImport = new List<Coach>();

            var coachesDtos = xmlHelper.Deserialize<ImportCoachDto[]>(xmlString, xmlRoot);
            foreach (var coachDto in coachesDtos)
            {
                if (!IsValid(coachDto) || string.IsNullOrEmpty(coachDto.Nationality))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                ICollection<Footballer> footballersToImport = new List<Footballer>();
                foreach (var footballerDto in coachDto.Footballers)
                {
                    if (!IsValid(footballerDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    bool isStartDateValid = DateTime
                        .TryParseExact(footballerDto.ContractStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                            out DateTime startDateTime);

                    bool isEndDateValid = DateTime
                        .TryParseExact(footballerDto.ContractEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                            out DateTime endDateTime);

                    if (isStartDateValid == false || isEndDateValid == false || startDateTime >= endDateTime)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Footballer newFootballer = new Footballer
                    {
                        Name = footballerDto.Name,
                        ContractStartDate = startDateTime,
                        ContractEndDate = endDateTime,
                        BestSkillType = (BestSkillType)footballerDto.BestSkillType,
                        PositionType = (PositionType)footballerDto.PositionType,
                    };

                    footballersToImport.Add(newFootballer);
                }

                Coach newCoach = new Coach
                {
                    Name = coachDto.Name,
                    Nationality = coachDto.Nationality,
                    Footballers = footballersToImport
                };

                coachesToImport.Add(newCoach);
                sb.AppendLine(string.Format(SuccessfullyImportedCoach, newCoach.Name, newCoach.Footballers.Count));
            }

            context.Coaches.AddRange(coachesToImport);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportTeams(FootballersContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            ICollection<Team> teamsToImport = new List<Team>();

            var teamDtos = JsonConvert.DeserializeObject<ImportTeamDto[]>(jsonString);

            var validFootballerIds = context.Footballers
                .Select(f => f.Id)
                .ToList();

            foreach (var teamDto in teamDtos)
            {
                if (!IsValid(teamDto) || teamDto.Trophies == 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Team newTeam = new Team
                {
                    Name = teamDto.Name,
                    Nationality = teamDto.Nationality,
                    Trophies = teamDto.Trophies
                };

                foreach (var id in teamDto.FootballersIds.Distinct())
                {
                    if (!validFootballerIds.Contains(id))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    TeamFootballer newTeamFootballer = new TeamFootballer
                    {
                        FootballerId = id,
                        Team = newTeam
                    };

                    newTeam.TeamsFootballers.Add(newTeamFootballer);
                }

                if (!IsValid(newTeam))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                teamsToImport.Add(newTeam);
                sb.AppendLine(string.Format(SuccessfullyImportedTeam, newTeam.Name, newTeam.TeamsFootballers.Count));
            }

            context.Teams.AddRange(teamsToImport);
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
