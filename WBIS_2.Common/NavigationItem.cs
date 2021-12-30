using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
namespace WBIS_2.Common
{
    [Serializable]
    public class NavigationItem : INavigationItem, ISupportState<NavigationItem>
    {
        public string Caption { get; set; }
        public NavigationItem() { }
        public NavigationItem(string caption)
        {
            Caption = caption;
        }

        #region Serialization
        NavigationItem ISupportState<NavigationItem>.SaveState()
        {
            return this;
        }
        void ISupportState<NavigationItem>.RestoreState(NavigationItem state)
        {
            Caption = state.Caption;
        }
        #endregion
    }
}
