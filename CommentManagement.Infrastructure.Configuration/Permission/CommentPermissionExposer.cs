using _01_Framework.Infrastructure;

namespace CommentManagement.Infrastructure.Configuration.Permission
{
    public class CommentPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Comment",new List<PermissionDto>
                    {
                        new PermissionDto(CommentPermissions.ListComments,"ListComments"),
                        new PermissionDto(CommentPermissions.Confirm,"Confirm"),
                        new PermissionDto(CommentPermissions.Cancel,"Cancel")
                    }
                }
            };
        }
    }
}
