﻿using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WBIS_2.Modules.Views.RecordImporters
{
    /// <summary>
    /// Interaction logic for RecordImportGridControl.xaml
    /// </summary>
    public partial class RecordImportGridControl : UserControl
    {
        public RecordImportGridControl()
        {
            InitializeComponent();
        }

        private void ComboBoxEdit_KeyDown(object sender, KeyEventArgs e)
        {
            ((ComboBoxEdit)sender).SelectedIndex = -1;
            ((ComboBoxEdit)sender).SelectedItem = null;
        }
    }
}
