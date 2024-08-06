using System.ComponentModel.DataAnnotations.Schema;

[Table("authors")]
public class Author
{
    [Column("id")]
    public int Id { get; set; }
    
    [Column("first_name")]
    public string FirstName { get; set; }
    
    [Column("last_name")]
    public string LastName { get; set; }
}