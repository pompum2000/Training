using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Training.Models;
using Training.Hubs;
using Training.Services;
using Training.Interfaces;
using Microsoft.AspNetCore.Http.Json;
using Training;


static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new();
    builder.EntitySet<SelectAll_Result>("SelectAll");
    builder.EntitySet<Department>("Departments");
    builder.EntitySet<NhanVien>("NhanViens");
    builder.EntitySet<SelectAllid>("SelectAllid");
    return builder.GetEdmModel();
}
log4net.Config.XmlConfigurator.Configure();
IHost host = Host.CreateDefaultBuilder(args)
        .ConfigureServices(services =>
        {
            services.AddHostedService<Worker>();
            
        })
        .UseWindowsService()
        .Build();


var builder = WebApplication.CreateBuilder(args);

    builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    ApplicationName = typeof(Program).Assembly.FullName,
    ContentRootPath = Directory.GetCurrentDirectory(),
    EnvironmentName = Environments.Development,
    WebRootPath = "wwwroot"
});

Console.WriteLine($"Application Name: {builder.Environment.ApplicationName}");
Console.WriteLine($"Environment Name: {builder.Environment.EnvironmentName}");
Console.WriteLine($"ContentRoot Path: {builder.Environment.ContentRootPath}");
Console.WriteLine($"WebRootPath: {builder.Environment.WebRootPath}");

// Add services to the container.
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNameCaseInsensitive = false;
    options.SerializerOptions.PropertyNamingPolicy = null;
    options.SerializerOptions.WriteIndented = true;
});
builder.Services.AddScoped<INhanVien,NhanVienSvc>();
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Services.AddDbContext<DataContext>();
builder.Services.AddMvc(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;

});
builder.Services.AddControllers().AddOData(opt => opt.AddRouteComponents("odata", GetEdmModel()).Filter().Select().Expand());


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapHub<SignalrServer>("/signalrServer");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
host.RunAsync();
app.Run();
