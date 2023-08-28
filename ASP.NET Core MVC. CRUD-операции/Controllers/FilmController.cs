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

		// GET: Films/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Films/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Film_Name,Film_director,Film_genre,Year_of_issue,poster,Short_description")] Film film, IFormFile uploadedFile)
		{
			if (ModelState.IsValid)
			{
				try
				{
					if (uploadedFile != null)
					{
						string file_path = "/Image/" + uploadedFile.FileName;

						using (var fileStream = new FileStream(_appEnvironment.WebRootPath + file_path, FileMode.Create))
						{
							await uploadedFile.CopyToAsync(fileStream); // копируем файл в поток
						}
						film.PosterPath = "~" + file_path;
					}
					_context.Add(film);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!FilmExists(film.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction("Index");
			}
			return View(film);
		}

		private bool FilmExists(int id)
		{
			return (_context.Films?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}