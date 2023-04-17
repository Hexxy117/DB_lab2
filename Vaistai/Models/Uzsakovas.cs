namespace Vaistai.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


/// <summary>
/// 'Vadovas' in list form.
/// </summary>
public class Uzsakovas
{
    [DisplayName("Kryptis")]
    [MaxLength(20)]
    [Required]
    public string Kryptis { get; set; }

    [DisplayName("Pavadinimas")]
    [MaxLength(20)]
    [Required]
    public string Pavadinimas { get; set; }

    [DisplayName("Salis")]
    [MaxLength(30)]
    [Required]
    public string Salis { get; set; }
}
