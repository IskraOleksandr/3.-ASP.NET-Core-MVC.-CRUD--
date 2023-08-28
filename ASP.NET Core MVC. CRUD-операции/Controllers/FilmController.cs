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
		public async Task<IActionResult> Create([Bind("Id,Title,Director,Genre,Year,PosterPath,Description")] Film film, IFormFile uploadedFile)
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

		//GET: Students/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Films == null)
			{
				return NotFound();
			}

			var film = await _context.Films.FindAsync(id);
			if (film == null)
			{
				return NotFound();
			}
			return View(film);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Director,Genre,Year,PosterPath,Description")] Film film, IFormFile uploadedFile)
		{
			if (id != film.Id)
				return NotFound();

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
				_context.Update(film);
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

		private bool FilmExists(int id)
		{
			return (_context.Films?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}