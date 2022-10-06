using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

/// <summary>
/// Group Model.
/// </summary>
public class GroupModel
{
    /// <summary>
    /// Group identifier.
    /// </summary>
    /// <example>321</example>
    [Required]
    public long Id { get; set; }

    /// <summary>
    /// Group name.
    /// </summary>
    /// <example>Family</example>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Amount of members in this group.
    /// </summary>
    /// <example>7</example>
    [Required]
    public int MemberCount { get; set; }
}
