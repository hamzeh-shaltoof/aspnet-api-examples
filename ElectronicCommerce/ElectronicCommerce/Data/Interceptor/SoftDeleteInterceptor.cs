using ElectronicCommerce.Entities.Contact;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ElectronicCommerce.Data.Interceptor
{
    public class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (eventData == null)
                return result;

            foreach (var entry in eventData.Context!.ChangeTracker.Entries())
            {
                if (entry is not { State: EntityState.Deleted, Entity: ISoftDelete entity })
                    continue;

                entry.State = EntityState.Modified;
                entity.Delete();
            }
            return result;
        }
    }
}
