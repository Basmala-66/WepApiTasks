using Health_Care_System.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace Health_Care_System.Data.Interceptors
{
    public class ModifiedAtInterceptor: SaveChangesInterceptor
    {

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData,InterceptionResult<int> result)
        {
            ApplyModifiedAt(eventData);
            return result;
        }




        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
           DbContextEventData eventData,
           InterceptionResult<int> result,
           CancellationToken cancellationToken = default)
        {
            ApplyModifiedAt(eventData);
            return ValueTask.FromResult(result);
        }




        private static void ApplyModifiedAt(DbContextEventData eventData)
        {
            if (eventData.Context is null)
                return;

            foreach (var entry in eventData.Context.ChangeTracker.Entries())
            {
                if (entry.State != EntityState.Modified ||
                    entry.Entity is not BaseEntity entity)
                    continue;

                entity.ModifiedAt = DateTime.UtcNow;
            }
        }

    }
}
