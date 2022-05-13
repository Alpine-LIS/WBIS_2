using DevExpress.Xpf.Editors;
using WBIS_2.Modules.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WBIS_2.Modules
{
    public  class UserControlExtension
    {
        bool EditGridControl = false;
        public UserControlExtension(UserControl self, bool editGridControl= false)
        {
            _userControl = self;
            EditGridControl = editGridControl;
            self.Loaded += SelfLoaded;

            if (_userControl.DataContext is WBISViewModelBase rmb)
            {
                rmb.RecordSaved += ResetGridTracker;
            }
            //if (loaded) SelfLoaded(self, new RoutedEventArgs());
        }
        public void ResetGridTracker(object semder, EventArgs e)
        {
            OriginalValues = new Dictionary<Guid, Dictionary<string, object>>();
        }

        private  UserControl _userControl;

        private  void SelfLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var children = AllChildren((DependencyObject)sender);
            foreach (DependencyObject control in children)
            {              
                //if (control is TextBox tb)
                //{
                //    //if (tb.IsReadOnly) return;
                //    tb.TextChanged += Tb_TextChanged;
                //}
                if (control is TextEdit tb1)
                {
                    if (tb1.Name == "CbxChildren") continue;
                    //if (tb.IsReadOnly) return;
                    //if (EditGridControl && tb1.Parent == null) return;
                    if (tb1.Parent == null && EditGridControl) tb1.EditValueChanged += Tb1_EditValueChanged1; 
                    else tb1.EditValueChanged += Tb1_EditValueChanged;
                }
                //if (control is ComboBox cb)
                //{
                //    //if (cb.IsReadOnly) return;
                //    cb.SelectionChanged += Cb_SelectionChanged;
                //}
                if (control is ComboBoxEdit cb1)
                {
                    if (cb1.Name == "CbxChildren") continue;
                    //if (cb1.IsReadOnly) return;
                    // if (EditGridControl && cb1.Parent == null) return;
                    //cb1.SelectedIndexChanged += Cb1_SelectedIndexChanged;
                    if (cb1.Parent == null && EditGridControl) cb1.EditValueChanged += Cb1_EditValueChanged1;
                    else cb1.EditValueChanged += Cb1_EditValueChanged;
                }
            }
        }


      
        private void Tb1_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            if (sender is TextEdit tb)
            {
                tb.FontWeight = FontWeights.Bold;
                tb.TextDecorations = TextDecorations.Underline;
                if (_userControl.DataContext is WBISViewModelBase rmb)
                {
                    rmb.Changed = true;
                }
            }
        }

        private void Cb1_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            if (sender is ComboBoxEdit cb)
            {
                cb.FontWeight = FontWeights.Bold;
                if (_userControl.DataContext is WBISViewModelBase rmb)
                {
                    rmb.Changed = true;
                }
            }
        }

        private void Tb1_EditValueChanged1(object sender, EditValueChangedEventArgs e)
        {
            if (sender is TextEdit tb)
            {
                if (!CheckOriginalValue(tb, e.NewValue, e.OldValue))
                {
                    tb.FontWeight = FontWeights.Bold;
                    tb.TextDecorations = TextDecorations.Underline;
                    if (_userControl.DataContext is WBISViewModelBase rmb)
                    {
                        rmb.Changed = true;
                    }
                }
                else
                {
                    tb.FontWeight = FontWeights.Normal;
                    tb.TextDecorations = null;
                }
                //if (_userControl.DataContext is RMSViewModelBase rmb)
                //{
                //    rmb.Changed = true;
                //}
            }
        }

        private void Cb1_EditValueChanged1(object sender, EditValueChangedEventArgs e)
        {
            if (sender is ComboBoxEdit cb)
            {
                if (!CheckOriginalValue(cb, e.NewValue, e.OldValue)) 
                {
                    cb.FontWeight = FontWeights.Bold;
                    if (_userControl.DataContext is WBISViewModelBase rmb)
                    {
                        rmb.Changed = true;
                    }
                }
                else
                {
                    cb.FontWeight = FontWeights.Normal;
                }

                //if (_userControl.DataContext is RMSViewModelBase rmb)
                //{
                //    rmb.Changed = true;
                //}
            }
        }


        //Some activity types have editable gridcontrols.
        //These get listed as edited whenever they are sorted or displayed because of how devexpress handels cells.
        //if this edit object has been listed yet in OriginalValues then record it and assume that it has not been edited.
        //If it has been added and vbalue is not the original value then it has been edited
        Dictionary<Guid, Dictionary<string,object>> OriginalValues = new Dictionary<Guid, Dictionary<string, object>>();
        private bool CheckOriginalValue(BaseEdit control, object newVal, object oldVal)
        {
            if (control.Parent == null && EditGridControl)
            {
                //if (newVal == null) return false;
                //else if (newVal.GetType() == typeof(string))
                //{ if ((string)newVal == "" || (string)newVal == "0.00") return false; }
                if (((DevExpress.Xpf.Grid.EditGridCellData)control.DataContext).Row == null) return true;
                if (((DevExpress.Xpf.Grid.EditGridCellData)control.DataContext).Row.GetType().Name == "NotLoadedObject") return true;

                Guid rowId = ((WBIS_2.DataModel.IUserRecords)((DevExpress.Xpf.Grid.EditGridCellData)control.DataContext).Row).Guid;
                string filedName = ((DevExpress.Xpf.Grid.EditGridCellData)control.DataContext).Column.FieldName;

                if (!OriginalValues.ContainsKey(rowId)) OriginalValues.Add(rowId, new Dictionary<string, object>());
                if (!OriginalValues[rowId].ContainsKey(filedName)) 
                {
                    OriginalValues[rowId].Add(filedName, null);
                    if (newVal != null) OriginalValues[rowId][filedName] = newVal;
                    return true;
                }
                else
                {
                    if (newVal == null) return true;
                    if (OriginalValues[rowId][filedName] == null) OriginalValues[rowId][filedName] = newVal;
                    //if (newVal == null) return false;
                    //else if (newVal.GetType() == typeof(string))
                    //{ if ((string)newVal == "" || (string)newVal == "0.00") return false; }

                    return OriginalValues[rowId][filedName].Equals(newVal);
                }

                //if (oldVal == null) return false;
                //else if (oldVal.GetType() == typeof(string))
                //{ if ((string)oldVal == "" || (string)oldVal == "0.00") return false; }
            }
            return true;
        }








        //private void Cb1_SelectedIndexChanged(object sender, RoutedEventArgs e)
        //{
        //    if (sender is ComboBoxEdit cb)
        //    {
        //        //if (!CheckOriginalValue(cb, cb.value)) return;

        //        cb.FontWeight = FontWeights.Bold;
        //        if (_userControl.DataContext is RMSViewModelBase rmb)
        //        {
        //            rmb.Changed = true;
        //        }
        //    }
        //}
        //private  void Cb1_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        //{
        //    if (sender is ComboBox cb)
        //    {
        //        cb.FontWeight = FontWeights.Bold;
        //        if (_userControl.DataContext is RMSViewModelBase rmb)
        //        {
        //            rmb.Changed = true;
        //        }
        //    }
        //}

        //private  void Cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (sender is ComboBox cb)
        //    {
        //        cb.FontWeight = FontWeights.Bold;
        //        if (_userControl.DataContext is RMSViewModelBase rmb)
        //        {
        //            rmb.Changed = true;
        //        }
        //    }
        //}

        //private  void Tb_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (sender is TextBox tb)
        //    {
        //        tb.FontWeight = FontWeights.Bold;
        //        tb.TextDecorations = TextDecorations.Underline;
        //        if (_userControl.DataContext is RMSViewModelBase rmb)
        //        {
        //            rmb.Changed = true;
        //        }
        //    }
        //}

        private List<Control> AllChildren(DependencyObject parent)
        {
            var list = new List<Control> { };
            for (int count = 0; count < VisualTreeHelper.GetChildrenCount(parent); count++)
            {
                var child = VisualTreeHelper.GetChild(parent, count);
                if (child is Control)
                {
                    list.Add(child as Control);
                }
                list.AddRange(AllChildren(child));
            }
            return list;
        }
    }
}
