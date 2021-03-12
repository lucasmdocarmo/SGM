using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SGM.Shared.Core.Contracts.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Manager.Infra.Context
{
    public static class ManagerContextInitialization
    {
        public static void EnsureMigrationOfContext<T>(this IApplicationBuilder app) where T : DbContext
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var context = scope.ServiceProvider.GetService<ManagerContext>();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            if (!unitOfWork.CheckIfDatabaseExists())
            {
                context.Database.EnsureCreated();
            }
            else { context.Database.Migrate(); }

        }
    }
}
