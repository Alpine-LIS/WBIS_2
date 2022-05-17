using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WBIS_2.DataModel;

namespace WBIS_2.Modules.Tools
{
    public class DeleteRestoreAndRepository
    {
        WBIS2Model Database = new WBIS2Model();
        IQueryable<IUserRecords> TrackedRecords { get; set; }
        public DeleteRestoreAndRepository(IInformationType[] _UnTrackedRecords)
        {
            IInfoTypeManager m= _UnTrackedRecords[0].Manager;
            TrackedRecords = (IQueryable<IUserRecords>)m.GetQueryable(_UnTrackedRecords, m.InformationType, Database, track: true);
        }
        

        public void DeleteRecords()
        {
            var w = new WaitWindowHandler();
            w.Start();

            FindDeletableChildren(TrackedRecords);
            DeleteRecords(TrackedRecords);
            Database.SaveChanges();

            w.Stop();
        }
        private void FindDeletableChildren(IQueryable<IUserRecords> records)
        {
            IInfoTypeManager manager = records.First().Manager;
            var children = manager.AvailibleChildren
                    .Where(_ => _.Manager.InformationType.GetInterfaces().Contains(typeof(IUserRecords)));
            foreach (var child in children)
            {
                var childrenRecords = (IQueryable<IUserRecords>)child.Manager.GetQueryable(records.ToArray(), manager.InformationType, Database, track: true);
                if (childrenRecords.Count() > 0)
                {
                    FindDeletableChildren(childrenRecords);
                    DeleteRecords(childrenRecords);
                }
            }

            //.ChangeTracker.Clear();
        }
        private void DeleteRecords(IQueryable<IUserRecords> records)
        {
            foreach (var record in records)
                record._delete = true;
        }


        public void RestoreRecords()
        {
            var w = new WaitWindowHandler();
            w.Start();

            string problem = FindRestorableParents(TrackedRecords);
            if (problem != "")
            {
                w.Stop();
                MessageBox.Show(problem);
                Database.ChangeTracker.Clear();
                return;
            }
            problem = RestoreRecords(TrackedRecords);
            if (problem != "")
            {
                w.Stop();
                MessageBox.Show(problem);
                Database.ChangeTracker.Clear();
                return;
            }
            Database.SaveChanges();

            w.Stop();
        }
        private string FindRestorableParents(IQueryable<IUserRecords> records)
        {
            IInfoTypeManager manager = records.First().Manager;
            var parents = manager.PossibleParents
                    .Where(_ => _.Manager.InformationType.GetInterfaces().Contains(typeof(IUserRecords)));
            foreach (var parent in parents)
            {
                var parentRecords = (IQueryable<IUserRecords>)parent.Manager.GetQueryableFromChildren(records.ToArray(), manager.InformationType, Database);
                if (parentRecords.Count() > 0)
                {
                    FindRestorableParents(parentRecords);
                    string problem = RestoreRecords(parentRecords);
                    if (problem != "") return problem; 
                }
            }
            return "";
        }
        private string RestoreRecords(IQueryable<IUserRecords> records)
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
                record._delete = false;
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
