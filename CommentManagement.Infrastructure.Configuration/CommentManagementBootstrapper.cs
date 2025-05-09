﻿using _01_Framework.Infrastructure;
using CommentManagement.Application;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infrastructure.Configuration.Permission;
using CommentManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CommentManagement.Infrastructure.Configuration
{
    public class CommentManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<ICommentApplication,CommentApplication>();
            services.AddTransient<ICommentRepository,CommentRepository>();

            services.AddTransient<IPermissionExposer, CommentPermissionExposer>();

            services.AddDbContext<CommentContext>(x => x.UseSqlServer(connectionString));
        }

    }
}
