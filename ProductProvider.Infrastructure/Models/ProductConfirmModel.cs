using System.ComponentModel.DataAnnotations;

namespace ProductProvider.Infrastructure.Models;

public class ProductConfirmModel
{
    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string UserId { get; set; } = null!;

    [Required]
    public string CourseId { get; set; } = null!;

    [Required]
    public string CourseTitle { get; set; } = null!;

    [Required]
    public string AuthorName { get; set; } = null!;

    [Required]
    [RegularExpression(@"^(([^<>()\]\\.,;:\s@\""]+(\.[^<>()\]\\.,;:\s@\""]+)*)|("".+""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", ErrorMessage = "The field Email must match xx@xx.xx")]
    public string Email { get; set; } = null!;
}
