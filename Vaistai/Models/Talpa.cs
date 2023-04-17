namespace Vaistai.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


/// <summary>
/// 'Uzsakovas' in list form.
/// </summary>
public class Talpa
{
	[DisplayName("Rūšis")]
	[MaxLength(20)]
	[Required]
	public string Rusis { get; set; }

	[DisplayName("Tūris")]
	[MaxLength(20)]
	[Required]
	public string Turis { get; set; }

	[DisplayName("ID")]
	public int Id { get; set; }
}

