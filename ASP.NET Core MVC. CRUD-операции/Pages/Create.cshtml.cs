using ASP.NET_Core_MVC._CRUD_операции.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP.NET_Core_MVC._CRUD_операции.Pages
{
	public class CreateModel : PageModel
	{
		private readonly FilmContext _context;
		IWebHostEnvironment _appEnvironment;

		public CreateModel(FilmContext context, IWebHostEnvironment appEnvironment)
		{
			_context = context;
			_appEnvironment = appEnvironment;
		}

		public IActionResult OnGet()
		{
			return Page();
		}

		[BindProperty]
		public Film Film_ { get; set; } = default!;
		public IFormFile uploadedFile { get; set; } = default!;

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid || _context.Films == null || Film_ == null || uploadedFile == null)
			{
				return Page();
			}

			string file_path = "/Image/" + uploadedFile.FileName; // имя файла

			using (var fileStream = new FileStream(_appEnvironment.WebRootPath + file_path, FileMode.Create))
			{
				await uploadedFile.CopyToAsync(fileStream); // копируем файл в поток
			}
			Film_.PosterPath = "~" + file_path;

			_context.Films.Add(Film_);
			await _context.SaveChangesAsync();

			return RedirectToPage("./Index");
		}
	}
}
