using BethanysPieShopHRM.App;
using BethanysPieShopHRM.App.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<IEmployeeDataService, EmployeeDataService>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)).AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
builder.Services.AddHttpClient<ICountryDataService, CountryDataService>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)).AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
builder.Services.AddHttpClient<IJobCategoryDataService, JobCategoryDataService>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)).AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddScoped<ApplicationState>();

builder.Services.AddBlazoredLocalStorage();


builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    // Set mapping for claims fixed issue
    options.UserOptions.NameClaim = "name";
    options.UserOptions.RoleClaim = "role";
    options.UserOptions.ScopeClaim = "scope";
    options.ProviderOptions.ResponseType = "code";
    options.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration["Auth0:Audience"]);
});
/*
 * builder.Services.AddOidcAuthentication(options =>
{
  builder.Configuration.Bind("Oidc", options.ProviderOptions);
 
  // Set mapping for claims fixed issue
  options.UserOptions.NameClaim = "name";
  options.UserOptions.RoleClaim = "role";
  options.UserOptions.ScopeClaim = "scope";
});
 */
await builder.Build().RunAsync();
