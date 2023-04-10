using Blazored.LocalStorage;
using Client;
using Client.Services.Foundations.AuthentificationStatService;
using Client.Services.Foundations.CabinetMedicalService;
using Client.Services.Foundations.DoctorService;
using Client.Services.Foundations.LocalStorageService;
using Client.Services.Foundations.LoginService;
using Client.Services.Foundations.MedicalPlanningService;
using Client.Services.Foundations.SecretaryService;
using Client.Services.Foundations.SignInService;
using Client.Services.Foundations.UserService;
using Client.Services.Foundations.WorkDoctorService;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;



var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");


builder.RootComponents.Add<HeadOutlet>("head::after");





builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7104/") });
builder.Services.AddScoped<ISignInService, SignInService>();
builder.Services.AddScoped<ILocalStorageServices, LocalStorageServices>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<AuthentificationStatService>();
builder.Services.AddScoped<ICabinetMedicalService, CabinetMedicalService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IWorkDoctorService, WorkDoctorService>();
builder.Services.AddScoped<ISercretaryService, SercretaryService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMedicalPlanningService, MedicalPlanningService>();
builder.Services.AddScoped<AuthenticationStateProvider>((provider => provider.GetRequiredService<AuthentificationStatService>()));
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
await builder.Build().RunAsync();
