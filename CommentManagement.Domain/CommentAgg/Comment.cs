using _01_Framework.Domain;

namespace CommentManagement.Domain.CommentAgg
{
    public class Comment : EntityBase
    {
        public string Name { get; private set; }
        public string Message { get; private set; }
        public string Email { get; private set; }
        public bool IsConfirm { get; private set; }
        public bool IsCancel { get; private set; }
        public int Type { get; private set; }
        public long OwnerRecordId { get; private set; }

        public Comment(string name, string message, string email, int type, long ownerRecordId)
        {
            Name = name;
            Message = message;
            Email = email;
            Type = type;
            OwnerRecordId = ownerRecordId;
        }


        public void Confirm()
        {
            IsConfirm = true;
            IsCancel =false;
        }

        public void Cancel()
        {
            IsCancel = true;
            IsConfirm = false;
        }
    }
}
