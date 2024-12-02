﻿// <auto-generated />
using System;
using Insurance_final_project.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Insurance_final_project.Migrations
{
    [DbContext(typeof(InsuranceContext))]
    partial class InsuranceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Insurance_final_project.Models.Admin", b =>
                {
                    b.Property<Guid>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AdminId");

                    b.HasIndex("UserId");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("Insurance_final_project.Models.Agent", b =>
                {
                    b.Property<Guid>("AgentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<double>("CommissionEarned")
                        .HasColumnType("float");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Qualification")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TotalCommission")
                        .HasColumnType("float");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AgentId");

                    b.HasIndex("UserId");

                    b.ToTable("Agents");
                });

            modelBuilder.Entity("Insurance_final_project.Models.City", b =>
                {
                    b.Property<Guid>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StateId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CityId");

                    b.HasIndex("StateId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Insurance_final_project.Models.Claim", b =>
                {
                    b.Property<Guid>("ClaimId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<DateTime?>("AcknowledgementDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("AmountToBeClaimed")
                        .HasColumnType("float");

                    b.Property<string>("ApprovedStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateAndTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DocumentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PolicyAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ClaimId");

                    b.HasIndex("DocumentId");

                    b.HasIndex("PolicyAccountId");

                    b.ToTable("Claim");
                });

            modelBuilder.Entity("Insurance_final_project.Models.Commission", b =>
                {
                    b.Property<Guid>("CommissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AgentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("CommissionType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("CommissionId");

                    b.HasIndex("AgentId");

                    b.ToTable("Commission");
                });

            modelBuilder.Entity("Insurance_final_project.Models.CommissionWithdrawal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<Guid>("AgentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("ApprovedStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("TransactionStatus")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AgentId");

                    b.ToTable("CommissionRequests");
                });

            modelBuilder.Entity("Insurance_final_project.Models.Customer", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<Guid>("CityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmailId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IsApproved")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nominee")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomineeRelation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CustomerId");

                    b.HasIndex("CityId");

                    b.HasIndex("StateId");

                    b.HasIndex("UserId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Insurance_final_project.Models.Document", b =>
                {
                    b.Property<Guid>("DocumentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DocumentFileURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DocumentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DocumentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IsVerified")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DocumentId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("Insurance_final_project.Models.Employee", b =>
                {
                    b.Property<Guid>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Salary")
                        .HasColumnType("float");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EmployeeId");

                    b.HasIndex("UserId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Insurance_final_project.Models.Policy", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<double>("CommissionPercentage")
                        .HasColumnType("float");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaximumAgeCriteria")
                        .HasColumnType("int");

                    b.Property<double>("MaximumInvestmentAmount")
                        .HasColumnType("float");

                    b.Property<int>("MaximumPolicyTerm")
                        .HasColumnType("int");

                    b.Property<int>("MinimumAgeCriteria")
                        .HasColumnType("int");

                    b.Property<double>("MinimumInvestmentAmount")
                        .HasColumnType("float");

                    b.Property<int>("MinimumPolicyTerm")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PolicyTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("ProfitPercentage")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("PolicyTypeId");

                    b.ToTable("Policies");
                });

            modelBuilder.Entity("Insurance_final_project.Models.PolicyAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<double?>("AgentCommission")
                        .HasColumnType("float");

                    b.Property<Guid?>("AgentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("CoverageAmount")
                        .HasColumnType("float");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InstallmentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PolicyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TotalAmountPaid")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("AgentId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PolicyId");

                    b.ToTable("PolicyAccounts");
                });

            modelBuilder.Entity("Insurance_final_project.Models.PolicyCancel", b =>
                {
                    b.Property<Guid>("PolicyCancelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("DateAndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("IsApproved")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PolicyAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PolicyCancelId");

                    b.HasIndex("PolicyAccountId");

                    b.ToTable("PolicyCancel");
                });

            modelBuilder.Entity("Insurance_final_project.Models.PolicyInstallment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("InstallmentDueDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("InstallmentPaidDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<Guid>("PolicyAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PolicyAccountId");

                    b.HasIndex("TransactionId")
                        .IsUnique();

                    b.ToTable("PolicyInstallments");
                });

            modelBuilder.Entity("Insurance_final_project.Models.PolicyType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PolicyTypes");
                });

            modelBuilder.Entity("Insurance_final_project.Models.Query", b =>
                {
                    b.Property<Guid>("QueryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Response")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("QueryId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Query");
                });

            modelBuilder.Entity("Insurance_final_project.Models.Role", b =>
                {
                    b.Property<Guid>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Insurance_final_project.Models.State", b =>
                {
                    b.Property<Guid>("StateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("StateName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StateId");

                    b.ToTable("States");
                });

            modelBuilder.Entity("Insurance_final_project.Models.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PolicyAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PolicyInstallmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ReferenceNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PolicyAccountId");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("Insurance_final_project.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Insurance_final_project.Models.Admin", b =>
                {
                    b.HasOne("Insurance_final_project.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Insurance_final_project.Models.Agent", b =>
                {
                    b.HasOne("Insurance_final_project.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Insurance_final_project.Models.City", b =>
                {
                    b.HasOne("Insurance_final_project.Models.State", "State")
                        .WithMany("Cities")
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("State");
                });

            modelBuilder.Entity("Insurance_final_project.Models.Claim", b =>
                {
                    b.HasOne("Insurance_final_project.Models.Document", "Document")
                        .WithMany()
                        .HasForeignKey("DocumentId");

                    b.HasOne("Insurance_final_project.Models.PolicyAccount", "PolicyAccount")
                        .WithMany()
                        .HasForeignKey("PolicyAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");

                    b.Navigation("PolicyAccount");
                });

            modelBuilder.Entity("Insurance_final_project.Models.Commission", b =>
                {
                    b.HasOne("Insurance_final_project.Models.Agent", null)
                        .WithMany("Commissions")
                        .HasForeignKey("AgentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Insurance_final_project.Models.CommissionWithdrawal", b =>
                {
                    b.HasOne("Insurance_final_project.Models.Agent", "Agent")
                        .WithMany("CommissionWithdrawal")
                        .HasForeignKey("AgentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Agent");
                });

            modelBuilder.Entity("Insurance_final_project.Models.Customer", b =>
                {
                    b.HasOne("Insurance_final_project.Models.City", "City")
                        .WithMany("Customers")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Insurance_final_project.Models.State", "State")
                        .WithMany("Customer")
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Insurance_final_project.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("State");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Insurance_final_project.Models.Document", b =>
                {
                    b.HasOne("Insurance_final_project.Models.Customer", "Customer")
                        .WithMany("Documents")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Insurance_final_project.Models.Employee", b =>
                {
                    b.HasOne("Insurance_final_project.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Insurance_final_project.Models.Policy", b =>
                {
                    b.HasOne("Insurance_final_project.Models.PolicyType", "PolicyType")
                        .WithMany("Policies")
                        .HasForeignKey("PolicyTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PolicyType");
                });

            modelBuilder.Entity("Insurance_final_project.Models.PolicyAccount", b =>
                {
                    b.HasOne("Insurance_final_project.Models.Agent", "Agent")
                        .WithMany("PolicyAccounts")
                        .HasForeignKey("AgentId");

                    b.HasOne("Insurance_final_project.Models.Customer", "Customer")
                        .WithMany("PolicyAccounts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Insurance_final_project.Models.Policy", "Policy")
                        .WithMany("PolicyAccounts")
                        .HasForeignKey("PolicyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Agent");

                    b.Navigation("Customer");

                    b.Navigation("Policy");
                });

            modelBuilder.Entity("Insurance_final_project.Models.PolicyCancel", b =>
                {
                    b.HasOne("Insurance_final_project.Models.PolicyAccount", "PolicyAccount")
                        .WithMany()
                        .HasForeignKey("PolicyAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PolicyAccount");
                });

            modelBuilder.Entity("Insurance_final_project.Models.PolicyInstallment", b =>
                {
                    b.HasOne("Insurance_final_project.Models.PolicyAccount", "PolicyAccount")
                        .WithMany("policyInstallments")
                        .HasForeignKey("PolicyAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Insurance_final_project.Models.Transaction", "Transaction")
                        .WithOne("policyInstallement")
                        .HasForeignKey("Insurance_final_project.Models.PolicyInstallment", "TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PolicyAccount");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("Insurance_final_project.Models.Query", b =>
                {
                    b.HasOne("Insurance_final_project.Models.Customer", "Customer")
                        .WithMany("Queries")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Insurance_final_project.Models.Transaction", b =>
                {
                    b.HasOne("Insurance_final_project.Models.Customer", "Customer")
                        .WithMany("Transactions")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Insurance_final_project.Models.PolicyAccount", "PolicyAccount")
                        .WithMany("transactions")
                        .HasForeignKey("PolicyAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("PolicyAccount");
                });

            modelBuilder.Entity("Insurance_final_project.Models.User", b =>
                {
                    b.HasOne("Insurance_final_project.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Insurance_final_project.Models.Agent", b =>
                {
                    b.Navigation("CommissionWithdrawal");

                    b.Navigation("Commissions");

                    b.Navigation("PolicyAccounts");
                });

            modelBuilder.Entity("Insurance_final_project.Models.City", b =>
                {
                    b.Navigation("Customers");
                });

            modelBuilder.Entity("Insurance_final_project.Models.Customer", b =>
                {
                    b.Navigation("Documents");

                    b.Navigation("PolicyAccounts");

                    b.Navigation("Queries");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Insurance_final_project.Models.Policy", b =>
                {
                    b.Navigation("PolicyAccounts");
                });

            modelBuilder.Entity("Insurance_final_project.Models.PolicyAccount", b =>
                {
                    b.Navigation("policyInstallments");

                    b.Navigation("transactions");
                });

            modelBuilder.Entity("Insurance_final_project.Models.PolicyType", b =>
                {
                    b.Navigation("Policies");
                });

            modelBuilder.Entity("Insurance_final_project.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Insurance_final_project.Models.State", b =>
                {
                    b.Navigation("Cities");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Insurance_final_project.Models.Transaction", b =>
                {
                    b.Navigation("policyInstallement")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
