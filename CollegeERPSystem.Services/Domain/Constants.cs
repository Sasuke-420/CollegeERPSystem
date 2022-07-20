using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CollegeERP.Tests")]
namespace CollegeERPSystem.Services.Domain
{
    internal class Constants
    {
        public const int NameSize = 20;
        public const int AddressSize = 100;
        public const int CodeSize = 6;
        public const string LengthError = "{0} cannot be more than {1}";
        public const string RequiredError = "{0} is required";
        public const string ConnString = "Server=localhost;Port=5432;Database=CollegeERP;User Id=ERPAdmin;Password=ERPAdmin;";
    }
}
