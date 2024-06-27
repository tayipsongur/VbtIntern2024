using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VbtIntern.Entities.Models;

[Table("User")]
public partial class User
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [StringLength(50)]
    public string? Surname { get; set; }

    public int? Age { get; set; }

    [StringLength(100)]
    public string? UserName { get; set; }

    public string? Password { get; set; }

    public bool? IsDeleted { get; set; }
}
