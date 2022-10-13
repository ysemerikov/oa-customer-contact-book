using System.ComponentModel.DataAnnotations;

namespace Database.Tables;

public class Contact
{
    [Key]
    public long Id { get; set; }

    public string FirstName { get; set; }

    public string? LastName { get; set; }

    public string? PhoneNumber { get; set; }
}
