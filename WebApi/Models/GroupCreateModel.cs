using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

/// <summary>
/// Group Create/Update Model.
/// </summary>
public class GroupCreateModel
{
    /// <summary>
    /// Group name.
    /// </summary>
    /// <example>Family</example>
    [Required]
    public string Name { get; set; }
}
