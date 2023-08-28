using Microsoft.EntityFrameworkCore;
using ASP.NET_Core_MVC._CRUD_операции.Models;

var builder = WebApplication.CreateBuilder(args);

// Получаем строку подключения из файла конфигурации
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

// добавляем контекст ApplicationContext в качестве сервиса в приложениеs_MVC_
builder.Services.AddDbContext<FilmContext>(options => options.UseSqlServer(connection));

// Добавляем сервисы MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseStaticFiles(); // обрабатывает запросы к файлам в папке wwwroot s_MVCs_MVC_ControllerMVC

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Film}/{action=Index}/{id?}");

app.Run();
