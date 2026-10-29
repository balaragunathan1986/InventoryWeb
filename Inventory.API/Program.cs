using Inventory.Repository.Data;
using Inventory.Repository.Implementation.Account;
using Inventory.Repository.Implementation.Inventory;
using Inventory.Repository.Implementation.Master;
using Inventory.Repository.Implementation.PointofSales;
using Inventory.Repository.Interfaces.Account;
using Inventory.Repository.Interfaces.Inventory;
using Inventory.Repository.Interfaces.Master;
using Inventory.Repository.Interfaces.PointofSales;
using Inventory.Services.Implementation.Account;
using Inventory.Services.Implementation.Inventory;
using Inventory.Services.Implementation.Master;
using Inventory.Services.Implementation.PointofSales;
using Inventory.Services.Interfaces.Account;
using Inventory.Services.Interfaces.Inventory;
using Inventory.Services.Interfaces.Master;
using Inventory.Services.Interfaces.PointofSales;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//  ✅ Logging decleration
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
Log.Information("starting server.");

builder.Host.UseSerilog((context, loggerConfiguration) =>
{
    loggerConfiguration.WriteTo.Console();
    loggerConfiguration.ReadFrom.Configuration(context.Configuration);
});

//  ✅ MSSQL server connection strings
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//  ✅ PostgreSql server connection strings
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection")));


// ✅ Controllers
builder.Services.AddControllers();


// ✅ Use Swashbuckle (SwaggerGen) only
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Inventory Management-api", Version = "v1" });

    // JWT Bearer definition
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter JWT token (Bearer <token>)",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    // Apply globally

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
   {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
   });
});

// ✅ JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
        )
    };
});

// ✅ CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowCORS", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

// ✅ Services and Repositories Middleware
// Add services to the container.
builder.Services.AddTransient<IAccountService,AccountService>();
#region Masters Services
builder.Services.AddTransient<IUomService, UomService>();
builder.Services.AddTransient<IDepartmentService, DepartmentService>();
builder.Services.AddTransient<IDesignationService, DesignationService>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<IPaymentModeService, PaymentModeService>();
builder.Services.AddTransient<IPaymentTermsService, PaymentTermsService>();
builder.Services.AddTransient<ICompanyService, CompanyService>();
builder.Services.AddTransient<IBankDetailsService, BankDetailsService>();
builder.Services.AddTransient<ISalesTypeService, SalesTypeService>();
builder.Services.AddTransient<IProductGroupService, ProductGroupService>();
builder.Services.AddTransient<IChargeService, ChargeService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<ISupplierService, SupplierService>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IProductStockService, ProductStockService>();
builder.Services.AddTransient<ISalesService, SalesService>();
builder.Services.AddTransient<ISalesReturnService, SalesReturnService>();
builder.Services.AddTransient<IReceiptService, ReceiptService>();
builder.Services.AddTransient<ICourierService, CourierService>();
#endregion

// Add Repository to the container.
builder.Services.AddTransient<IAccountRepository, AccountRepository>();
#region Masters Repository
builder.Services.AddTransient<IUomRepository, UomRepository>();
builder.Services.AddTransient<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddTransient<IDesignationRepository, DesignationRepository>();
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddTransient<IPaymentModeRepository, PaymentModeRepository>();
builder.Services.AddTransient<IPaymentTermsRepository, PaymentTermsRepository>();
builder.Services.AddTransient<ICompanyRepository, CompanyRepository>();
builder.Services.AddTransient<IBankDetailsRepository, BankDetailsRepository>();
builder.Services.AddTransient<ISalesTypeRepository, SalesTypeRepository>();
builder.Services.AddTransient<IProductGroupRepository, ProductGroupRepository>();
builder.Services.AddTransient<IChargeRepository, ChargeRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<ISupplierRepository, SupplierRepository>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IProductStockRepository, ProductStockRepository>();
builder.Services.AddTransient<ISalesRepository, SalesRepository>();
builder.Services.AddTransient<ISalesReturnRepository, SalesReturnRepository>();
builder.Services.AddTransient<IReceiptRepository, ReceiptRepository>();
builder.Services.AddTransient<ICourierRepository, CourierRepository>();
#endregion 

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//  ✅ Swagger UI
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//  ✅ Important: enable authentication before authorization
app.UseCors("AllowCORS");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
