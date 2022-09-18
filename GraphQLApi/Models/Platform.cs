using System.ComponentModel.DataAnnotations;
using GraphQLApi.Models;

namespace GraphQLApi.Models;

public class Platform{
    [Key]
    public int Id {get; set;} 
    [Required]
    public string  Name { get; set; } = null!;
    public string LicenseKey { get; set; } = null!;
    public ICollection<Command> Commands {get; set;} = new List<Command>();
}