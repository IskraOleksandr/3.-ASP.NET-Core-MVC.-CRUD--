using Microsoft.EntityFrameworkCore;
using ASP.NET_Core_MVC._CRUD_��������.Models;

var builder = WebApplication.CreateBuilder(args);

// �������� ������ ����������� �� ����� ������������
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

// ��������� �������� ApplicationContext � �������� ������� � ����������s_MVC_
builder.Services.AddDbContext<FilmContext>(options => options.UseSqlServer(connection));

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();
app.UseStaticFiles(); // ������������ ������� � ������ � ����� wwwroot s_MVCs_MVC_ControllerMVC

app.MapRazorPages();

app.Run();
