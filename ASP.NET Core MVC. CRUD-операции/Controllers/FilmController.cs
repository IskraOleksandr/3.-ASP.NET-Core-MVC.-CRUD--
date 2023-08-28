using ASP.NET_Core_MVC._CRUD_операции.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ASP.NET_Core_MVC._CRUD_операции.Controllers
{
	public class FilmController : Controller
	{
		private readonly FilmContext _context;

		private readonly IWebHostEnvironment _appEnvironment;

		public FilmController(FilmContext context, IWebHostEnvironment appEnvironment)
		{
			_context = context;
			_appEnvironment = appEnvironment;
		}

		public async Task<IActionResult> Index()
		{
			return _context.Films != null ?
						View(await _context.Films.ToListAsync()) :
						Problem("Entity set 'FilmContext.Films'  is null.");
		}
	}
}