using Insurance_final_project.Models;
using Microsoft.EntityFrameworkCore;

namespace Insurance_final_project.Data
{
    public class InsuranceContext:DbContext
    {
        public DbSet<Admin> admins {  get; set; }
        public DbSet<Employee> employees { get; set; }
        public DbSet<Agent> agents { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Policy> policies { get; set; }
        public DbSet<City> citys { get; set; }
        public DbSet<State> states { get; set; }
        public DbSet<Claim> claim { get; set; }
        public DbSet<CommissionWithdrawal> commissionsRequest { get; set; }
        public DbSet<Document> documents { get; set; }
        public DbSet<PolicyAccount> policyAccounts { get; set; }
        public DbSet<PolicyCancel> policyCancel { get; set; }
        public DbSet<PolicyInstallment> policyInstallments { get; set; }
        public DbSet<PolicyType> policyTypes { get; set; }
        public DbSet<Query> query { get; set; }
        public DbSet<Role> role { get; set; }
        public DbSet<Transaction> transaction { get; set; }
        public DbSet<User> users { get; set; }

        public InsuranceContext(DbContextOptions options):base(options)
        {
            
        }
    }
}
