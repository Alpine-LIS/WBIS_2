using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace WBIS_2.DataModel
{
    public class ProtectionZone : UserDataValidator, IUserRecords
    {
        public ApplicationUser User { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime DateAdded { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime DateModified { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool _delete { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Guid Guid { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string DisplayName { get { return "Proection Zone"; } }

        public IInformationType[] AvailibleChildren => throw new NotImplementedException();

        public List<KeyValuePair<string, string>> DisplayFields => throw new NotImplementedException();
    }
}
