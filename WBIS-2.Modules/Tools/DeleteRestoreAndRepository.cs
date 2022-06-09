using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WBIS_2.DataModel;

namespace WBIS_2.Modules.Tools
{
    public class DeleteRestoreAndRepository
    {
        WBIS2Model Database = new WBIS2Model();
        IQueryable<object> TrackedRecords { get; set; }
        
        /// <summary>
        /// sets if repository or _delete is being modified.
        /// </summary>
        PropertyInfo property { get; set; }
        public DeleteRestoreAndRepository(IInformationType[] _UnTrackedRecords, string _propName)
        {
            IInfoTypeManager m= _UnTrackedRecords[0].Manager;
            TrackedRecords = (IQueryable<object>)m.GetQueryable(_UnTrackedRecords, m.InformationType, Database, track: true);
            property = m.InformationType.GetProperty(_propName);
        }
        

        public void DeleteRecords()
        {
            var w = new WaitWindowHandler();
            w.Start();
            DateTime dateTime = DateTime.Now;

            FindDeletableChildren(TrackedRecords);
            DeleteRecords(TrackedRecords);
            Database.SaveChanges();

            if (((IInformationType)TrackedRecords.First()).Manager.InformationType == typeof(ProtectionZone))
            {
                var hexs = Database.Hex160s
               .Include(_ => _.ProtectionZones)
               .Where(_ => _.ProtectionZones.Any(x => x.DateModified >= dateTime)).Select(_ => _.Hex160ID).ToArray();
                new Hex160_PZs(hexs);
            }

            w.Stop();
        }
        private void FindDeletableChildren(IQueryable<object> records)
        {
            IInfoTypeManager manager = ((IInformationType)records.First()).Manager;
            var children = manager.GetChildren(true, "")
                    .Where(_ => _.Manager.InformationType.GetInterfaces().Contains(typeof(object)));
            foreach (var child in children)
            {
                var childrenRecords = (IQueryable<object>)child.Manager.GetQueryable(records.ToArray(), manager.InformationType, Database, track: true);
                if (childrenRecords.Count() > 0)
                {
                    FindDeletableChildren(childrenRecords);
                    DeleteRecords(childrenRecords);
                }
            }

            //.ChangeTracker.Clear();
        }
        private void DeleteRecords(IQueryable<object> records)
        {
            foreach (var record in records)
                property.SetValue(record, true);
                    //record._delete = true;
        }


        public void RestoreRecords()
        {
            var w = new WaitWindowHandler();
            w.Start();
            DateTime dateTime = DateTime.Now;

            string problem = FindRestorableParents(TrackedRecords.ToArray());
            if (problem != "")
            {
                w.Stop();
                MessageBox.Show(problem);
                Database.ChangeTracker.Clear();
                return;
            }
            problem = RestoreRecords(TrackedRecords.ToArray());
            if (problem != "")
            {
                w.Stop();
                MessageBox.Show(problem);
                Database.ChangeTracker.Clear();
                return;
            }
            Database.SaveChanges();
           
            if (((IInformationType)TrackedRecords.First()).Manager.InformationType == typeof(ProtectionZone))
            {
                var hexs = Database.Hex160s
               .Include(_ => _.ProtectionZones)
               .Where(_ => _.ProtectionZones.Any(x => x.DateModified >= dateTime)).Select(_ => _.Hex160ID).ToArray();
                new Hex160_PZs(hexs);
            }

            w.Stop();
        }
        private string FindRestorableParents(object[] records)
        {
            IInfoTypeManager manager = ((IInformationType)records.First()).Manager;
            var parents = manager.PossibleParents
                    .Where(_ => _.Manager.InformationType.GetInterfaces().Contains(typeof(object))).ToArray();
            foreach (var parent in parents)
            {
                var parentRecords = ((IQueryable<object>)parent.Manager.GetQueryableFromChildren(records.ToArray(), manager.InformationType, Database)).ToArray();
                if (parentRecords.Count() > 0)
                {
                    FindRestorableParents(parentRecords);
                    string problem = RestoreRecords(parentRecords);
                    if (problem != "") return problem; 
                }
            }
            return "";
        }
        private string RestoreRecords(object[] records)
        {
            foreach (var record in records)
            {
                if (record.GetType() == typeof(BotanicalScoping))
                {
                    var thpStr = ((BotanicalScoping)record).THP_Area.THPName.ToUpper().Trim();
                    var thp = Database.THP_Areas.FirstOrDefault(_ => _.THPName.ToUpper().Trim() == thpStr);
                    if (Database.BotanicalScopings
                    .Include(_ => _.THP_Area).Any(_ => _.THP_Area == thp && _.Guid != ((BotanicalScoping)record).Guid && !_._delete))
                        return $"Records could not be restored. A botanical scoping with the thp {((BotanicalScoping)record).THP_Area.THPName} already exists.";
                }
                property.SetValue(record, false);
                //record._delete = false;
            }
            return "";
        }


        public void StoreRepositoryRecords()
        {

        }
        public void ReviveRepositoryRecords()
        {

        }
    }
}
