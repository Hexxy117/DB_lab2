namespace Vaistai.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


/// <summary>
/// 'Uzsakymas' in list form.
/// </summary>
public class Uzsakymas
{
	[DisplayName("ID")]
	public int Id { get; set; }

	[DisplayName("Kaina")]
	public double Kaina { get; set; }


	[DisplayName("Būsena")]
	public string Busena { get; set; }

	[DisplayName("Užsakymo data")]
	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
	public DateTime? Data { get; set; }

	[DisplayName("Užsakovas")]
	public string Uzsakovas { get; set; }
}


/// <summary>
/// 'Uzsakymas' in create and edit forms.
/// </summary>
public class UzsakymasCE
{
	/// <summary>
	/// Entity data.
	/// </summary>
	public class UzsakymasM
	{
		[DisplayName("ID")]
		public int Id { get; set; }

		[DisplayName("Data")]
		[DataType(DataType.Date)]
		[Required]
		public DateTime? Data { get; set; }

		[DisplayName("Terminas")]
		[DataType(DataType.DateTime)]
		[Required]
		public DateTime? Terminas { get; set; }

		[DisplayName("Kaina")]
		[Required]
		public double Kaina { get; set; }

		[DisplayName("Uzsakovas")]
		[Required]
		public string FkUzsakovas { get; set; }

		[DisplayName("Vadovas")]
		[Required]
		public string FkVadovas { get; set; }


		[DisplayName("Būsena")]
		[Required]
		public string Busena { get; set; }
	}

	/// <summary>
	/// Representation of 'Vaistas' entity in 'Uzsakymas' edit form.
	/// </summary>
	public class VaistasM
	{
		public int InListId { get; set; }

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

	/// <summary>
	/// Select lists for making drop downs for choosing values of entity fields.
	/// </summary>
	public class ListsM
	{
		public IList<SelectListItem> Busena { get; set; }
		public IList<SelectListItem> Vadovas { get; set; }
		public IList<SelectListItem> Uzsakovas { get; set; }
		public IList<SelectListItem> Vaistas { get; set; }
		public IList<SelectListItem> Talpa { get; set; }
	}


	/// <summary>
	/// Uzsakymas.
	/// </summary>
	public UzsakymasM Uzsakymas { get; set; } = new UzsakymasM();

	/// <summary>
	/// Related 'Vaistas' records.
	/// </summary>
	public IList<VaistasM> Vaistai { get; set; } = new List<VaistasM>();

	/// <summary>
	/// Lists for drop down controls.
	/// </summary>
	public ListsM Lists { get; set; } = new ListsM();
}
