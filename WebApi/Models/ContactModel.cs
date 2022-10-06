namespace WebApi.Models;

public class ContactModel
{
    public long Id { get; set; }

    public string FirstName { get; set; }

    public string? LastName { get; set; }

    public string? PhoneNumber { get; set; }
}
