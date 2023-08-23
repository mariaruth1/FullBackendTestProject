namespace infrastructure.DataModels;

public class Book
{
    public required string Title { get; set; }
    public int BookId { get; set; }
    public required string CoverImgUrl { get; set; }
    public required string Publisher { get; set; }
}