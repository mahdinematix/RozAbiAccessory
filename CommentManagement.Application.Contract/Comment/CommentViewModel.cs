namespace CommentManagement.Application.Contract.Comment;

public class CommentViewModel : AddComment
{
    public string CreationDate { get; set; }
    public long Id { get; set; }
    public bool IsCanceled { get; set; }
    public bool IsConfirmed { get; set; }
}