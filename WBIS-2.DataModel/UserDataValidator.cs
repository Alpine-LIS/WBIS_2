using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Mvvm;

namespace WBIS_2.DataModel
{
    public class UserDataValidator : BindableBase, IDataErrorInfo
    {
        List<string> Errors = new List<string>();
        public string this[string columnName]
        {
            get
            {
                string ErrorStr = IDataErrorInfoHelper.GetErrorText(this, columnName);
                if (ErrorStr == "") Errors.Remove(columnName);
                else Errors.Add(columnName);
                return ErrorStr;
            }
        }
        string IDataErrorInfo.Error => string.Empty;
        public bool HasErrors()
        {
            if (Errors.Count > 0)
            {
                foreach (string propertyName in Errors)
                {
                    var property = this.GetType().GetProperty(propertyName);
                    var val = property.GetValue(this, null);
                    if (val == null) return true;
                }
            }

            //return Errors.Count > 0;

            return false;
        }
    }
}
