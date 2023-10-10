using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_MVC._CRUD_операции.Models
{
	public class Film 
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Поле названия фильма должно быть установлено.")]
		[StringLength(50, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 50 символов")]
		[Display(Name = "Название фильма: ")]
		public string? Title { get; set; }

		[Required(ErrorMessage = "Поле режиссера должно быть установлено.")]
		[StringLength(80, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 50 символов")]
		[Display(Name = "Режиссер: ")]
		public string? Director { get; set; }

		[Required(ErrorMessage = "Поле жанра должно быть установлено.")]
		[StringLength(80, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 3 до 70 символов")]
		[Display(Name = "Жанр:")]
		public string? Genre { get; set; }

		[Required(ErrorMessage = "Поле года выпуска должно быть установлено.")]
		[Display(Name = "Год выпуска: ")]
		[Range(1900, 2023)]
		public int? Year { get; set; }

        [Display(Name = "Постер:")]
        public string? PosterPath { get; set; }

		[Required(ErrorMessage = "Поле должно быть установлено.")]
		[StringLength(1000, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 400 символов")]
		[Display(Name = "Описание фильма: ")]
		public string? Description { get; set; }
	}
}