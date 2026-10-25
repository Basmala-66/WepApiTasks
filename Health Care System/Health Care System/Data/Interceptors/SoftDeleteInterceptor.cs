using Health_Care_System.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace Health_Care_System.Data.Interceptors
{
    public class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            ApplySoftDelete(eventData);
            return result;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            ApplySoftDelete(eventData);
            return ValueTask.FromResult(result);
        }

        private static void ApplySoftDelete(DbContextEventData eventData)
        {
            if (eventData.Context is null)
                return;

            foreach (var entry in eventData.Context.ChangeTracker.Entries())
            {
                if (entry.State != EntityState.Deleted ||
                    entry.Entity is not BaseEntity entity)
                    continue;

                entry.State = EntityState.Modified;
                entity.IsDeleted = true;
                entity.ModifiedAt = DateTime.UtcNow;
            }
        }
    }
}
