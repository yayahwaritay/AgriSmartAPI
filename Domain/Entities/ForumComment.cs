namespace AgriSmartSierra.Domain.Entities;

public class ForumComment
{
    public Guid Id { get; set; }
    public Guid PostId { get; set; }
    public Guid AuthorId { get; set; }
    public Guid? ParentCommentId { get; set; }
    public string Content { get; set; } = string.Empty;
    public int UpvoteCount { get; set; }
    public bool IsAcceptedAnswer { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public ForumPost Post { get; set; } = null!;
    public User Author { get; set; } = null!;
    public ForumComment? ParentComment { get; set; }
    public ICollection<ForumComment> Replies { get; set; } = new List<ForumComment>();
}