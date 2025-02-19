﻿using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Domain.RoleAgg;

public class RolePermission
{
    public long Id { get; private set; }
    public int Code { get; private set; }
    public string Name { get; private set; }
    public long RoleId { get; private set; }
    public Role Role { get; private set; }

    public RolePermission(int code, string name)
    {
        Code = code;
        Name = name;
    }

    public RolePermission(int code)
    {
        Code = code;
    }
}