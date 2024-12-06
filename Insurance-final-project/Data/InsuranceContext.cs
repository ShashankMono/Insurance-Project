using Insurance_final_project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;

namespace Insurance_final_project.Data
{
    public class InsuranceContext:DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Claim> Claim { get; set; }
        public DbSet<CommissionWithdrawal> CommissionRequests { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<PolicyAccount> PolicyAccounts { get; set; }
        public DbSet<PolicyCancel> PolicyCancel { get; set; }
        public DbSet<PolicyInstallment> PolicyInstallments { get; set; }
        public DbSet<PolicyType> PolicyTypes { get; set; }
        public DbSet<Query> Query { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PolicyAccountDocument> Policydocuments { get; set; }
        public InsuranceContext(DbContextOptions<InsuranceContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Policy>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Id)
                    .HasDefaultValueSql("NEWSEQUENTIALID()");
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.AdminId);

                entity.Property(e => e.AdminId)
                    .HasDefaultValueSql("NEWSEQUENTIALID()");
            });

            modelBuilder.Entity<Agent>(entity =>
            {
                entity.HasKey(e => e.AgentId);

                entity.Property(e => e.AgentId)
                    .HasDefaultValueSql("NEWSEQUENTIALID()");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.CityId);

                entity.Property(e => e.CityId)
                    .HasDefaultValueSql("NEWSEQUENTIALID()");
            });

            modelBuilder.Entity<Claim>(entity =>
            {
                entity.HasKey(e => e.ClaimId);

                entity.Property(e => e.ClaimId)
                    .HasDefaultValueSql("NEWSEQUENTIALID()");
            });

            modelBuilder.Entity<CommissionWithdrawal>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("NEWSEQUENTIALID()");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.Property(e => e.CustomerId)
                    .HasDefaultValueSql("NEWSEQUENTIALID()");
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.HasKey(e => e.DocumentId);

                entity.Property(e => e.DocumentId)
                    .HasDefaultValueSql("NEWSEQUENTIALID()");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.Property(e => e.EmployeeId)
                    .HasDefaultValueSql("NEWSEQUENTIALID()");
            });

            modelBuilder.Entity<PolicyAccount>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("NEWSEQUENTIALID()");
            });

            modelBuilder.Entity<PolicyCancel>(entity =>
            {
                entity.HasKey(e => e.PolicyCancelId);

                entity.Property(e => e.PolicyCancelId)
                    .HasDefaultValueSql("NEWSEQUENTIALID()");
            });

            modelBuilder.Entity<PolicyInstallment>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("NEWSEQUENTIALID()");
            });

            modelBuilder.Entity<PolicyType>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("NEWSEQUENTIALID()");
            });

            modelBuilder.Entity<Query>(entity =>
            {
                entity.HasKey(e => e.QueryId);

                entity.Property(e => e.QueryId)
                    .HasDefaultValueSql("NEWSEQUENTIALID()");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.Property(e => e.RoleId)
                    .HasDefaultValueSql("NEWSEQUENTIALID()");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => e.StateId);

                entity.Property(e => e.StateId)
                    .HasDefaultValueSql("NEWSEQUENTIALID()");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("NEWSEQUENTIALID()");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId)
                    .HasDefaultValueSql("NEWSEQUENTIALID()");
            });

            modelBuilder.Entity<PolicyAccountDocument>(entity =>
            {
                entity.HasKey(e => e.DocumentId);

                entity.Property(e => e.DocumentId)
                    .HasDefaultValueSql("NEWSEQUENTIALID()");
            });
        }
    }

}
