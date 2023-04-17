namespace Vaistai.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


/// <summary>
/// Model of 'Paslauga' entity.
/// </summary>
public class Vaistas
{
	[DisplayName("Id")]
	public int Id { get; set; }

	[DisplayName("Formulavimo buferis")]
	public string Buferis { get; set; }

	[DisplayName("Talpa")]
	public string Talpa { get; set; }

	[DisplayName("Procesas")]
	public string Procesas { get; set; }

	[DisplayName("Klinikinė fazė")]
	public string Faze { get; set; }

	[DisplayName("Galiojimo laikas (men.)")]
	public int Galiojimas { get; set; }
}

public class VaistasCE
{

	public class VaistasM
	{
		[DisplayName("Id")]
		public int Id { get; set; }

		[DisplayName("Formulavimo buferis")]
		[Required]
		public string Buferis { get; set; }

		[DisplayName("Dozė")]
		[Required]
		public int Doze { get; set; }

		[DisplayName("Talpa")]
		[Required]
		public string FkTalpa { get; set; }

		[DisplayName("Uzsakymo identifikatorius")]
		public string FkUzsakymas { get; set; }

		[DisplayName("Procesas")]
		[Required]
		public string Procesas { get; set; }

		[DisplayName("Klinikinė fazė")]
		[Required]
		public string Faze { get; set; }

		[DisplayName("Galiojimo laikas (men.)")]
		[Required]
		public int Galiojimas { get; set; }

		[DisplayName("Pagaminimo data")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		public DateTime? Data { get; set; }
	}
	public class ListsM
	{
		public IList<SelectListItem> Talpa { get; set; }
	}
	public VaistasM Vaistas { get; set; } = new VaistasM();
	public ListsM Lists { get; set; } = new ListsM();
}
