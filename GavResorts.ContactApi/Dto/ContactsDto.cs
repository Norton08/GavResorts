using System.ComponentModel.DataAnnotations;

namespace GavResorts.ContactApi.Dto;

public class ContactsDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The Name is Required")]
    [MinLength(3)]
    [MaxLength(100)]
    public string? Name { get; set; }

    [Required(ErrorMessage = "The Note is Required")]
    [MinLength(3)]
    [MaxLength(256)]
    public string? Note { get; set; }

    [Required(ErrorMessage = "The Phone Number is Required")]
    [MinLength(9)]
    [MaxLength(100)]
    public string? Telephone { get; set; }

    [Required(ErrorMessage = "The Email is Required")]
    [MinLength(15)]
    [MaxLength(100)]
    public string? Email { get; set; }
}
