using System.ComponentModel.DataAnnotations;

namespace Database.Tables;

public class Group
{
    [Key]
    public long Id { get; set; }

    public string Name { get; set; }
}
