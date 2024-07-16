namespace Cadastre.Common
{
    public static class ValidationConstants
    {
        //District
        public const int DistrictNameMinLength = 2;
        public const int DistrictNameMaxLength = 80;

        public const string DistrictPostalCodeRegex = @"^[A-Z]{2}-\d{5}$";

        //Property
        public const int PropertyIdentifierMinLength = 16;
        public const int PropertyIdentifierMaxLength = 20;
        public const int PropertyDetailsMinLength = 5;
        public const int PropertyDetailsMaxLength = 500;
        public const int PropertyAddressMinLength = 5;
        public const int PropertyAddressMaxLength = 200;

        //Citizen
        public const int CitizenNameMinLength = 2;
        public const int CitizenNameMaxLength = 30;
    }
}
