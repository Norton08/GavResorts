using System.ComponentModel.DataAnnotations;

namespace GavResorts.Web.Models;

public class ContactViewModel
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Note { get; set; }
    [Required]
    [Display(Name = "Phone Number")]
    public string? Telephone { get; set; }
    [Required]
    public string? Email { get; set; }
}