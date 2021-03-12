using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SGM.Shared.Core.Contracts.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Cidadao.Infra.Context
{
    public static class CidadaoContextInitialization
    {
        public static void EnsureMigrationOfContext<T>(this IApplicationBuilder app) where T : DbContext
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var context = scope.ServiceProvider.GetService<CidadaoContext>();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            if (!unitOfWork.CheckIfDatabaseExists())
            {
                context.Database.EnsureCreated();
            }
            else { context.Database.Migrate(); }
            
        }
    }

}
