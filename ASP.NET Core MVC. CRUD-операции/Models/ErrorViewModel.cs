namespace ASP.NET_Core_MVC._CRUD_операции.Models
{
	public class Film 
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Director { get; set; }
		public string Genre { get; set; }
		public int Year { get; set; }
		public string? PosterPath { get; set; }
		public string Description { get; set; }
	}
}