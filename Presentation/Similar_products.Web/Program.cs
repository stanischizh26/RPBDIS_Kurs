using AutoMapper;
using Similar_products.Application;
using Similar_products.Web.Extensions;
using Similar_products.Application.Requests.Queries;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// ��������� ��������� ������������ (���� ��� �����)
builder.Services.AddControllers();

// ������������� CORS
builder.Services.ConfigureCors();

// ������������� �������� ���� ������
builder.Services.ConfigureDbContext(builder.Configuration);

// ������������ �������������� �������
builder.Services.ConfigureServices();

// ������������ ��� AutoMapper
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper autoMapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(autoMapper);

// ��������� MediatR ��� ��������� �������� � ������
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetProductsQuery).Assembly));

// ���������� Swagger ��� ������������
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ��������� Razor Pages
builder.Services.AddRazorPages().AddRazorOptions(options =>
{
    options.PageViewLocationFormats.Add("/Pages/Shared/{0}.cshtml");
});
builder.Services.AddScoped<DatabaseSeeder>();  // ���� ����� ������������ ��� ������� ������

var app = builder.Build();

// ������������ ����������� ������ �� ����� wwwroot
app.UseStaticFiles();  // ��� ������ ���������� wwwroot

// �������� middleware ��� ���������� ���� ������
await app.UseDatabaseSeeder();  // ���� ���������� ��� ������������� ������

// ������������ HTTP pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ��������� �������� CORS
app.UseCors("CorsPolicy");

// �������� �������������
app.UseRouting();

// ���������� ����������� (���� ��� ����)
app.MapControllers();

// ���������� Razor Pages
app.MapRazorPages();

app.MapGet("/", () => Results.Redirect("/Home/Index"));
app.MapFallbackToPage("/Home/Index"); // ��������� �� �������� Home.cshtml ��� ���������

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

// ������ ����������
app.Run();