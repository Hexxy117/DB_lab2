namespace Vaistai.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


/// <summary>
/// 'Uzsakovas' in list form.
/// </summary>
public class Vadovas
{
	[DisplayName("Vardas")]
	[MaxLength(20)]
	[Required]
	public string Vardas { get; set; }

	[DisplayName("Pavardė")]
	[MaxLength(20)]
	[Required]
	public string Pavarde { get; set; }

	[DisplayName("El. Paštas")]
	[MaxLength(30)]
	[Required]
	public string Pastas { get; set; }
	[DisplayName("Tel. Numeris")]
	[MaxLength(20)]
	[Required]
	public string TelNumeris { get; set; }

	[DisplayName("ID")]
	public int Id { get; set; }
}

