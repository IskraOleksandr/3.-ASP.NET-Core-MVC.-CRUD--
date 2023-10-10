using ASP.NET_Core_MVC._CRUD_операции.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_MVC._CRUD_операции.Pages
{
    public class DeleteModel : PageModel
    {
		private readonly FilmContext _context;

		public DeleteModel(FilmContext context)
		{
			_context = context;
		}

		public Film Film_ { get; set; } = default!;

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
			else
			{
				Film_ = film;
			}
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(int? id)
		{
			if (id == null || _context.Films == null)
			{
				return NotFound();
			}
			var film = await _context.Films.FindAsync(id);

			if (film != null)
			{
				Film_ = film;
				_context.Films.Remove(Film_);
				await _context.SaveChangesAsync();
			}

			return RedirectToPage("./Index");
		}
	}
}
