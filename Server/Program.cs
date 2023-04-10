using DTO;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Server.Data;
using Server.Managers.Storages.CabinetMedicalManager;
using Server.Managers.Storages.DoctorManager;
using Server.Managers.Storages.PlanningAppoimentManager;
using Server.Managers.Storages.RolesManager;
using Server.Managers.Storages.SecretaryManager;
using Server.Managers.Storages.SpecialitiesManager;
using Server.Managers.Storages.UserRoleManager;
using Server.Managers.Storages.WorkDoctorManager;
using Server.Managers.UserManager;
using Server.Models.UserAccount;
using Server.Services.Foundation.CabinetMedicalService;
using Server.Services.Foundation.DoctorService;
using Server.Services.Foundation.JwtService;
using Server.Services.Foundation.MailService;
using Server.Services.Foundation.PlanningAppoimentService;
using Server.Services.Foundation.SecretaryService;
using Server.Services.Foundation.WorkDoctorService;
using Server.Services.UserService;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 5;
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;

    options.SignIn.RequireConfirmedEmail = true;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20);
    options.Lockout.MaxFailedAccessAttempts = 3;
});

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromMinutes(10);
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]))


    };
});


builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<ServerDbContext>().AddDefaultTokenProviders();





// Add services to the container.
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<ICabinetMedicalManager, CabinetMedicalManager>();
builder.Services.AddScoped<ICabinetMedicalService, CabinetMedicalService>();
builder.Services.AddScoped<IUserRoleManager, UserRoleManager>();
builder.Services.AddScoped<IDoctorManager, DoctorManager>();
builder.Services.AddScoped<ISpecialitiesManager, SpecialitiesManager>();
builder.Services.AddScoped<IRolesManager, RolesManager>();
builder.Services.AddScoped<IWorkDoctorManager, WorkDoctorManager>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IWorkDoctorService, WorkDoctorService>();
builder.Services.AddScoped<ISecretaryManager, SecretaryManager>();
builder.Services.AddScoped<ISecretaryService, SecretaryService>();
builder.Services.AddScoped<IPlanningAppoimentManager, PlanningAppoimentManager>();
builder.Services.AddScoped<IPlanningAppoimentService, PlanningAppoimentService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextPool<ServerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDataBase"))
);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseRouting();
app.UseCors(policy =>
    policy.WithOrigins("https://localhost:7286", "https://localhost:7286")
    .AllowAnyMethod()
    .WithHeaders(HeaderNames.ContentType)
.AllowAnyHeader()


);

app.UseAuthorization();

app.MapControllers();

app.Run();
