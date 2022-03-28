using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace WBIS_2.DataModel
{
    public static class Tracker
    {
        public static bool ChangesSaving = false;
        public static event EventHandler<IEnumerable<EntityEntry>> ChangesSaved;
        public static void SendEvent(IEnumerable<EntityEntry> entries)
        {
            ChangesSaved?.Invoke(null, entries);
        }
    }
}
