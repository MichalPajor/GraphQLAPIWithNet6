using System.ComponentModel.DataAnnotations;

namespace GraphQL.Models;

public class Platform{
    [Key]
    public int Id {get; set;} 
    [Required]
    public string  Name { get; set; } = null!;
    public string LicenseKey { get; set; } = null!;

}