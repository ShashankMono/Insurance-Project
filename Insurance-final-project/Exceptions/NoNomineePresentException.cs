using Microsoft.IdentityModel.Tokens;

namespace Insurance_final_project.Exceptions
{
    public class NoNomineePresentException:Exception
    {
        public NoNomineePresentException(string message):base(message)
        {
            
        }
    }
}
