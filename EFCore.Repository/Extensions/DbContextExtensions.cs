using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Infrastructure.Extensions;

public static class DbContextExtensions
{
    public static async Task<T> WithNoLock<T>(this DbContext context, Func<Task<T>> query)
    {
        using var transaction = context.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
        var result = await query();
        transaction.Commit();
        return result;
    }
}
