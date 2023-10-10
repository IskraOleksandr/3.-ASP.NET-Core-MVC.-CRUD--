using ASP.NET_Core_MVC._CRUD_операции.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_MVC._CRUD_операции.Pages
{
    public class EditModel : PageModel
    {
		private readonly FilmContext _context;
		IWebHostEnvironment _appEnvironment;

		public EditModel(FilmContext context, IWebHostEnvironment appEnvironment)
		{
			_context = context;
			_appEnvironment = appEnvironment;
		}

		[BindProperty]
		public Film Film_ { get; set; } = default!;
		public IFormFile uploadedFile { get; set; } = default!;

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null || _context.Films == null)
			{
				return NotFound();
			}

			var film = await _context.Films.FirstOrDefaultAsync(m => m.Id == id);
			if (film == null)
			{
				return NotFound();
			}
			Film_ = film;
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid || uploadedFile == null)
			{
				return Page();
			} 

			string file_path = "/Image/" + uploadedFile.FileName; // имя файла

			using (var fileStream = new FileStream(_appEnvironment.WebRootPath + file_path, FileMode.Create))
			{
				await uploadedFile.CopyToAsync(fileStream); // копируем файл в поток
			}
			Film_.PosterPath = "~" + file_path;

			_context.Attach(Film_).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!FilmExists(Film_.Id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return RedirectToPage("./Index");
		}

		private bool FilmExists(int id)
		{
			return (_context.Films?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
