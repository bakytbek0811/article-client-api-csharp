using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("articles")]
public class Article
{
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [MaxLength(250)]
    [Required]
    public string Title { get; set; }

    [Column("content")]
    [Required]
    public string Content { get; set; }

    [Column("author_id")]
    [Required]
    public int AuthorId { get; set; }

    [ForeignKey("AuthorId")]
    public Author Author { get; set; }

    [Column("category_id")]
    [Required]
    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    public Category Category { get; set; }

    [Column("published_date")]
    public DateTime PublishedDate { get; set; }
}