
using Insurance_final_project.Data;
using Insurance_final_project.Exceptions;
using Insurance_final_project.Mapper;
using Insurance_final_project.Repositories;
using Insurance_final_project.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

namespace Insurance_final_project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAutoMapper(typeof(MappingProfile));
            
            // Add services to the container.

            builder.Services.AddDbContext<InsuranceContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("connect"));
                options.ConfigureWarnings(warnings =>
                    warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
            });

            builder.Services.AddControllers().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });



            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddTransient<IEmailService,EmailService>();
            builder.Services.AddTransient<IAgentService, AgentService>();
            builder.Services.AddTransient<ICustomerService, CustomerService>();
            builder.Services.AddTransient<IAdminService, AdminService>();
            builder.Services.AddTransient <ICityService,CityService>();
            builder.Services.AddTransient<IClaimService,ClaimService>();
            builder.Services.AddTransient<ICommissionWithdrawalService, ComissionWithdrawalService>();
            builder.Services.AddTransient<ICommissionService, CommissionService>();
            builder.Services.AddTransient<ICustomerService, CustomerService>();
            builder.Services.AddTransient<IDocumentService, DocumentService>();
            builder.Services.AddTransient<IEmployeeService, EmployeeService>();
            builder.Services.AddTransient<INomineeService, NomineeService>();
            builder.Services.AddTransient<IPolicyAccountService, PolicyAccountService>();
            builder.Services.AddTransient<IPolicyAccountDocumentService, PolicyAccountDocumentService>();
            builder.Services.AddTransient<IPolicyCancelService, PolicyCancelService>();
            builder.Services.AddTransient<IPolicyInstallmentService, PolicyInstallmentService>();
            builder.Services.AddTransient<IPolicyService, PolicyService>();
            builder.Services.AddTransient<IPolicyTypeService, PolicyTypeService>();
            builder.Services.AddTransient<IQueryService, QueryService>();
            builder.Services.AddTransient<IRoleService, RoleService>();
            builder.Services.AddTransient<IStateService, StateService>();
            builder.Services.AddTransient<ITransactionService, TransactionService>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddControllers();
            builder.Services.AddControllers().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
            builder.Services.AddTransient<IAdminService, AdminService>();
            builder.Services.AddTransient<IEmployeeService, EmployeeService>();
            builder.Services.AddTransient<ICommonService, CommonService>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddExceptionHandler<AppExceptionHandler>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp", policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .WithExposedHeaders("*");
                });
            });
            //JWT management
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                        .GetBytes(builder.Configuration.GetSection("AppSettings:Key").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseExceptionHandler(_=>{});

            app.UseHttpsRedirection();
            app.UseCors("AllowAngularApp");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
