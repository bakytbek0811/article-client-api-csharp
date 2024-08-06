using System.ComponentModel.DataAnnotations.Schema;

[Table("categories")]
public class Category
{
    [Column("id")]
    public int Id { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
}