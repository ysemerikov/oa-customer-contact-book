using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

/// <summary>
/// Contact Model.
/// </summary>
public class ContactModel
{
    /// <summary>
    /// Contact identifier.
    /// </summary>
    /// <example>123</example>
    [Required]
    public long Id { get; set; }

    /// <summary>
    /// Contact first mame.
    /// </summary>
    /// <example>Will</example>
    [Required]
    public string FirstName { get; set; }

    /// <summary>
    /// Contact last mame.
    /// </summary>
    /// <example>Smith</example>
    public string? LastName { get; set; }

    /// <summary>
    /// Contact phone number.
    /// </summary>
    /// <example>+10987654321</example>
    public string? PhoneNumber { get; set; }
}
