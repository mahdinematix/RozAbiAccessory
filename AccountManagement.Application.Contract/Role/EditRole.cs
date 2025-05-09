﻿using _01_Framework.Infrastructure;

namespace AccountManagement.Application.Contract.Role;

public class EditRole : CreateRole
{
    public long Id { get; set; }
    public List<PermissionDto> MappedPermissions { get; set; }

    public EditRole()
    {
        Permissions = new List<int>();
    }
}