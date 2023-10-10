using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ASP.NET_Core_MVC._CRUD_операции.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_MVC._CRUD_операции.Pages
{
    public class IndexModel : PageModel
    {
		private readonly FilmContext _context;

		public IndexModel(FilmContext context)
		{
			_context = context;
		}

		public IList<Film> Film_ { get; set; } = default!;

		public async Task OnGetAsync()
		{
			if (_context.Films != null)
			{
				Film_ = await _context.Films.ToListAsync();
			}
		}
	}
}
