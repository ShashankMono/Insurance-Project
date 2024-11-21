using Microsoft.EntityFrameworkCore;

namespace Insurance_final_project.Data
{
    public class InsuranceContext:DbContext
    {
        public InsuranceContext(DbContextOptions options):base(options)
        {
            
        }
    }
}
